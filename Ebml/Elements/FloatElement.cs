using MkvTests.Ebml.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MkvTests.Ebml.Elements
{
    public class FloatElement : BinaryElement
    {
        public FloatElement(byte[] type) : base(type) { }

        public FloatElement() : base() { }

        public double Value
        {
            get
            {
                ulong position = (ulong)Data.Position;

                if (Size == 4)
                {
                    byte[] readBytes = new byte[4];
                    Data.Read(readBytes, 0, 4);
                    Data.Position = (long)position;
                    return BitConverter.ToSingle(Data.GetInvertedBytes(), 0);
                }
                else if (Size == 8)
                {
                    byte[] readBytes = new byte[8];
                    Data.Read(readBytes, 0, 4);
                    Data.Position = (long)position;
                    return BitConverter.ToDouble(Data.ToArray(), 0);
                }
                else
                {
                    throw new ArithmeticException("80-bit floats are not supported.");
                }
            }
            set
            {
                if (value < float.MaxValue)
                {
                    Data = new MemoryStream(new byte[4]);
                    Data.Write(BitConverter.GetBytes((float)value), 0, 4);
                    Data.Flip();
                }
                else
                {
                    Data = new MemoryStream(new byte[8]);
                    Data.Write(BitConverter.GetBytes(value), 0, 8);
                    Data.Flip();
                }
            }
        }
    }
}
