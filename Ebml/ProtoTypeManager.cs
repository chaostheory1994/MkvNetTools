using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MkvTests.Ebml
{
    public static class ProtoTypeManager
    {
        private static readonly Dictionary<ulong, dynamic> CLASS_MAP = new Dictionary<ulong, dynamic>();

        public static Element GetInstance(MemoryStream stream)
        {
            ulong codeName = Utility.ParseEbmlCode(stream);
            dynamic eType = CLASS_MAP[codeName];
            Utility.LogTrace($"Got codename {codeName}, for element type {eType.Name}");
            return eType.GetInstance();
        }

        public static void AddDictionaryElement(ulong key, dynamic value)
        {
            CLASS_MAP.Add(key, value);
        }
    }
}
