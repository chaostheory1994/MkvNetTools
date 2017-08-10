using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MkvTests.Ebml
{
    public class ProtoType<T> where T : Element, new()
    {
        public int Level { get; private set; }
        public string Name { get; }
        public MemoryStream Type { get; private set; }

        public ProtoType(string name, byte[] type, int level)
        {
            Level = level;
            Name = name;
            Type = new MemoryStream(type);
            ulong codename = Utility.ParseEbmlCode(Type);
            ProtoTypeManager.AddDictionaryElement(codename, this);
            Utility.LogTrace($"Associating {Name} with {codename}");
        }

        public T GetInstance()
        {
            Utility.LogTrace($"Instantiating {typeof(T).Name}");
            T elem = new T();
            elem.Type = Type;
            elem.TypeInfo = this;
            return elem;
        }
    }
}
