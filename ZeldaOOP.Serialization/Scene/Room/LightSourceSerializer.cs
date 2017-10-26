using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZeldaOOP.Core.Scene.Room;

namespace ZeldaOOP.Serialization.Scene.Room
{
    public class LightSourceSerializer : IZSerializer<LightSourceModel>
    {
        // http://wiki.cloudmodding.com/oot/Scenes_and_Rooms#Unused_Lighting_list
        public async Task<List<byte>> Serialize(LightSourceModel model, uint offset)
        {
            var binary = new List<byte>
            {
                (byte)(model.LightSourceType),
                0x00
            };

            // TODO: Not very SOLID-like...
            switch (model.LightSourceType)
            {
                case LightSourceModel.LightSourceTypes.Directional:
                    binary.AddRange(
                        EncodeDirectionalLightProperties(model)
                    );
                    break;
                case LightSourceModel.LightSourceTypes.Point:
                    binary.AddRange(
                        EncodePointLightProperties(model)
                    );
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        $"{nameof(LightSourceSerializer)}: {nameof(model.LightSourceType)} is invalid."
                    );
            }

            return binary;
        }

        private List<byte> EncodeDirectionalLightProperties(LightSourceModel model)
        {
            return new List<byte>
            {
                (byte)((model.PositionOrDirection.X & 0xFF00) >> 8),
                (byte)((model.PositionOrDirection.Y & 0xFF00) >> 8),
                (byte)((model.PositionOrDirection.Z & 0xFF00) >> 8),
                model.Color.R,
                model.Color.G,
                model.Color.B,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };
        }

        private List<byte> EncodePointLightProperties(LightSourceModel model)
        {
            return new List<byte>
            {
                (byte)((model.PositionOrDirection.X & 0xFF00) >> 8),
                (byte)(model.PositionOrDirection.X & 0x00FF),
                (byte)((model.PositionOrDirection.Y & 0xFF00) >> 8),
                (byte)(model.PositionOrDirection.Y & 0x00FF),
                (byte)((model.PositionOrDirection.Z & 0xFF00) >> 8),
                (byte)(model.PositionOrDirection.Z & 0x00FF),
                model.Color.R,
                model.Color.G,
                model.Color.B,
                0x00,
                model.Attenuation,
                0x00
            };
        }
    }
}
