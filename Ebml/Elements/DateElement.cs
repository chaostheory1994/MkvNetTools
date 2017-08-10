using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MkvTests.Ebml.Elements
{
    public class DateElement : SignedIntegerElement
    {
        public static DateTime DelayedDateTime { get; }
        private static readonly new uint _minSizeLength;

        static DateElement()
        {
            DelayedDateTime = new DateTime(2001, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            _minSizeLength = 8;
        }

        public DateElement() : base() { }

        public DateElement(byte[] type) : base(type) { }

        public override ulong Value
        {
            set
            {
                Data = new MemoryStream(Utility.PackInteger(value, _minSizeLength));
            }
        }

        public DateTime Date
        {
            get
            {
                ulong temp = Value / 1000000000;
                TimeSpan valueTimeSpan = TimeSpan.FromMilliseconds(temp);
                return DelayedDateTime + valueTimeSpan;
            }
            set
            {
                TimeSpan diff = value - DelayedDateTime;
                Data = new MemoryStream(Utility.PackInteger(
                    (ulong)diff.TotalMilliseconds * 1000000000, 
                    _minSizeLength));
            }
        }
    }
}
