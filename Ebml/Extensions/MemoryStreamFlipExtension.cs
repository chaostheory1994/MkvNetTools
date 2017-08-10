using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MkvTests.Ebml.Extensions
{
    public static class MemoryStreamFlipExtension
    {
        public static MemoryStream Flip(this MemoryStream stream)
        {
            stream.Capacity = (int)stream.Position;
            stream.Position = 0;
            return stream;
        }

        public static byte[] GetInvertedBytes(this MemoryStream stream)
        {
            var bytes = stream.ToArray();
            byte[] ret = new byte[bytes.Length];

            for (int i = 0; i < bytes.Length; i++)
                ret[i] = bytes[bytes.Length - i - 1];

            return ret;
        }
    }
}
