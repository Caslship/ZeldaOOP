using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ZeldaOOP.Core.Drawing
{
    public class ColorRGB<T> : IEnumerable<T>
    {
        public T R { get; set; }
        public T G { get; set; }
        public T B { get; set; }

        public virtual IEnumerator<T> GetEnumerator()
        {
            yield return R;
            yield return G;
            yield return B;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
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
