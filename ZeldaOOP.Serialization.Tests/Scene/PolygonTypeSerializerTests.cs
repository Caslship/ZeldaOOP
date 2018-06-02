using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZeldaOOP.Serialization.Scene;
using ZeldaOOP.Serialization;
using ZeldaOOP.Core.Scene;

namespace ZeldaOOP.Tests.Serialization.Scene
{
    [TestClass]
    public class PolygonTypeSerializerTests
    {
        [TestMethod]
        public void PolygonTypeSerialization()
        {
            var serializer = new PolygonTypeSerializer();

            var polygonTypeModel = new PolygonTypeModel
            {
                ObjectSetTriggerEffects = new List<PolygonTypeModel.ObjectSetTriggerEffectMask>
                {
                    PolygonTypeModel.ObjectSetTriggerEffectMask.Set4
                },
                HorizontalCollisionEffects = new List<PolygonTypeModel.HorizontalCollisionEffectMask>
                {
                    PolygonTypeModel.HorizontalCollisionEffectMask.Climbable,
                    PolygonTypeModel.HorizontalCollisionEffectMask.Crawlable
                },
                ExitTriggerEffect = PolygonTypeModel.ExitTriggerEffects.Index2,
                CameraAngleEffect = PolygonTypeModel.CameraAngleEffects.Default,
                VerticalCollisionEffects = new List<PolygonTypeModel.VerticalCollisionEffectMask>
                {
                    PolygonTypeModel.VerticalCollisionEffectMask.Harmful
                },
                InteractionCollisionEffects = new List<PolygonTypeModel.InteractionCollisionEffectMask>
                {
                    PolygonTypeModel.InteractionCollisionEffectMask.Hookshotable
                },
                EchoEffect = 0x01,
                AmbientLightEffect = 0x00,
                TerrainTypeEffect = PolygonTypeModel.TerrainTypeEffects.HighAngle,
                SoundEffect = PolygonTypeModel.SoundEffects.LooseEarth
            };

            var expectedBinary = new List<byte>
            {
                0x40,
                0x00,
                0x52,
                0x00,
                0x01,
                0x04,
                0x10,
                0x1D
            };

            var polygonTypeBinary = serializer.Serialize(polygonTypeModel);

            Assert.IsTrue(polygonTypeBinary.SequenceEqual(expectedBinary));
        }
    }
}
