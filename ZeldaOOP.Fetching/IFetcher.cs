using System;
using System.Threading.Tasks;

namespace ZeldaOOP.Fetching
{
    public interface IFetcher<T>
    {
        Task<T> Fetch(uint resourceAddress);
    }
}
