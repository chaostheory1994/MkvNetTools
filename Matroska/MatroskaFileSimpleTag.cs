using System;
using System.Collections.Generic;
using System.Text;

namespace MkvTests.Matroska
{
    public class MatroskaFileSimpleTag
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public List<MatroskaFileSimpleTag> Children { get; set; }

        public MatroskaFileSimpleTag()
        {
            Children = new List<MatroskaFileSimpleTag>();
        }

        public string ToString(int depth)
        {
            var sb = new StringBuilder();
            var depthIndent = new StringBuilder();
            for (int d = 0; d < depth; d++)
            {
                depthIndent.Append('\t');
            }

            sb.Append($"{depthIndent}SimpleTag\n");
            sb.Append($"{depthIndent}\tName: {Name}\n");
            sb.Append($"{depthIndent}\tValue: {Value}\n");

            depth++;
            foreach (var child in Children)
                sb.Append(child.ToString(depth));

            return sb.ToString();
        }
    }
}
