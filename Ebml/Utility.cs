using Microsoft.Extensions.Logging;
using MkvTests.Matroska;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MkvTests.Ebml
{
    public static class Utility
    {
        public static ILogger _logger { get; set; }

        public static void LogTrace(string message)
        {
            if (_logger != null) _logger.LogTrace(message);
        }

        public static void LogError(string error)
        {
            if (_logger != null) _logger.LogError(error);
        }

        public static void LogDebug(string message)
        {
            if (_logger != null) _logger.LogDebug(message);
        }

        public static ulong ParseEbmlCode(MemoryStream data)
        {
            if (data == null)
                return 0;

            var position = data.Position;

            // Put this into a ulong.
            ulong size = 0;
            for (int i = (int)(data.Length - data.Position) - 1; i >= 0; i--)
            {
                ulong n = (ulong)data.ReadByte() & (byte)0xFF;
                size = size | (n << (8 * i));
            }
            data.Position = position;
            LogTrace($"Parsed EBML code {ByteArrayToString(data.ToArray())} as {size}");
            return size;
        }
        
        public static int CodedSizeLength(ulong value, int minSizeLen)
        {
            int codedSize = 0;
            if (value < 0xFE)
                codedSize = 1;
            else if (value < 16383)
                codedSize = 2;
            else if (value < 2097151)
                codedSize = 3;
            else if (value < 268435455)
                codedSize = 4;
            if ((minSizeLen > 0) && (codedSize <= minSizeLen))
                codedSize = minSizeLen;
            return codedSize;
        }

        public static byte[] MakeEbmlCodedSize(ulong size, int minSizeLen = 0)
        {
            int length = CodedSizeLength(size, minSizeLen);
            byte[] ret = new byte[length];

            ulong mask = 0x00000000000000FF;
            for (int i = 0; i < length; i++)
            {
                ret[length - 1 - i] = (byte)(((ulong)size & mask) >> (i * 8));
                mask <<= 8;
            }

            // The first size bits should be clear, otherwise we have an error in the size determination.
            ret[0] |= (byte)(0x80 >> (length - 1));
            Utility.LogTrace($"Ebml coded size {Utility.ByteArrayToString(ret)} for {size}");
            return ret;
        }

        /// <summary>
        /// Takes a byte array and converts it to a string in hex format.
        /// </summary>
        /// <param name="array">The array of bytes to convert.</param>
        /// <returns></returns>
        public static string ByteArrayToString(byte[] array)
        {
            StringBuilder hex = new StringBuilder(array.Length * 2);
            foreach (byte b in array)
                hex.Append(ByteToString(b));
            return hex.ToString();
        }

        public static string ByteToString(byte b)
        {
            return $"{b:x2}";
        }

        public static uint GetMinByteSize(ulong value)
        {
            ulong absValue = (ulong)Math.Abs((long) value);
            return GetMinByteSizeUnsigned(absValue << 1);
        }

        public static uint GetMinByteSizeUnsigned(ulong value)
        {
            uint size = 8;
            ulong mask = 0xFF00000000000000;
            for(int i = 0; i < 8; i++)
            {
                if((value & mask) == 0)
                {
                    mask = mask >> 8;
                    size--;
                }
                else
                {
                    return size;
                }
            }
            return 8;
        }

        public static byte[] PackIntegerUnsigned(ulong value)
        {
            uint size = GetMinByteSizeUnsigned(value);
            return PackInteger(value, size);
        }

        public static byte[] PackInteger(ulong value)
        {
            uint size = GetMinByteSize(value);
            return PackInteger(value, size);
        }

        public static byte[] PackInteger(ulong value, uint size)
        {
            byte[] ret = new byte[size];
            BitConverter.GetBytes(value).CopyTo(ret, 0);
            return ret;
        }
    }
}
