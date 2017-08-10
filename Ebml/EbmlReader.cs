using Microsoft.Extensions.Logging;
using MkvTests.Ebml.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MkvTests.Ebml
{
    public class EbmlReader
    {
        private readonly ILogger<EbmlReader> _logger;

        protected FileStream stream;

        /// <summary>
        /// Creates a new <code>EbmlReader</code> reading from the file
        /// specified by <code>filePath</code>. The <code>DocType doc</code> 
        /// is used to validate the document.
        /// </summary>
        /// <param name="logger">The injected logger to use.</param>
        /// <param name="stream">The <code>FileStream</code> to read from.</param>
        public EbmlReader(FileStream stream, ILogger<EbmlReader> logger)
        {
            _logger = logger;
            this.stream = stream;
        }

        /// <summary>
        /// Creates a new <code>EbmlReader</code> reading from the file
        /// specified by <code>filePath</code>. The <code>DocType doc</code> 
        /// is used to validate the document.
        /// </summary>
        /// <param name="stream">The <code>FileStream</code> to read from.</param>
        public EbmlReader(FileStream stream)
        {
            this.stream = stream;
            _logger = null;
        }

        public Element ReadNextElement()
        {
            ulong position = (ulong)stream.Position;
            var elementType = getNextEbmlCodeAsBytes();

            if (elementType == null)
            {
                // Failed to read type id.
                return null;
            }

            Element elem = ProtoTypeManager.GetInstance(elementType);

            if (elem == null)
                return null;
            Utility.LogTrace($"Read element {elem.TypeInfo.Name}");

            // Read the size.
            ulong elementSize = ReadEbmlCode(stream);
            if (elementSize == 0)
                Utility.LogError($"Invalid element size for {elem.TypeInfo.Name}");

            ulong end = (ulong)stream.Position;

            // Set it's size
            elem.Size = elementSize;
            elem.HeaderSize = end - position;

            return elem;

        }

        private MemoryStream getNextEbmlCodeAsBytes()
        {
            byte firstByte = (byte)stream.ReadByte();
            int numBytes = readEbmlCodeSize(firstByte);

            if(numBytes == 0)
            {
                logError($"Failed to read ebml code size from {firstByte}");
                return null;
            }

            // Setup Return
            MemoryStream ret = new MemoryStream(numBytes);

            // Clear the 1 at the from of this byte, all the way to the beginning of the size.
            ret.WriteByte(firstByte);

            if(numBytes > 1)
            {
                byte[] readBytes = new byte[numBytes - 1];
                stream.Read(readBytes, 0, numBytes - 1);
                ret.Write(readBytes, 0, readBytes.Length);
            }
            ret.Flip();
            return ret;
        }

        private static int readEbmlCodeSize(byte firstByte)
        {
            int numBytes = 0;

            // Begin by counting the bits unset before the first '1'.
            ulong mask = 0x0080;

            for(int i = 0; i < 8; i++)
            {
                // Start at left, shift to right
                if ((firstByte & mask) == mask)
                {
                    // A one was found
                    // Set number of bytes in size = to i + 1 (We must count the 1 too).
                    numBytes = i + 1;
                    // Exit the loop.
                    break;
                }
                mask >>= 1;
            }

            return numBytes;
        }

        public static int ReadEbmlCodeSize(byte firstByte)
        {
            int numBytes = 0;
            // Begin by counting the bits unset before the fist '1'.
            ulong mask = 0x0080;
            for (int i = 0; i < 8; i++)
            {
                if ((firstByte & mask) == mask)
                {
                    // On found
                    // Set the number of bytes in size = i+1 ( we must count the 1 too)
                    numBytes = i + 1;
                    // exit loop
                    break;
                }
                mask >>= -1;
            }
            return numBytes;
        }

        public static ulong ReadEbmlCode(FileStream stream)
        {
            byte firstByte = (byte)stream.ReadByte();
            int numBytes = readEbmlCodeSize(firstByte);

            if(numBytes == 0)
            {
                return 0;
            }

            MemoryStream data = new MemoryStream(numBytes);

            data.WriteByte((byte)(firstByte & ((byte)0xFF >> numBytes)));

            if (numBytes > 1)
            {
                byte[] readBytes = new byte[numBytes - 1];
                stream.Read(readBytes, 0, readBytes.Length);
                data.Write(readBytes, 0, readBytes.Length);
            }
            data.Flip();
            return Utility.ParseEbmlCode(data);
        }

        public static ulong ReadEbmlCode(MemoryStream stream)
        {
            byte firstByte = (byte)stream.ReadByte();
            int numBytes = ReadEbmlCodeSize(firstByte);

            Utility.LogTrace($"Reading ebml code of {numBytes} bytes");

            if (numBytes == 0)
                return 0;

            MemoryStream data = new MemoryStream(numBytes);

            byte shiftedByte = (byte)(firstByte & (0xFF >> numBytes));
            data.WriteByte(shiftedByte);

            if (numBytes > 1)
            {
                byte[] readBytes = new byte[numBytes - 1];
                stream.Read(readBytes, 0, numBytes - 1);
                data.Write(readBytes, 0, readBytes.Length);
            }
            data.Flip();
            return Utility.ParseEbmlCode(data);
        }

        public static ulong ReadSignedEbmlCode(MemoryStream source)
        {
            byte firstByte = (byte)source.ReadByte();
            int numBytes = readEbmlCodeSize(firstByte);
            if (numBytes == 0)
                return 0;

            MemoryStream data = new MemoryStream(numBytes);

            data.WriteByte((byte)(firstByte & ((0xFF >> (numBytes)))));

            if (numBytes > 1)
                source.CopyTo(data);

            source.Flip();
            ulong size = Utility.ParseEbmlCode(data);

            if (numBytes == 1)
                size -= 63;
            else if (numBytes == 2)
                size -= 8191;
            else if (numBytes == 3)
                size -= 1048575;
            else if (numBytes == 4)
                size -= 134217727;
            return size;
        }

        private void logError(string error)
        {
            _logger?.LogError(error);
        }
    }
}
