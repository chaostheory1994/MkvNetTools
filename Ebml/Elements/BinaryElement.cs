using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MkvTests.Ebml.Elements
{
    public class BinaryElement : Element
    {
        public static int MinSizeLength { get; set; }

        static BinaryElement()
        {
            MinSizeLength = 4;
        }

        public BinaryElement(byte[] type) : base(type)
        {
        }

        public BinaryElement() : base() { }

    }
}
