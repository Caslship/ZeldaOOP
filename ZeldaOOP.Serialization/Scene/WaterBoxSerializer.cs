using System;
using System.Collections.Generic;
using System.Text;
using ZeldaOOP.Core.Scene;

namespace ZeldaOOP.Serialization.Scene
{
    public class WaterBoxSerializer : ISerializer<WaterBoxModel>
    {
        private int WaterBoxBinarySize => 3 * sizeof(short) + 5 * sizeof(ushort);

        public List<byte> Serialize(WaterBoxModel waterBox, uint offset)
        {
            var binary = new List<byte>(WaterBoxBinarySize);

            foreach (var dimension in waterBox.Position)
            {
                binary.AddRange(
                    SerializationHelpers.SerializeShort(dimension)
                );
            }

            binary.AddRange(
                SerializationHelpers.SerializeUShort(waterBox.Length)
            );

            binary.AddRange(
                SerializationHelpers.SerializeUShort(waterBox.Width)
            );

            binary.AddRange(
                new List<byte>
                {
                    0x00, 0x00, 0x00, 0x00
                }
            );

            binary.AddRange(
                SerializationHelpers.SerializeUShort(waterBox.Properties)
            );

            return binary;
        }
    }
}
