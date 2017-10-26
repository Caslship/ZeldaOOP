using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ZeldaOOP.Core.Drawing
{
    public class ColorRGB<T> : Channel3D<T>
    {
        public T R
        {
            get { return base.X; }
            set { base.X = value; }
        }
        public T G
        {
            get { return base.Y; }
            set { base.Y = value; }
        }
        public T B
        {
            get { return base.Z; }
            set { base.Z = value; }
        }
    }

    public class ColorRGBA<T> : ColorRGB<T>
    {
        public T A { get; set; }

        public override IEnumerator<T> GetEnumerator()
        {
            var baseEnumerator = base.GetEnumerator();
            while (baseEnumerator.MoveNext())
            {
                yield return baseEnumerator.Current;
            }

            yield return A;
        }
    }
}
