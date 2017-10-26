using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaOOP.Fetching.BinaryResource
{
    public abstract class BinaryResourceFetcherBase : IZFetcher<List<byte>>
    {
        public abstract Task<List<byte>> Fetch(uint resourceAddress);
    }
}
