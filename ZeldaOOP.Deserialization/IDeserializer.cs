using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ZeldaOOP.Deserialization
{
    public interface IDeserializer<T>
    {
        T Deserialize(List<byte> data);
    }
}
