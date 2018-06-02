using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ZeldaOOP.Serialization
{
    public interface ISerializer<T>
    {
        List<byte> Serialize(T model, uint offset);
    }

    public static class SerializerExtensions
    {
        public static List<byte> Serialize<T>(this ISerializer<T> serializer, T model)
        {
            return serializer.Serialize(model, 0x0);
        }
    }

    public static class SerializationHelpers
    {
        public static List<byte> SerializeUShort(ushort model)
        {
            return new List<byte>
            {
                (byte)((model & 0xFF00) >> 8),
                (byte)(model & 0x00FF)
            };
        }

        public static List<byte> SerializeShort(short model)
        {
            return SerializeUShort((ushort)(model));
        }

        public static List<byte> SerializeUInt(uint model)
        {
            var binary = SerializeUShort((ushort)((model & 0xFFFF0000) >> 16));
            binary.AddRange(SerializeUShort((ushort)(model & 0x0000FFFF)));

            return binary;
        }

        public static List<byte> SerializeInt(int model)
        {
            return SerializeUInt((uint)(model));
        }
    }
}
