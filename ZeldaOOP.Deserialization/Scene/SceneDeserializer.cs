using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZeldaOOP.Core.Scene;
using ZeldaOOP.Fetching;
using ZeldaOOP.Deserialization.Scene.Room;

namespace ZeldaOOP.Deserialization.Scene
{
    public class SceneDeserializer : IDeserializer<SceneModel>
    {
        private IFetcher<List<byte>> ExternalResourceFetcher { get; }

        public SceneDeserializer(IFetcher<List<byte>> externalResourceFetcher)
        {
            ExternalResourceFetcher = externalResourceFetcher;
        }

        public SceneModel Deserialize(List<byte> data)
        {
            throw new NotImplementedException();
        }
    }
}
