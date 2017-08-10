using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace MkvTests.Ebml.Elements
{
    public class StringElement : BinaryElement
    {
        public Encoding Charset { get; }

        public StringElement(byte[] typeId) : base(typeId)
        {
            Charset = Encoding.ASCII;
        }

        public StringElement() : base()
        {
            Charset = Encoding.ASCII;
        }

        public StringElement(byte[] typeId, Encoding encoding) : base(typeId)
        {
            Charset = encoding;
        }

        public StringElement(Encoding encoding) : base()
        {
            Charset = encoding;
        }

        public string Value
        {
            get
            {
                return Charset.GetString(Data.ToArray());
            }
            set
            {
                Data = new MemoryStream(Charset.GetBytes(value));
            }
        }
    }
}
