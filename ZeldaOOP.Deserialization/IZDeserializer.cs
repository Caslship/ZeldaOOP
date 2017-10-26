using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ZeldaOOP.Deserialization
{
    public interface IZDeserializer<T>
    {
        Task<T> Deserialize(List<byte> data);
    }
}
