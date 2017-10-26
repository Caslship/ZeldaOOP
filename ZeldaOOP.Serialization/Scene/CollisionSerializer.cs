using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZeldaOOP.Core.Scene;
using ZeldaOOP.Core.Drawing;

namespace ZeldaOOP.Serialization.Scene
{
    public class CollisionSerializer : IZSerializer<CollisionModel>
    {
        public class TriangleModel
        {
            public ushort PolygonTypeIndex { get; set; }
            public ushort VertexAIndex { get; set; }
            public ushort VertexBIndex { get; set; }
            public ushort VertexCIndex { get; set; }
            public Vector3D<short> Normal { get; set; }
            public ushort DistanceFromOrigin { get; set; }
        }

        private IZSerializer<PolygonTypeModel> PolygonTypeSerializer { get; }

        public CollisionSerializer(IZSerializer<PolygonTypeModel> polygonTypeSerializer)
        {
            PolygonTypeSerializer = polygonTypeSerializer;
        }

        public async Task<List<byte>> Serialize(CollisionModel model, uint offset)
        {
            var binary = new List<byte>();

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

            foreach (var dimension in minCoordinate)
            {
                binary.AddRange(
                    SerializationHelpers.SerializeInt16(dimension)
                );
            }

            foreach (var dimension in maxCoordinate)
            {
                binary.AddRange(
                    SerializationHelpers.SerializeInt16(dimension)
                );
            }

            binary.AddRange(
                SerializationHelpers.SerializeInt16((short)vertices.Count)
            );

            // TODO: Finish this

            return binary;
        }
    }
}
