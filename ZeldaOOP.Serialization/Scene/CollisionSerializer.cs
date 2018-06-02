using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZeldaOOP.Core.Scene;
using ZeldaOOP.Core.Drawing;

namespace ZeldaOOP.Serialization.Scene
{
    public class CollisionSerializer : ISerializer<CollisionModel>
    {
        private ISerializer<PolygonTypeModel> PolygonTypeSerializer { get; }
        private ISerializer<SkyboxCameraModel> SkyboxCameraSerializer { get; }
        private ISerializer<WaterBoxModel> WaterBoxSerializer { get; }
        private ISerializer<TriangleModel> CollisionTriangleSerializer { get; }
        private int CollisionHeaderBinarySize => 0x2C;

        public CollisionSerializer(ISerializer<PolygonTypeModel> polygonTypeSerializer, ISerializer<SkyboxCameraModel> skyboxCameraSerializer, ISerializer<WaterBoxModel> waterBoxSerializer)
        {
            PolygonTypeSerializer = polygonTypeSerializer;
            SkyboxCameraSerializer = skyboxCameraSerializer;
            WaterBoxSerializer = waterBoxSerializer;
            CollisionTriangleSerializer = new TriangleSerializer();
        }

        // https://wiki.cloudmodding.com/oot/Collision_Format_(Scenes)
        public List<byte> Serialize(CollisionModel model, uint offset)
        {
            var minCoordinate = new Point3D<short>
            {
                X = short.MaxValue,
                Y = short.MaxValue,
                Z = short.MaxValue
            };

            var maxCoordinate = new Point3D<short>
            {
                X = short.MinValue,
                Y = short.MinValue,
                Z = short.MinValue
            };

            // https://stackoverflow.com/questions/38788135/set-vs-list-when-need-both-unique-elements-and-access-by-index
            // We need a unique list of vertices for indexing...
            // Sacrifice memory and take advantage of the properties of HashSet and List
            var vertexSet = new HashSet<Point3D<short>>();
            var vertices = new List<Point3D<short>>();

            // We need a unique list of polygon types for indexing... same deal
            var polygonTypeSet = new HashSet<PolygonTypeModel>();
            var polygonTypes = new List<PolygonTypeModel>();

            // No need for indexing of unqiue list of triangles
            var triangleSet = new HashSet<TriangleModel>();

            // Build up polygon collision data
            foreach (var polygon in model.Polygons)
            {
                var polygonTypeIndex = -1;
                if (polygonTypeSet.Add(polygon.PolygonType))
                {
                    polygonTypeIndex = polygonTypes.Count;
                    polygonTypes.Add(polygon.PolygonType);
                }
                else
                {
                    polygonTypeIndex = polygonTypes.IndexOf(polygon.PolygonType);
                }

                var vertexIndices = new List<ushort>(3);
                var triangle = polygon.Triangle;

                foreach (var vertex in triangle)
                {
                    minCoordinate.X = Math.Min(vertex.X, minCoordinate.X);
                    minCoordinate.Y = Math.Min(vertex.Y, minCoordinate.Y);
                    minCoordinate.Z = Math.Min(vertex.Z, minCoordinate.Z);

                    maxCoordinate.X = Math.Max(vertex.X, maxCoordinate.X);
                    maxCoordinate.Y = Math.Max(vertex.Y, maxCoordinate.Y);
                    maxCoordinate.Z = Math.Max(vertex.Z, maxCoordinate.Z);

                    var vertexIndex = -1;
                    if (vertexSet.Add(vertex))
                    {
                        vertexIndex = vertices.Count;
                        vertices.Add(vertex);
                    }
                    else
                    {
                        vertexIndex = vertices.IndexOf(vertex);
                    }

                    vertexIndices.Add((ushort)vertexIndex);
                }

                triangleSet.Add(
                    new TriangleModel
                    {
                        PolygonTypeIndex = (ushort)polygonTypeIndex,
                        VertexAIndex = vertexIndices[0],
                        VertexBIndex = vertexIndices[1],
                        VertexCIndex = vertexIndices[2],
                        Normal = triangle.Normal,
                        DistanceFromOrigin = (ushort)triangle.DistanceFromOrigin
                    }
                );
            }

            // Serialize vertex array
            var verticesBinarySize = ComputeVerticesBinarySize(vertices);
            var verticesBinary = new List<byte>(verticesBinarySize);
            var verticesBinaryOffset = offset + (uint)CollisionHeaderBinarySize;
            foreach (var vertex in vertices)
            {
                foreach (var dimension in vertex)
                {
                    verticesBinary.AddRange(
                        SerializationHelpers.SerializeShort(dimension)
                    );
                }
            }

            // Serialize polygon array
            var trianglesBinarySize = ComputeTrianglesBinarySize(triangleSet);
            var trianglesBinary = new List<byte>(trianglesBinarySize);
            var trianglesBinaryOffset = verticesBinaryOffset + (uint)verticesBinarySize;
            foreach (var triangle in triangleSet)
            {
                trianglesBinary.AddRange(
                    CollisionTriangleSerializer.Serialize(triangle)
                );
            }

            // Serialize polygon type array
            var polygonTypesBinarySize = ComputePolygonTypesBinarySize(polygonTypes);
            var polygonTypesBinary = new List<byte>(polygonTypesBinarySize);
            var polygonTypesBinaryOffset = trianglesBinaryOffset + (uint)trianglesBinarySize;
            foreach (var polygonType in polygonTypes)
            {
                polygonTypesBinary.AddRange(
                    PolygonTypeSerializer.Serialize(polygonType)
                );
            }

            // Serialize skybox camera array
            var skyboxCamerasBinarySize = ComputeSkyboxCamerasBinarySize(model.SkyboxCameras);
            var skyboxCamerasBinary = new List<byte>(skyboxCamerasBinarySize);
            var skyboxCamerasBinaryOffset = polygonTypesBinaryOffset + (uint)polygonTypesBinarySize;
            var currentSkyboxCameraBinaryOffset = skyboxCamerasBinaryOffset;
            foreach (var skyboxCamera in model.SkyboxCameras)
            {
                var skyboxCameraBinary = SkyboxCameraSerializer.Serialize(skyboxCamera, currentSkyboxCameraBinaryOffset);
                skyboxCamerasBinary.AddRange(skyboxCameraBinary);

                currentSkyboxCameraBinaryOffset += (uint)skyboxCameraBinary.Count;
            }

            // Serialize water box array
            var waterBoxesBinarySize = ComputeWaterBoxesBinarySize(model.WaterBoxes);
            var waterBoxesBinary = new List<byte>(waterBoxesBinarySize);
            var waterBoxesBinaryOffset = skyboxCamerasBinaryOffset + (uint)skyboxCamerasBinarySize;
            foreach (var waterBox in model.WaterBoxes)
            {
                waterBoxesBinary.AddRange(
                    WaterBoxSerializer.Serialize(waterBox)
                );
            }

            var binarySize = CollisionHeaderBinarySize
                + verticesBinarySize
                + trianglesBinarySize
                + polygonTypesBinarySize
                + skyboxCamerasBinarySize
                + waterBoxesBinarySize;

            var binary = new List<byte>(binarySize);

            // Begin serializing collision header
            // Minimum XYZ coordinate
            foreach (var dimension in minCoordinate)
            {
                binary.AddRange(
                    SerializationHelpers.SerializeShort(dimension)
                );
            }

            // Maximum XYZ coordinate
            foreach (var dimension in maxCoordinate)
            {
                binary.AddRange(
                    SerializationHelpers.SerializeShort(dimension)
                );
            }

            // Vertex count
            binary.AddRange(
                SerializationHelpers.SerializeUShort((ushort)vertices.Count)
            );

            binary.AddRange(
                new List<byte>
                {
                    0x00, 0x00
                }
            );

            // Vertex array offset
            binary.AddRange(
                SerializationHelpers.SerializeUInt(verticesBinaryOffset)
            );

            // Polygon count
            binary.AddRange(
                SerializationHelpers.SerializeUShort((ushort)triangleSet.Count)
            );

            binary.AddRange(
                new List<byte>
                {
                    0x00, 0x00
                }
            );

            // Polygon array offset
            binary.AddRange(
                SerializationHelpers.SerializeUInt(trianglesBinaryOffset)
            );

            // Polygon type array offset
            binary.AddRange(
                SerializationHelpers.SerializeUInt(polygonTypesBinaryOffset)
            );

            // Skybox camera array offset
            binary.AddRange(
                SerializationHelpers.SerializeUInt(skyboxCamerasBinaryOffset)
            );

            // Water box count
            binary.AddRange(
                SerializationHelpers.SerializeUShort((ushort)model.WaterBoxes.Count)
            );

            binary.AddRange(
                new List<byte>
                {
                    0x00, 0x00
                }
            );

            // Water box array offset
            binary.AddRange(
                SerializationHelpers.SerializeUInt(waterBoxesBinaryOffset)
            );

            // Add all binaries after collision header (order is important here)
            binary.AddRange(verticesBinary);
            binary.AddRange(trianglesBinary);
            binary.AddRange(polygonTypesBinary);
            binary.AddRange(skyboxCamerasBinary);
            binary.AddRange(waterBoxesBinary);

            return binary;
        }

