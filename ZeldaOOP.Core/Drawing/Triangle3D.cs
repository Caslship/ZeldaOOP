using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ZeldaOOP.Core.Drawing
{
    public class Triangle3D<T> : IEnumerable<Point3D<T>>
    {
        public Point3D<T> A { get; set; }
        public Point3D<T> B { get; set; }
        public Point3D<T> C { get; set; }

        public IEnumerator<Point3D<T>> GetEnumerator()
        {
            yield return A;
            yield return B;
            yield return C;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public Vector3D<T> Normal
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public T DistanceFromOrigin
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
