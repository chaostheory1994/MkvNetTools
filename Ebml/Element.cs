using MkvTests.Ebml.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MkvTests.Ebml
{
    public class Element
    {
        public static int _minSizeLength { get; set; }

        public Element Parent { get; set; }
        public dynamic TypeInfo { get; set; }
        private MemoryStream type;
        public MemoryStream Type
        {
            get
            {
                MemoryStream ret = new MemoryStream(type.ToArray());
                return ret;
            }
            set
            {
                type = new MemoryStream(value.ToArray());
            }
        }
        private ulong size;
        public ulong Size
        {
            get
            {
                return size;
            }
            set
            {
                HeaderSize = null;
                size = value;
            }
        }
        private MemoryStream data;
        public MemoryStream Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
                if (data != null) Size = (ulong)(data.Length - data.Position);
            }
        }
        public ulong TotalSize
        {
            get
            {
                if (HeaderSize == null)
                    return (ulong)Type.ToArray().Length + (ulong)Utility.CodedSizeLength(Size, 0) + Size;
                else
                    return (ulong)HeaderSize + Size;
            }
        }
        public bool DataRead { get; set; }
        public ulong? HeaderSize { private get; set; }

        /// <summary>
        /// Creates a new isntance of Element
        /// </summary>
        /// <param name="type"></param>
        public Element(byte[] type)
        {
            this.type = new MemoryStream(type);
        }

        public Element() { }

        /// <summary>
        /// Read the element data.
        /// </summary>
        /// <param name="source">The <code>FileStream</code> from which to read the element data.</param>
        public void ReadData(FileStream source)
        {
            // Sedup a buffer for its data.
            data = new MemoryStream((int)Size);

            // Read the data
            byte[] readBytes = new byte[Size];
            source.Read(readBytes, 0, (int)Size);
            Data.Write(readBytes, 0, readBytes.Length);
            Data.Flip();
            DataRead = true;

            Utility.LogTrace($"Read {Size} bytes from {TypeInfo.Name}");
        }

        /// <summary>
        /// Skip the element data
        /// </summary>
        /// <param name="source">The <code>FileStream</code> from which to read the element data.</param>
        public virtual void SkipData(FileStream source)
        {
            if (!DataRead)
            {
                source.Seek((long)Size, SeekOrigin.Current);
                DataRead = true;
            }
        }

        public ulong WriteElement(FileStream writer)
        {
            Utility.LogTrace($"Writing element {TypeInfo.Name} with size {TotalSize}");
            return WriteHeaderData(writer) + WriteData(writer);
        }

        /// <summary>
        /// Writes the element header data. Ovverride this in sub-classes for more speicalized writing.
        /// </summary>
        /// <param name="writer">The <code>FileStream</code> to write to.</param>
        /// <returns></returns>
        public virtual ulong WriteHeaderData(FileStream writer)
        {
            int length = 0;

            length += (int)(Type.Length - Type.Position);

            byte[] encodedSize = Utility.MakeEbmlCodedSize(Size);

            length += encodedSize.Length;
            MemoryStream stream = new MemoryStream((int)length);
            Type.CopyTo(stream);
            stream.Write(encodedSize, 0, encodedSize.Length);
            stream.Flip();
            Utility.LogTrace($"Writing out header {stream.Length - stream.Position}, {Utility.ByteArrayToString(stream.ToArray())}");
            stream.CopyTo(writer);
            return (uint)length;
        }

        /// <summary>
        /// Write the element data. Override this in sub-classes for more specialized writing.
        /// </summary>
        /// <param name="writer">The <code>FileStream</code> to write to.</param>
        /// <returns></returns>
        public ulong WriteData(FileStream writer)
        {
            if (Data == null)
                throw new NullReferenceException($"No data to write: {TypeInfo.Name}, {Type?.ToArray()?.ToString()}");
            ulong position = (ulong)Data.Position;
            int remaining = 0;
            try
            {
                Utility.LogTrace($"Writing data {Data.Length - Data.Position} bytes of {Utility.ByteArrayToString(Data.ToArray())}");
                remaining = (int)(Data.Length - Data.Position);
                Data.CopyTo(writer);
            }
            finally
            {
                Data.Position = (long)position;
            }
            return (uint)remaining;
        }

        public void ClearData()
        {
            Data = null;
        }

        public bool IsType(byte[] typeId)
        {
            return Array.Equals(Type.ToArray(), typeId);
        }

        public bool IsType(MemoryStream typeId)
        {
            byte[] hostType = Type.ToArray();
            byte[] comparedType = typeId.ToArray();
            if (hostType == null || comparedType == null || hostType.Length != comparedType.Length)
                return false;
            for (int i = 0; i < hostType.Length; i++)
                if (hostType[i] != comparedType[i])
                    return false;

            return true;
        }


    }
}
