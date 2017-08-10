using MkvTests.Ebml.DocTypes;
using MkvTests.Ebml.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MkvTests.Ebml.Elements
{
    public class VoidElement : Element
    {
        private static readonly ulong _maxSize;
        public ulong RealSize { get; set; }

        public new ulong Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                if (value < 2 || value > _maxSize)
                    throw new Exception("Sizemust be greater than on and less that (2^52 -2)");

                RealSize = value;
                ulong partialSize = value - (ulong)(Type.Length - Type.Position);
                base.Size = partialSize - Math.Min(partialSize, 8);
            }
        }

        static VoidElement()
        {
            _maxSize = (ulong)(Math.Pow(2, 56) - 2);
        }

        public VoidElement(ulong size) : base()
        {
            TypeInfo = MatroskaDocTypes.Void;
            Type = MatroskaDocTypes.Void.Type;
            Size = size;
        }

        public new ulong WriteHeaderData(FileStream writer)
        {
            int length = 0;

            length += (int)(Type.Length - Type.Position);

            byte[] encodedSize = Utility.MakeEbmlCodedSize(Size, (int)Math.Min(RealSize - (ulong)length, 8));

            length += encodedSize.Length;
            MemoryStream stream = new MemoryStream(length);
            Type.CopyTo(stream);
            stream.Write(encodedSize, 0, encodedSize.Length);
            stream.Flip();
            stream.CopyTo(writer);
            return (ulong)length;
        }

        public new ulong WriteData(FileStream writer)
        {
            byte[] voids = new byte[(int)Size];
            for (int i = 0; i < voids.Length; i++)
                voids[i] = 1;

            writer.Write(voids, 0, voids.Length);

            return (ulong)voids.Length;
        }

        public void ReduceSize(ulong size)
        {
            base.Size = Size - size;
        }
    }
}
