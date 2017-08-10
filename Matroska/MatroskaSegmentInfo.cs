using MkvTests.Ebml.DocTypes;
using MkvTests.Ebml.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MkvTests.Matroska
{
    public class MatroskaSegmentInfo
    {
        private static long BLOCK_SIZE;

        static MatroskaSegmentInfo()
        {
            BLOCK_SIZE = 128;
        }

        public ulong TimecodeScale { get; set; }
        public double? Duration { get; set; }
        public DateTime SegmentDate { get; set; }
        public ulong MyPosition { get; set; }

        public MatroskaSegmentInfo(ulong position)
        {
            TimecodeScale = 1000000;
            SegmentDate = new DateTime();
            MyPosition = position;
        }

        public long WriteElement(FileStream writer)
        {
            MasterElement segmentInfoElement = MatroskaDocTypes.Info.GetInstance();

            StringElement writingAppElement = MatroskaDocTypes.WritingApp.GetInstance();
            writingAppElement.Value = "Matroska File Writer v1.0 C#";

            StringElement muxingAppElement = MatroskaDocTypes.MuxingApp.GetInstance();
            muxingAppElement.Value = ".NET EBML v1.0";

            DateElement dateElement = MatroskaDocTypes.DateUTC.GetInstance();
            dateElement.Date = SegmentDate;

            UnsignedIntegerElement timecodeScaleElement = MatroskaDocTypes.TimecodeScale.GetInstance();
            timecodeScaleElement.Value = TimecodeScale;

            segmentInfoElement.AddChildElement(dateElement);
            segmentInfoElement.AddChildElement(timecodeScaleElement);

            if(Duration != null)
            {
                FloatElement durationElement = MatroskaDocTypes.Duration.GetInstance();
                durationElement.Value = (double)Duration;
                segmentInfoElement.AddChildElement(durationElement);
            }

            segmentInfoElement.AddChildElement(writingAppElement);
            segmentInfoElement.AddChildElement(muxingAppElement);

            ulong len = segmentInfoElement.WriteElement(writer);
            VoidElement spacer = new VoidElement((ulong)BLOCK_SIZE - len);
            spacer.WriteElement(writer);
            return 1;
        }
        
        public void Update(FileStream writer)
        {
            long startingPosition = writer.Position;
            writer.Seek((long)MyPosition, SeekOrigin.Begin);
            WriteElement(writer);
            writer.Seek(startingPosition, SeekOrigin.Begin);
        }
    }
}
