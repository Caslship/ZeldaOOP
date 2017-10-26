using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using ZeldaOOP.Core.Scene;

namespace ZeldaOOP.Serialization.Scene
{
    public class SceneSerializer : IZSerializer<SceneModel>
    {
        public async Task<List<byte>> Serialize(SceneModel model, uint offset)
        {
            throw new NotImplementedException();
        }
    }
}
