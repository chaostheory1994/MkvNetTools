using MkvTests.Ebml.DocTypes;
using MkvTests.Ebml.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MkvTests.Matroska
{
    public class MatroskaFileWriter
    {
        public FileStream Writer { get; set; }
        public MatroskaFileMetaSeek MetaSeek { get; set; }
        public MatroskaFileCues CueData { get; set; }
        public MatroskaCluster Cluster { get; set; }
        public MatroskaSegmentInfo SegmentInfoElement { get; set; }
        public MatroskaFileTracks Tracks { get; set; }
        public ulong TimecodeScale
        {
            get
            {
                return SegmentInfoElement.TimecodeScale;
            }
            set
            {
                SegmentInfoElement.TimecodeScale = value;
            }
        }
        public double? Duration
        {
            get
            {
                return SegmentInfoElement.Duration;
            }
            set
            {
                SegmentInfoElement.Duration = value;
            }
        }
        

        public MatroskaFileWriter(FileStream outputFileStream)
        {
            Writer = outputFileStream;
            writeEBMLHeader();
            writeSegmentHeader();
            ulong endOfSegmentHeader = (ulong)Writer.Position;
            MetaSeek = new MatroskaFileMetaSeek(endOfSegmentHeader);
            CueData = new MatroskaFileCues(endOfSegmentHeader);
            MetaSeek.Write(Writer);
            SegmentInfoElement = new MatroskaSegmentInfo((ulong)Writer.Position);
            MetaSeek.AddIndexedElement(MatroskaDocTypes.Info.Type, (ulong)Writer.Position);
            SegmentInfoElement.WriteElement(Writer);
            MetaSeek.AddIndexedElement(MatroskaDocTypes.Tracks.Type, (ulong)Writer.Position);
            Tracks = new MatroskaFileTracks(Writer.Position);
            Tracks.WriteTracks(Writer);
            Cluster = new MatroskaCluster();
            Cluster.SizeLimit = 128 * 1024;
            Cluster.DurationLimit = 5000;
            MetaSeek.AddIndexedElement(MatroskaDocTypes.Cluster.Type, (ulong)Writer.Position);
        }

        private void writeEBMLHeader()
        {
            MasterElement ebmlHeaderElement = MatroskaDocTypes.EBML.GetInstance();

            UnsignedIntegerElement ebmlVersionElement = MatroskaDocTypes.EBMLVersion.GetInstance();
            ebmlVersionElement.Value = 1;

            UnsignedIntegerElement ebmlReadVersionElement = MatroskaDocTypes.EBMLReadVersion.GetInstance();
            ebmlReadVersionElement.Value = 1;

            UnsignedIntegerElement ebmlMaxIdLengthElement = MatroskaDocTypes.EBMLMaxIDLength.GetInstance();
            ebmlMaxIdLengthElement.Value = 4;

            UnsignedIntegerElement ebmlMaxSizeLengthElement = MatroskaDocTypes.EBMLMaxSizeLength.GetInstance();
            ebmlMaxSizeLengthElement.Value = 8;

            StringElement docTypeElement = MatroskaDocTypes.DocType.GetInstance();
            docTypeElement.Value = "matroska";

            UnsignedIntegerElement docTypeVersionElement = MatroskaDocTypes.DocTypeVersion.GetInstance();
            docTypeVersionElement.Value = 3;

            UnsignedIntegerElement docTypeReadVersionElement = MatroskaDocTypes.DocTypeReadVersion.GetInstance();
            docTypeReadVersionElement.Value = 2;

            ebmlHeaderElement.AddChildElement(ebmlVersionElement);
            ebmlHeaderElement.AddChildElement(ebmlReadVersionElement);
            ebmlHeaderElement.AddChildElement(ebmlMaxIdLengthElement);
            ebmlHeaderElement.AddChildElement(ebmlMaxSizeLengthElement);
            ebmlHeaderElement.AddChildElement(docTypeElement);
            ebmlHeaderElement.AddChildElement(docTypeVersionElement);
            ebmlHeaderElement.AddChildElement(docTypeReadVersionElement);
            ebmlHeaderElement.WriteElement(Writer);
        }

        private void writeSegmentHeader()
        {
            MatroskaSegment segmentElement = new MatroskaSegment();
            segmentElement.UnknownSize = true;
            segmentElement.WriteHeaderData(Writer);

        }

        private void writeSegmentInfo()
        {
            SegmentInfoElement.Update(Writer);
        }

        private void writeTracks()
        {
            Tracks.Update(Writer);
        }

        /// <summary>
        /// Adds a track to the file. You may add tracks at any time before close()ing, even after adding frames for the track.
        /// </summary>
        /// <param name="track"></param>
        public void AddTrack(MatroskaFileTrack track)
        {
            Tracks.AddTrack(track);
        }

        /// <summary>
        /// Adds the silent track notation for the given track to subsequent clusters, note that this has little effect on most players
        /// </summary>
        /// <param name="trackNumber"></param>
        public void SilenceTrack(ulong trackNumber)
        {
            Cluster.SilenceTrack(trackNumber);
        }

        /// <summary>
        /// Removes the silent track notation for this track
        /// </summary>
        /// <param name="trackNumber"></param>
        public void UnsilenceTrack(ulong trackNumber)
        {
            Cluster.UnsilenceTrack(trackNumber);
        }

        /// <summary>
        /// Add a frame.
        /// </summary>
        /// <param name="MatroskaFileFrame">The frame to add.</param>
        /// <param name=""></param>
        public void addFrame(MatroskaFileFrame frame)
        {
            if (!Cluster.AddFrame(frame))
            {
                Flush();
            }
        }

        /**
         * Flushes pending content to disk and starts a new cluster. This is typically not necessary to call manually. 
         */
        public void Flush()
        {
            ulong clusterPos = (ulong)Writer.Position;
            CueData.AddCue(clusterPos, Cluster.ClusterTimecode, (List<int>)Cluster.Tracks);
            Cluster.Flush(Writer);
        }

        /**
         * Finalizes the file by writing the final headers, index, and flushing data to the writer.
         */
        public void Close()
        {
            Flush();

            CueData.Write(Writer, MetaSeek);
            MetaSeek.Update(Writer);
            SegmentInfoElement.Update(Writer);
            Tracks.Update(Writer);
        }
    }
}
