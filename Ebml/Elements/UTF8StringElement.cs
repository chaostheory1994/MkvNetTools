using System;
using System.Collections.Generic;
using System.Text;

namespace MkvTests.Ebml.Elements
{
    public class UTF8StringElement : StringElement
    {
        public UTF8StringElement() : base(Encoding.UTF8) { }
    }
}
