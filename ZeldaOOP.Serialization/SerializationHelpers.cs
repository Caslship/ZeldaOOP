using System;
using System.Collections.Generic;
using System.Text;

namespace ZeldaOOP.Serialization
{
    public static class SerializationHelpers
    {
        public static List<byte> SerializeInt16(short value)
        {
            return new List<byte>
            {
                (byte)((value & 0xFF00) >> 16),
                (byte)(value & 0x00FF)
            };
        }
    }
}
