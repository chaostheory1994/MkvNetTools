using MkvTests.Ebml.DocTypes;
using MkvTests.Ebml.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MkvTests.Matroska
{
    public class MatroskaCluster
    {
        private Queue<MatroskaFileFrame> frames;
        private List<ulong> silencedTracks;
        public ulong ClusterTimecode { get; private set; }
        public ICollection<int> Tracks { get; private set; }
        public int SizeLimit { get; set; }
        private int totalSize { get; set; }
        public ulong DurationLimit { get; set; }

        public MatroskaCluster()
        {
            frames = new Queue<MatroskaFileFrame>();
            silencedTracks = new List<ulong>();
            Tracks = new HashSet<int>();
            ClusterTimecode = ulong.MaxValue;
            SizeLimit = int.MaxValue;
            totalSize = 0;
            DurationLimit = ulong.MaxValue;
        }

        /// <summary>
        /// Add a frame to the cluster.
        /// </summary>
        /// <param name="frame">The <code>MatroskaFileFrame</code></param>
        /// <returns>false if you should begin another cluster.</returns>
        public bool AddFrame(MatroskaFileFrame frame)
        {
            if (frame.Timecode < ClusterTimecode)
                ClusterTimecode = frame.Timecode;

            frames.Enqueue(frame);
            totalSize += (int)(frame.Data.Length - frame.Data.Position);
            Tracks.Add(frame.TrackNumber);
            return ((frame.Timecode - ClusterTimecode) < (ulong)DurationLimit) && (totalSize < SizeLimit);
        }

        public ulong Flush(FileStream writer)
        {
            if (frames.Count == 0)
                return 0;

            try
            {
                MasterElement clusterElement = MatroskaDocTypes.Cluster.GetInstance();
                UnsignedIntegerElement timecodeElement = MatroskaDocTypes.Timecode.GetInstance();
                timecodeElement.Value = ClusterTimecode;
                clusterElement.AddChildElement(timecodeElement);

                if(silencedTracks.Count != 0)
                {
                    MasterElement silentElement = MatroskaDocTypes.SilentTracks.GetInstance();
                    foreach(ulong silent in silencedTracks)
                    {
                        UnsignedIntegerElement silentTrackElement = MatroskaDocTypes.SilentTrackNumber.GetInstance();
                        silentTrackElement.Value = silent;
                        silentElement.AddChildElement(silentTrackElement);
                    }
                    clusterElement.AddChildElement(silentElement);
                }

                MatroskaSimpleBlock block = null;
                bool forceNew = true;
                ulong lastTimecode = 0;
                int lastTrackNumber = 0;

                foreach(var frame in frames)
                {
                    frame.Timecode -= ClusterTimecode;
                    if(forceNew || lastTimecode != frame.Timecode || lastTrackNumber != frame.TrackNumber)
                    {
                        if(block != null)
                        {
                            clusterElement.AddChildElement(block.ToElement());
                        }
                        block = new MatroskaSimpleBlock();
                    }
                    lastTimecode = frame.Timecode;
                    lastTrackNumber = frame.TrackNumber;
                    forceNew = true;
                    block.AddFrame(frame);
                }
                if (block != null)
                    clusterElement.AddChildElement(block.ToElement());
                return clusterElement.WriteElement(writer);
            }
            finally
            {
                frames.Clear();
                Tracks.Clear();
                totalSize = 0;
                ClusterTimecode = ulong.MaxValue;
            }
        }

        public void UnsilenceTrack(ulong trackNumber)
        {
            silencedTracks.Remove(trackNumber); 
        }

        public void SilenceTrack(ulong trackNumber)
        {
            silencedTracks.Add(trackNumber);
        }
    }
}
