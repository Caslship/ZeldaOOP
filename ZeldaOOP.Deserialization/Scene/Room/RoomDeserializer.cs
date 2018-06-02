using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZeldaOOP.Fetching;
using ZeldaOOP.Core.Scene.Room;

namespace ZeldaOOP.Deserialization.Scene.Room
{
    public class RoomDeserializer : IDeserializer<RoomModel>
    {
        private IFetcher<List<byte>> ExternalResourceFetcher { get; }

        public RoomDeserializer(IFetcher<List<byte>> externalResourceFetcher)
        {
            ExternalResourceFetcher = externalResourceFetcher;
        }

        public RoomModel Deserialize(List<byte> data)
        {
            throw new NotImplementedException();
        }
    }
}
