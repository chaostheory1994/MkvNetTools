using MkvTests.Ebml;
using MkvTests.Ebml.DocTypes;
using MkvTests.Ebml.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MkvTests.Matroska
{
    public class MatroskaFileTracks
    {
        private static ulong BLOCK_SIZE;
        
        static MatroskaFileTracks()
        {
            BLOCK_SIZE = 4096;
        }

        private long myPosition;
        private List<MatroskaFileTrack> tracks;

        public MatroskaFileTracks(long position)
        {
            myPosition = position;
            tracks = new List<MatroskaFileTrack>();
        }

        public void AddTrack(MatroskaFileTrack track)
        {
            tracks.Add(track);
        }

        public ulong WriteTracks(FileStream writer)
        {
            MasterElement trackElements = MatroskaDocTypes.Tracks.GetInstance();

            foreach(var track in tracks)
            {
                trackElements.AddChildElement(track.ToElement());
            }
            trackElements.WriteElement(writer);
            if (BLOCK_SIZE < trackElements.TotalSize)
                throw new Exception("BLOCK_SIZE was greater than the track element. Something went wrong.");
            new VoidElement(BLOCK_SIZE - trackElements.TotalSize).WriteElement(writer);
            return BLOCK_SIZE;
        }

        public void Update(FileStream writer)
        {
            Utility.LogTrace("Updating Track List!");
            long start = writer.Position;
            writer.Seek(myPosition, SeekOrigin.Begin);
            WriteTracks(writer);
            writer.Seek(start, SeekOrigin.Begin);
        }
    }
}
