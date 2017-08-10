using Microsoft.Extensions.Logging;
using MkvTests.Ebml;
using MkvTests.Ebml.DocTypes;
using MkvTests.Ebml.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MkvTests.Matroska
{
    public class MatroskaFile
    {
        public static readonly int CLUSTER_TRACK_SEARCH_COUNT = 4;
        private readonly FileStream stream;
        private readonly EbmlReader reader;
        private readonly ILogger<MatroskaFile> logger;
        private Element level0;
        private DateTime segmentDate;
        private string writingApp;
        private readonly List<MatroskaFileTrack> trackList;
        private readonly List<MatroskaFileTagEntry> tagList;
        private readonly List<MatroskaFileFrame> frameQueue;
        public string Report
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                int t;

                sb.Append("MatroskaFile report\n");

                sb.Append($"\tSegment Title: {SegmentTitle}\n");
                sb.Append($"\tSegment Date: {segmentDate}\n");
                sb.Append($"\tMuxing App: {MuxingApp}\n");
                sb.Append($"\tWriting App: {writingApp}\n");
                sb.Append($"\tDuration: {Duration / 1000}sec\n");
                sb.Append($"\tTimecodeScale: {TimecodeScale}\n");

                sb.Append($"Track Count: {trackList.Count}\n");
                for(t = 0; t < trackList.Count; t++)
                {
                    sb.Append($"\tTrack {t}\n");
                    sb.Append(trackList[t].ToString());
                }

                sb.Append($"Tag Count: {tagList.Count}\n");
                for(t = 0; t < tagList.Count; t++)
                {
                    sb.Append($"Tag Entry\n");
                    sb.Append(tagList[t].ToString());
                }

                sb.Append("End Report\n");
                return sb.ToString();
            }
        }

        public MatroskaFileTrack[] TrackList
        {
            get
            {
                if(trackList.Count > 0)
                {
                    MatroskaFileTrack[] tracks = trackList.ToArray();
                    return tracks;
                }
                else
                {
                    return new MatroskaFileTrack[0];
                }
            }
        }

        public long TimecodeScale { get; private set; }

        public string SegmentTitle { get; private set; }

        public string MuxingApp { get; private set; }

        public double Duration { get; private set; }

        public bool ScanFirstCluster { get; set; }

        public MatroskaFile(FileStream inputDataSource)
        {
            level0 = null;
            TimecodeScale = 1000000;
            stream = inputDataSource;
            reader = new EbmlReader(stream);
            trackList = new List<MatroskaFileTrack>();
            tagList = new List<MatroskaFileTagEntry>();
            frameQueue = new List<MatroskaFileFrame>();
            ScanFirstCluster = true;
            logger = null;

            initDocTypes();
        }

        public MatroskaFile(FileStream inputDataSource, ILogger<MatroskaFile> logger)
        {
            level0 = null;
            TimecodeScale = 1000000;
            stream = inputDataSource;
            reader = new EbmlReader(stream);
            trackList = new List<MatroskaFileTrack>();
            tagList = new List<MatroskaFileTagEntry>();
            frameQueue = new List<MatroskaFileFrame>();
            ScanFirstCluster = true;
            this.logger = logger;

            initDocTypes();
        }

        private void initDocTypes()
        {
            Type type = typeof(MatroskaDocTypes);
            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }

        /// <summary>
        /// Read and Parse the Matroska file. Call this before any other method.
        /// </summary>
        public void ReadFile()
        {
            Element level1 = null;
            Element level2 = null;

            level0 = reader.ReadNextElement();
            if (level0 == null)
            {
                throw new Exception("Error: Unable to scan for EBML elements.");
            }

            if (level0.IsType(MatroskaDocTypes.EBML.Type))
            {
                level1 = ((MasterElement)level0).ReadNextChild(reader);

                while (level1 != null)
                {
                    level1.ReadData(stream);
                    if (level1.IsType(MatroskaDocTypes.DocType.Type))
                    {
                        string docType = ((StringElement)level1).Value;
                        if (!docType.Equals("matroska") && !docType.Equals("webm"))
                        {
                            throw new Exception($"Error: DocType is not matroska, \"{docType}\"");
                        }
                    }
                    level1 = ((MasterElement)level0).ReadNextChild(reader);
                }
            }
            else
            {
                throw new Exception("Error: EBML Header not the first element in the file.");
            }

            level0 = reader.ReadNextElement();
            if (level0.IsType(MatroskaDocTypes.Segment.Type))
            {
                level1 = ((MasterElement)level0).ReadNextChild(reader);
                logger?.LogDebug("Got segment element.");
                while (level1 != null)
                {
                    logger?.LogDebug($"Got {level1.TypeInfo.Name} element in segment.");
                    if (level1.IsType(MatroskaDocTypes.Info.Type))
                    {
                        parseSegmentInfo(level1, level2);
                    }
                    else if (level1.IsType(MatroskaDocTypes.Tracks.Type))
                    {
                        parseTracks(level1, level2);
                    }
                    else if (level1.IsType(MatroskaDocTypes.Cluster.Type))
                    {
                        if (ScanFirstCluster)
                            parseNextCluster(level1);

                        break;
                    }
                    else if (level1.IsType(MatroskaDocTypes.Tags.Type))
                    {
                        parseTags(level1, level2);
                    }

                    level1.SkipData(stream);
                    level1 = ((MasterElement)level0).ReadNextChild(reader);
                }
            }
            else
            {
                throw new Exception($"Error: Segment not the second element in the file: was {level0.TypeInfo.Name}");
            }
        }

        /// Get the next MatroskaFileFrame
        /// </summary>
        /// <returns>The next MatroskaFileFrame in the queue, or null if the file has ended.</returns>
        public MatroskaFileFrame GetNextFrame()
        {
            if (frameQueue.Count == 0)
                fillFrameQueue();

            if (frameQueue.Count == 0)
            {
                return null;
            }
            return frameQueue[0];
        }

        /// <summary>
        /// Get the next MatroskaFileFrame, limited by TrackNo
        /// </summary>
        /// <param name="trackNumber">The track number to only get MatroskaFileName(s) from.</param>
        /// <returns>The next MatroskaFileFrame in the queue, or null if there are no more frames for the TrackNumber Track.</returns>
        public MatroskaFileFrame GetNextFrame(int trackNumber)
        {
            if (frameQueue.Count == 0)
                fillFrameQueue();

            if (frameQueue.Count == 0)
                return null;

            int tryCount = 0;
            MatroskaFileFrame frame = null;
            try
            {
                var iterator = frameQueue.GetEnumerator();
                while (frame == null)
                {
                    if (iterator.MoveNext())
                    {
                        frame = iterator.Current;
                        if (frame.TrackNumber == trackNumber)
                        {
                            lock (frameQueue)
                            {
                                frameQueue.Remove(frame);
                            }
                            return frame;
                        }
                        frame = null;
                    }
                    else
                    {
                        fillFrameQueue();
                        if (++tryCount > CLUSTER_TRACK_SEARCH_COUNT)
                        {
                            return null;
                        }
                    }
                }
            } catch(Exception e)
            {
                logger?.LogError(e.StackTrace);
                return null;
            }

            return frame;
        }

        /// <summary>
        /// Seek to the requested timecode, rescan clusters and/or discarding frames until we reach the nearest possible timecode, rounded down.
        /// DOESNT SEEM TO BE IMPLEMENTED.
        /// </summary>
        /// <param name="timecode">Timecode to seek to in milliseconds.</param>
        /// <returns>Actual timecode we seeked to.</returns>
        public long Seek(long timecode)
        {
            return 0;
        }

        private void fillFrameQueue()
        {
            if(level0 == null)
            {
                throw new Exception("Call readFile() before reading frames.");
            }

            lock (level0)
            {
                Element level1 = ((MasterElement)level0).ReadNextChild(reader);
                while(level1 != null)
                {
                    if (level1.IsType(MatroskaDocTypes.Cluster.Type))
                    {
                        parseNextCluster(level1);
                    }

                    level1.SkipData(stream);
                    level1 = ((MasterElement)level0).ReadNextChild(reader);
                }
            }
        }

        private void parseNextCluster(Element level1)
        {
            Element level2 = null;
            Element level3 = null;
            ulong clusterTimecode = 0;
            level2 = ((MasterElement)level1).ReadNextChild(reader);

            while(level2 != null)
            {
                if (level2.IsType(MatroskaDocTypes.Timecode.Type))
                {
                    level2.ReadData(stream);
                    clusterTimecode = ((UnsignedIntegerElement)level2).Value;
                }
                else if (level2.IsType(MatroskaDocTypes.SimpleBlock.Type))
                {
                    level2.ReadData(stream);
                    MatroskaBlock block = null;
                    ulong blockDuration = 0;
                    block = new MatroskaBlock(level2.Data);

                    block.ParseBlock();
                    MatroskaFileFrame frame = new MatroskaFileFrame();
                    frame.TrackNumber = block.TrackNumber;
                    frame.Timecode = block.AdjustedBlockTimecode(clusterTimecode, TimecodeScale);
                    frame.Duration = blockDuration;
                    frame.Data = block.GetFrame(0);
                    frame.IsKeyFrame = block.IsKeyFrame;
                    lock (frameQueue)
                    {
                        frameQueue.Add(new MatroskaFileFrame(frame));
                    }

                    if(block.FrameCount > 1)
                    {
                        for(int f = 1; f < block.FrameCount; f++)
                        {
                            frame.Data = block.GetFrame(f);
                            frameQueue.Add(new MatroskaFileFrame(frame));
                        }
                    }
                    level2.SkipData(stream);
                }
                else if (level2.IsType(MatroskaDocTypes.BlockGroup.Type))
                {
                    ulong blockDuration = 0;
                    ulong blockReference = 0;

                    level3 = ((MasterElement)level2).ReadNextChild(reader);

                    MatroskaBlock block = null;
                    while(level3 != null)
                    {
                        if (level3.IsType(MatroskaDocTypes.Block.Type))
                        {
                            level3.ReadData(stream);
                            block = new MatroskaBlock(level3.Data);
                            block.ParseBlock();
                        }
                        else if (level3.IsType(MatroskaDocTypes.BlockDuration.Type))
                        {
                            level3.ReadData(stream);
                            blockDuration = ((UnsignedIntegerElement)level3).Value;
                        }
                        else if (level3.IsType(MatroskaDocTypes.ReferenceBlock.Type))
                        {
                            level3.ReadData(stream);
                            blockReference = ((SignedIntegerElement)level3).Value;
                        }

                        level3.SkipData(stream);
                        level3 = ((MasterElement)level2).ReadNextChild(reader);
                    }

                    if (block == null)
                        throw new NullReferenceException("BlockGroup element with no child Block!");

                    MatroskaFileFrame frame = new MatroskaFileFrame();
                    frame.TrackNumber = block.TrackNumber;
                    frame.Timecode = block.AdjustedBlockTimecode(clusterTimecode, TimecodeScale);
                    frame.Duration = blockDuration;
                    frame.References.Add(blockReference);
                    frame.Data = block.GetFrame(0);
                    frameQueue.Add(new MatroskaFileFrame(frame));

                    if(block.FrameCount > 1)
                    {
                        for(int f = 1; f < block.FrameCount; f++)
                        {
                            frame.Data = block.GetFrame(f);
                            frameQueue.Add(new MatroskaFileFrame(frame));
                        }
                    }
                }

                level2.SkipData(stream);
                level2 = ((MasterElement)level1).ReadNextChild(reader);
            }
        }

        protected bool badMp3Headers()
        {
            var iter = frameQueue.GetEnumerator();
            while (iter.MoveNext())
            {
                MatroskaFileFrame frame = iter.Current;
                if(frame.TrackNumber == 2
                    && frame.Data.ToArray()[3] != 0x54)
                {
                    throw new Exception("Bad MP3 Header! Index: " + iter);
                }
            }
            return false;
        }

        private void parseSegmentInfo(Element level1, Element level2)
        {
            level2 = ((MasterElement)level1).ReadNextChild(reader);

            while(level2 != null)
            {
                if (level2.IsType(MatroskaDocTypes.Title.Type))
                {
                    level2.ReadData(stream);
                    SegmentTitle = ((StringElement)level2).Value;
                }
                else if (level2.IsType(MatroskaDocTypes.DateUTC.Type))
                {
                    level2.ReadData(stream);
                    segmentDate = ((DateElement)level2).Date;
                }
                else if (level2.IsType(MatroskaDocTypes.MuxingApp.Type))
                {
                    level2.ReadData(stream);
                    MuxingApp = ((StringElement)level2).Value;
                }
                else if (level2.IsType(MatroskaDocTypes.WritingApp.Type))
                {
                    level2.ReadData(stream);
                    writingApp = ((StringElement)level2).Value;
                }
                else if (level2.IsType(MatroskaDocTypes.Duration.Type)){
                    level2.ReadData(stream);
                    Duration = ((FloatElement)level2).Value;
                }
                else if (level2.IsType(MatroskaDocTypes.TimecodeScale.Type))
                {
                    level2.ReadData(stream);
                    TimecodeScale = (long)((UnsignedIntegerElement)level2).Value;
                }

                level2.SkipData(stream);
                level2 = ((MasterElement)level1).ReadNextChild(reader);
            }
        }

        private void parseTracks(Element level1, Element level2)
        {
            level2 = ((MasterElement)level1).ReadNextChild(reader);
            while(level2 != null){
                if (level2.IsType(MatroskaDocTypes.TrackEntry.Type))
                {
                    trackList.Add(MatroskaFileTrack.FromElement(level2, stream, reader));
                }
                level2.SkipData(stream);
                level2 = ((MasterElement)level1).ReadNextChild(reader);
            }
        }

        private void parseTags(Element level1, Element level2)
        {
            Element level3 = null;
            Element level4 = null;
            level2 = ((MasterElement)level1).ReadNextChild(reader);

            while(level2 != null)
            {
                if (level2.IsType(MatroskaDocTypes.Tag.Type))
                {
                    MatroskaFileTagEntry tag = new MatroskaFileTagEntry();
                    level3 = ((MasterElement)level2).ReadNextChild(reader);

                    while(level3 != null)
                    {
                        if (level3.IsType(MatroskaDocTypes.Targets.Type))
                        {
                            level4 = ((MasterElement)level3).ReadNextChild(reader);

                            while(level4 != null)
                            {
                                if (level4.IsType(MatroskaDocTypes.TagTrackUID.Type))
                                {
                                    level4.ReadData(stream);
                                    tag.TrackUID.Add((ulong)((UnsignedIntegerElement)level4).Value);
                                }
                                else if (level4.IsType(MatroskaDocTypes.TagChapterUID.Type))
                                {
                                    level4.ReadData(stream);
                                    tag.ChapterUID.Add((ulong)((UnsignedIntegerElement)level4).Value);
                                }
                                else if (level4.IsType(MatroskaDocTypes.TagAttachmentUID.Type))
                                {
                                    level4.ReadData(stream);
                                    tag.AttachmentUID.Add((ulong)((UnsignedIntegerElement)level4).Value);
                                }

                                level4.SkipData(stream);
                                level4 = ((MasterElement)level3).ReadNextChild(reader);
                            }
                        }
                        else if (level3.IsType(MatroskaDocTypes.SimpleTag.Type))
                        {
                            tag.SimpleTags.Add(parseTagsSimpleTag(level3, level4));
                        }
                        level3.SkipData(stream);
                        level3 = ((MasterElement)level2).ReadNextChild(reader);
                    }
                    tagList.Add(tag);
                }

                level2.SkipData(stream);
                level2 = ((MasterElement)level1).ReadNextChild(reader);
            }
        }

        private MatroskaFileSimpleTag parseTagsSimpleTag(Element level3, Element level4)
        {
            MatroskaFileSimpleTag simpleTag = new MatroskaFileSimpleTag();
            level4 = ((MasterElement)level3).ReadNextChild(reader);

            while(level4 != null)
            {
                if (level4.IsType(MatroskaDocTypes.TagName.Type))
                {
                    level4.ReadData(stream);
                    simpleTag.Name = ((StringElement)level4).Value;
                }
                else if (level4.IsType(MatroskaDocTypes.TagString.Type))
                {
                    level4.ReadData(stream);
                    simpleTag.Value = ((StringElement)level4).Value;
                }

                level4.SkipData(stream);
                level4 = ((MasterElement)level3).ReadNextChild(reader);
            }

            return simpleTag;
        }

        public MatroskaFileTrack GetTrack(int trackNumber)
        {
            for(int t = 0; t < trackList.Count; t++)
            {
                MatroskaFileTrack track = trackList[t];
                if (track.TrackNumber == trackNumber)
                    return track;
            }
            return null;
        }
    }
}
