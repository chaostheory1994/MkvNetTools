using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MkvTests.Ebml.Elements
{
    public class SignedIntegerElement : BinaryElement
    {
        public virtual ulong Value
        {
            get
            {
                byte[] dataArray = Data.ToArray();
                ulong length = 0;
                ulong temp = 0;
                length |= ((ulong)dataArray[0] << (56 - ((8 - dataArray.Length) * 8)));
                for(int i = 1; i < dataArray.Length; i++)
                {
                    temp = ((ulong)dataArray[dataArray.Length - i]) << 56;
                    temp >>= 56 - (8 * (i - 1));
                    length |= temp;
                }

                return length;
            }
            set
            {
                this.Data = new MemoryStream(Utility.PackInteger(value));
            }
        }

        public SignedIntegerElement(byte[] typeId) : base(typeId) { }

        public SignedIntegerElement() : base() { }
    }
}
