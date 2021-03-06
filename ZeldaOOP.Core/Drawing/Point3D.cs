﻿using System;
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

        public IEnumerator<T> GetEnumerator()
        {
            yield return X;
            yield return Y;
            yield return Z;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public static Vector3D<T> operator -(Point3D<T> a, Point3D<T> b)
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
    }
}
