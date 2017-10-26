using System;
using System.Collections.Generic;
using System.Text;
using ZeldaOOP.Core.Drawing;

namespace ZeldaOOP.Core.Scene
{
    public class ActorEntryModel
    {
        public ushort ActorNumber { get; set; }
        public Point3D<short> Position { get; set; }
        public Rotation3D<short> Rotation { get; set; }
        public ushort Variable { get; set; }
    }
}
