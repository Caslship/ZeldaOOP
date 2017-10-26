using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ZeldaOOP.Serialization
{
    public interface IZSerializer<T>
    {
        Task<List<byte>> Serialize(T model, uint offset);
    }

    public static class ZSerializerExtensions
    {
        public static async Task<List<byte>> Serialize<T>(this IZSerializer<T> serializer, T model)
        {
            return await serializer.Serialize(model, 0x0);
        }
    }
}
