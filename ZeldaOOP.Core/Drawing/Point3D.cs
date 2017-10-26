using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ZeldaOOP.Core.Drawing
{
    public class Point3D<T> : IEnumerable<T>
    {
        public T X { get; set; }
        public T Y { get; set; }
        public T Z { get; set; }

        public virtual IEnumerator<T> GetEnumerator()
        {
            yield return X;
            yield return Y;
            yield return Z;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public static Point3D<T> operator- (Point3D<T> a, Point3D<T> b)
        {
            throw new NotImplementedException();
        }
    }

    // Helpful aliases
    public class Rotation3D<T> : Point3D<T> { }

    public class Vector3D<T> : Point3D<T>
    {
        public Vector3D<T> Normalize()
        {
            throw new NotImplementedException();
        }

        public T Dot(Vector3D<T> other)
        {
            throw new NotImplementedException();
        }

        public Vector3D<T> Cross(Vector3D<T> other)
        {
            throw new NotImplementedException();
        }
    }

    public class Channel3D<T> : Point3D<T> { }
}
