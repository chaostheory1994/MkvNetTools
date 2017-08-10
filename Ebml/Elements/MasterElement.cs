using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MkvTests.Ebml.Elements
{
    public class MasterElement : Element
    {
        protected ulong usedSize { get; set; }
        protected List<Element> children { get; set; }
        public MasterElement(byte[] type) : base(type)
        {
            children = new List<Element>();
            usedSize = 0;
        }

        public MasterElement() : base()
        {
            children = new List<Element>();
            usedSize = 0;
        }

        public Element ReadNextChild(EbmlReader reader)
        {
            if (usedSize >= Size)
            {
                Utility.LogTrace("Can't read any more children.");
                return null;
            }

            Element element = reader.ReadNextElement();
            if(element == null)
            {
                Utility.LogDebug("Reader returned null");
                return null;
            }

            element.Parent = this;

            usedSize += element.TotalSize;

            Utility.LogTrace($"Read element {element.TypeInfo.Name} of size {element.TotalSize}, {Size - usedSize} remaining.");
            return element;
        }

        public override void SkipData(FileStream stream)
        {
            stream.Seek((long)(Size - usedSize), SeekOrigin.Current);
        }

        public new ulong WriteData(FileStream writer)
        {
            ulong length = 0;
            for(int i = 0; i < children.Count; i++)
            {
                Element element = children[i];
                length += element.WriteData(writer);
            }
            return length;
        }

        public void AddChildElement(Element element)
        {
            children.Add(element);
            Size += element.TotalSize;
        }
    }
}
