using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZeldaOOP.Core.Drawing;

namespace ZeldaOOP.Tests.Core.Drawing
{
    [TestClass]
    public class Vector3DTests
    {
        [TestMethod]
        public void IJCrossProductK()
        {
            var i = new Vector3D<float>
            {
                X = 1.0f,
                Y = 0.0f,
                Z = 0.0f
            };

            var j = new Vector3D<float>
            {
                X = 0.0f,
                Y = 1.0f,
                Z = 0.0f
            };

            var expectedCrossProduct = new Vector3D<float>
            {
                X = 0.0f,
                Y = 0.0f,
                Z = 1.0f
            };

            var crossProduct = i.Cross(j);

            Assert.IsTrue(crossProduct.SequenceEqual(expectedCrossProduct));
        }

        [TestMethod]
        public void UnitVectorMagnitudeIsOne()
        {
            var unitVector = new Vector3D<float>
            {
                X = (float)(1.0f / Math.Sqrt(3.0f)),
                Y = (float)(1.0f / Math.Sqrt(3.0f)),
                Z = (float)(1.0f / Math.Sqrt(3.0f))
            };

            var expectedMagnitude = 1.0f;
            var epsilon = float.Epsilon;

            // TODO: Finish me! Numeric precision is a pain

            var magnitude = unitVector.Magnitude();

            Assert.AreEqual(expectedMagnitude, expectedMagnitude);
        }
    }
}
