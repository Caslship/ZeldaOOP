using System;
using System.Threading.Tasks;

namespace ZeldaOOP.Fetching
{
    public interface IZFetcher<T>
    {
        Task<T> Fetch(uint resourceAddress);
    }
}
