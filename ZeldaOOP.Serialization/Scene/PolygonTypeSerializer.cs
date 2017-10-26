using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZeldaOOP.Core.Scene;

namespace ZeldaOOP.Serialization.Scene
{
    public class PolygonTypeSerializer : IZSerializer<PolygonTypeModel>
    {
        // http://wiki.cloudmodding.com/oot/Collision_Format_(Scenes)#Polygon_Types
        public async Task<List<byte>> Serialize(PolygonTypeModel model, uint offset)
        {
            var objectSetTriggerEffects = 0x00;
            foreach (var mask in model.ObjectSetTriggerEffects)
            {
                objectSetTriggerEffects |= (byte)mask;
            }

            var horizontalCollisionEffects = 0x0000;
            foreach (var mask in model.HorizontalCollisionEffects)
            {
                horizontalCollisionEffects |= (ushort)mask;
            }

            var verticalCollisionEffects = 0x00;
            foreach (var mask in model.VerticalCollisionEffects)
            {
                verticalCollisionEffects = (byte)mask;
            }

            var interactionCollisionEffects = 0x00;
            foreach (var mask in model.InteractionCollisionEffects)
            {
                interactionCollisionEffects |= (byte)mask;
            }

            return new List<byte>
            {
                (byte)objectSetTriggerEffects,
                (byte)((horizontalCollisionEffects & 0x0FF0) >> 4),
                (byte)(((horizontalCollisionEffects & 0x000F) << 4) | (byte)model.ExitTriggerEffect & 0x0F),
                (byte)model.CameraAngleEffect,
                (byte)verticalCollisionEffects,
                (byte)interactionCollisionEffects,
                (byte)((model.EchoEffect << 4) | (model.AmbientLightEffect & 0x0F)),
                (byte)(((byte)model.TerrainTypeEffect << 4) | ((byte)model.SoundEffect & 0x0F))
            };
        }
    }
}
