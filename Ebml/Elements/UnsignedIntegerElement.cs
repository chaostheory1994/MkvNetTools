using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MkvTests.Ebml.Elements
{
    public class UnsignedIntegerElement : BinaryElement
    {
        public UnsignedIntegerElement(byte[] typeId) : base(typeId) { }

        public UnsignedIntegerElement() : base() { }

        public ulong Value
        {
            get
            {
                return (ulong)Utility.ParseEbmlCode(new MemoryStream(Data.ToArray()));
            }
            set
            {
                MemoryStream buffer = new MemoryStream(Utility.PackIntegerUnsigned(value));
                Utility.LogTrace($"Setting value {value} to {Utility.ByteArrayToString(buffer.ToArray())}");
                Data = buffer;
            }
        }
    }
}
