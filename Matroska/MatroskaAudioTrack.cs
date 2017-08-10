using System;
using System.Collections.Generic;
using System.Text;

namespace MkvTests.Matroska
{
    public class MatroskaAudioTrack
    {
        public float SamplingFrequency { get; set; }
        public float OutputSamplingFrequency { get; set; }
        public short Channels { get; set; }
        public byte BitDepth { get; set; }
    }
}
