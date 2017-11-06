using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
    }

    // Helpful aliases
    public class Rotation3D<T> : Point3D<T> { }

    public class Vector3D<T> : Point3D<T>
    {
        public Vector3D<T> Scale(T scalar)
        {
            var a = this;
            return a * scalar;
        }

        public T Dot(Vector3D<T> b)
        {
            var a = this;
            return a * b;
        }

        public T Magnitude()
        {
            var a = this;
            dynamic squaredMagnitude = a * a;
            dynamic magnitude = Math.Sqrt(squaredMagnitude);

            return (T)magnitude;
        }

        public Vector3D<T> Normalize()
        {
            var a = this;
            return a / a.Magnitude();
        }

        public Vector3D<T> Cross(Vector3D<T> b)
        {
            var a = this;
            return a ^ b;
        }

        // https://blogs.msdn.microsoft.com/lucabol/2009/02/05/simulating-inumeric-with-dynamic-in-c-4-0/
        public static Vector3D<T> operator *(Vector3D<T> a, T scalar)
        {
            dynamic ax = a.X;
            dynamic ay = a.Y;
            dynamic az = a.Z;

            return new Vector3D<T>
            {
                X = ax * scalar,
                Y = ay * scalar,
                Z = az * scalar
            };
        }

        // TODO: Not sure how I feel about explicit casting to T...
        // Maybe I should force people to make due just using multiplication
        public static Vector3D<T> operator /(Vector3D<T> a, T scalar)
        {
            var inverseScalar = (T)(1 / (dynamic)scalar);
            return a * inverseScalar;
        }

        public static Vector3D<T> operator -(Vector3D<T> a, Vector3D<T> b)
        {
            dynamic ax = a.X;
            dynamic ay = a.Y;
            dynamic az = a.Z;

            dynamic bx = b.X;
            dynamic by = b.Y;
            dynamic bz = b.Z;

            return new Vector3D<T>
            {
                X = ax - bx,
                Y = ay - by,
                Z = az - bz
            };
        }

        public static T operator *(Vector3D<T> a, Vector3D<T> b)
        {
            dynamic ax = a.X;
            dynamic ay = a.Y;
            dynamic az = a.Z;

            dynamic bx = b.X;
            dynamic by = b.Y;
            dynamic bz = b.Z;

            return (ax * bx) + (ay * by) + (az * bz);
        }

        // http://tutorial.math.lamar.edu/Classes/CalcII/CrossProduct.aspx
        public static Vector3D<T> operator ^(Vector3D<T> a, Vector3D<T> b)
        {
            dynamic ax = a.X;
            dynamic ay = a.Y;
            dynamic az = a.Z;

            dynamic bx = b.X;
            dynamic by = b.Y;
            dynamic bz = b.Z;

            return new Vector3D<T>
            {
                X = (ay * bz) - (az * by),
                Y = (az * bx) - (ax * bz),
                Z = (ax * by) - (ay * bx)
            };
        }
    }

    public class Channel3D<T> : Point3D<T> { }
}
