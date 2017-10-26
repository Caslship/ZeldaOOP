using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZeldaOOP.Serialization.Scene.Room;
using ZeldaOOP.Core.Scene.Room;
using ZeldaOOP.Core.Drawing;
using ZeldaOOP.Serialization;

namespace ZeldaOOP.Tests.Serialization.Scene.Room
{
    [TestClass]
    public class LightSourceSerializationTests
    {
        [TestMethod]
        public async Task PointLightSourceSerialization()
        {
            var serializer = new LightSourceSerializer();

            var pointLightModel = new LightSourceModel
            {
                LightSourceType = LightSourceModel.LightSourceTypes.Point,
                PositionOrDirection = new Vector3D<short>
                {
                    X = 0x0001,
                    Y = 0x0010,
                    Z = 0x0100
                },
                Color = new ColorRGB<byte>
                {
                    R = 0x01,
                    G = 0x02,
                    B = 0x04
                },
                Attenuation = 0x10
            };

            var expectedPointLightBinary = new List<byte>
            {
                0x02, // Point
                0x00,
                0x00, 0x01, // X
                0x00, 0x10, // Y
                0x01, 0x00, // Z
                0x01, 0x02, 0x04, // RGB
                0x00,
                0x10, // Attenuation
                0x00
            };

            var pointLightBinary = await serializer.Serialize(pointLightModel);

            Assert.IsTrue(pointLightBinary.SequenceEqual(expectedPointLightBinary));
        }

        [TestMethod]
        public async Task DirectionalLightSourceSerialization()
        {
            var serializer = new LightSourceSerializer();

            var directionalLightModel = new LightSourceModel
            {
                LightSourceType = LightSourceModel.LightSourceTypes.Directional,
                PositionOrDirection = new Vector3D<short>
                {
                    X = 0x7E00,
                    Y = 0x7E00,
                    Z = 0x7E00
                },
                Color = new ColorRGB<byte>
                {
                    R = 0x04,
                    G = 0x02,
                    B = 0x01
                },
                Attenuation = 0x01
            };

            var expectedDirectionalLightBinary = new List<byte>
            {
                0x01, // Directional
                0x00,
                0x7E, 0x7E, 0x7E, // XYZ
                0x04, 0x02, 0x01, // RGB
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };

            var directionalLightBinary = await serializer.Serialize(directionalLightModel);

            Assert.IsTrue(directionalLightBinary.SequenceEqual(expectedDirectionalLightBinary));
        }
    }
}
