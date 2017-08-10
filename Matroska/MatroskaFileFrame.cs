using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MkvTests.Matroska
{
    public class MatroskaFileFrame
    {
        public int TrackNumber { get; set; }
        public ulong Timecode { get; set; }
        public ulong Duration { get; set; }
        public MemoryStream Data { get; set; }
        public bool IsKeyFrame { get; set; }
        public List<ulong> References { get; set; }

        public MatroskaFileFrame()
        {
            References = new List<ulong>();
        }

        public MatroskaFileFrame(MatroskaFileFrame copy)
        {
            TrackNumber = copy.TrackNumber;
            Timecode = copy.Timecode;
            Duration = copy.Duration;
            IsKeyFrame = copy.IsKeyFrame;
            References = new List<ulong>();
            if(copy.References != null)
            {
                this.References.AddRange(copy.References);
            }
            if (copy.Data != null)
                this.Data = copy.Data;
        }

        public void AddReferences(params ulong[] references)
        {
            References.AddRange(references);
        }
        
    }
}
