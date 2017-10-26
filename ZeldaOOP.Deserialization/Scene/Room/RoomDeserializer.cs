using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZeldaOOP.Fetching;
using ZeldaOOP.Core.Scene.Room;

namespace ZeldaOOP.Deserialization.Scene.Room
{
    public class RoomDeserializer : IZDeserializer<RoomModel>
    {
        private IZFetcher<List<byte>> ExternalResourceFetcher { get; }

        public RoomDeserializer(IZFetcher<List<byte>> externalResourceFetcher)
        {
            ExternalResourceFetcher = externalResourceFetcher;
        }

        public async Task<RoomModel> Deserialize(List<byte> data)
        {
            throw new NotImplementedException();
        }
    }
}
