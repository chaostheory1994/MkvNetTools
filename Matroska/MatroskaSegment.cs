using MkvTests.Ebml;
using MkvTests.Ebml.DocTypes;
using MkvTests.Ebml.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MkvTests.Matroska
{
    public class MatroskaSegment : MasterElement
    {
        public bool UnknownSize { get; set; }

        public MatroskaSegment() : base(MatroskaDocTypes.Segment.Type.ToArray())
        {
            UnknownSize = false;
        }

        public new ulong WriteHeaderData(FileStream writer)
        {
            ulong length = 0;

            MemoryStream type = Type;
            length += (ulong)(type.Length - type.Position);
            type.CopyTo(writer);

            byte[] size;
            if (UnknownSize)
            {
                size = new byte[4];
                size[0] = (byte)(0xFF >> (size.Length - 1));
                for(int i = 1; i < size.Length; i++)
                {
                    size[i] = 0xFF;
                }
            }
            else
            {
                size = Utility.MakeEbmlCodedSize(Size);
            }

            length += (ulong)size.Length;
            writer.Write(size, 0, size.Length);

            return length;
        }

    }
}