        private int ComputeVerticesBinarySize(List<Point3D<short>> vertices)
        {
            return vertices.Count * sizeof(short) * 3;
        }

        private int ComputeTrianglesBinarySize(HashSet<TriangleModel> triangleSet)
        {
            return triangleSet.Count * (sizeof(ushort) * 5 + sizeof(short) * 3);
        }

        private int ComputePolygonTypesBinarySize(List<PolygonTypeModel> polygonTypes)
        {
            return polygonTypes.Count * sizeof(uint) * 2;
        }

        private int ComputeSkyboxCamerasBinarySize(List<SkyboxCameraModel> skyboxCameras)
        {
            return skyboxCameras.Count * (8 + sizeof(short) * 7 + 2);
        }

        private int ComputeWaterBoxesBinarySize(List<WaterBoxModel> waterBoxes)
        {
            return waterBoxes.Count * (3 * sizeof(short) + 5 * sizeof(ushort));
        }

        public class TriangleModel
        {
            public ushort PolygonTypeIndex { get; set; }
            public ushort VertexAIndex { get; set; }
            public ushort VertexBIndex { get; set; }
            public ushort VertexCIndex { get; set; }
            public Vector3D<short> Normal { get; set; }
            public ushort DistanceFromOrigin { get; set; }
        }

        public class TriangleSerializer : ISerializer<TriangleModel>
        {
            private int TriangleBinarySize => sizeof(ushort) * 5 + sizeof(short) * 3;

            public List<byte> Serialize(TriangleModel triangle, uint offset)
            {
                var binary = new List<byte>(TriangleBinarySize);

                binary.AddRange(
                    SerializationHelpers.SerializeUShort(triangle.PolygonTypeIndex)
                );

                binary.AddRange(
                    SerializationHelpers.SerializeUShort(triangle.VertexAIndex)
                );

                binary.AddRange(
                    SerializationHelpers.SerializeUShort(triangle.VertexBIndex)
                );

                binary.AddRange(
                    SerializationHelpers.SerializeUShort(triangle.VertexCIndex)
                );

                foreach (var dimension in triangle.Normal)
                {
                    binary.AddRange(
                        SerializationHelpers.SerializeShort(dimension)
                    );
                }

                binary.AddRange(
                    SerializationHelpers.SerializeUShort(triangle.DistanceFromOrigin)
                );

                return binary;
            }
        }
    }
}
