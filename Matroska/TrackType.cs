using System;
using System.Collections.Generic;
using System.Text;

namespace MkvTests.Matroska
{
    public enum TrackType
    {
        Video = 1,
        Audio = 2,
        Complex = 3,
        Logo = 0x10,
        Subtitle = 0x11,
        Buttons = 0x12,
        Control = 0x20
    }
}
