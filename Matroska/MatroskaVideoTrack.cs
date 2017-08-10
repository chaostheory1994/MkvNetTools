using System;
using System.Collections.Generic;
using System.Text;

namespace MkvTests.Matroska
{
    public class MatroskaVideoTrack
    {
        public short PixelWidth { get; set; }
        public short PixelHeight { get; set; }
        public short DisplayWidth { get; set; }
        public short DisplayHeight { get; set; }

        public MatroskaVideoTrack()
        {
            DisplayHeight = 0;
            DisplayWidth = 0;
        }
    }
}
