using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ZeldaOOP.Core.Drawing
{
    public class CoordinateUV<T> : IEnumerable<T>
    {
        public T U { get; set; }
        public T V { get; set; }

        public virtual IEnumerator<T> GetEnumerator()
        {
            yield return U;
            yield return V;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public class CoordinateUVW<T> : CoordinateUV<T>
    {
        public T W { get; set; }

        public override IEnumerator<T> GetEnumerator()
        {
            var baseEnumerator = base.GetEnumerator();
            while (baseEnumerator.MoveNext())
            {
                yield return baseEnumerator.Current;
            }

            yield return W;
        }
    }
}
