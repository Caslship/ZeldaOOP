using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZeldaOOP.Core.Scene;

namespace ZeldaOOP.Serialization.Scene
{
    public class SkyboxCameraSerializer : ISerializer<SkyboxCameraModel>
    {
        private int SkyboxCameraBinarySize => 8 + sizeof(short) * 7 + 2;

        // https://wiki.cloudmodding.com/oot/Collision_Format_(Scenes)#Camera_Data
        public List<byte> Serialize(SkyboxCameraModel model, uint offset)
        {
            var binary = new List<byte>(SkyboxCameraBinarySize);

            binary.AddRange(
                SerializationHelpers.SerializeUShort(model.CameraType)
            );

            binary.AddRange(
                new List<byte>
                {
                    0x00, 0x01
                }
            );

            var dataOffset = offset + 4;
            binary.AddRange(SerializationHelpers.SerializeUInt(dataOffset));
            
            foreach (var dimension in model.Position)
            {
                binary.AddRange(SerializationHelpers.SerializeShort(dimension));
            }

            foreach (var dimension in model.Rotation)
            {
                binary.AddRange(SerializationHelpers.SerializeShort(dimension));
            }

            binary.AddRange(SerializationHelpers.SerializeShort(model.Zoom));

            binary.AddRange(
                new List<byte>
                {
                    0xFF, 0xFF
                }
            );

            return binary;
        }
    }
}
