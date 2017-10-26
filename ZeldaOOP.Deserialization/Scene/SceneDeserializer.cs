using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZeldaOOP.Core.Scene;
using ZeldaOOP.Fetching;
using ZeldaOOP.Deserialization.Scene.Room;

namespace ZeldaOOP.Deserialization.Scene
{
    public class SceneDeserializer : IZDeserializer<SceneModel>
    {
        private IZFetcher<List<byte>> ExternalResourceFetcher { get; }

        public SceneDeserializer(IZFetcher<List<byte>> externalResourceFetcher)
        {
            ExternalResourceFetcher = externalResourceFetcher;
        }

        public async Task<SceneModel> Deserialize(List<byte> data)
        {
            throw new NotImplementedException();
        }
    }
}
