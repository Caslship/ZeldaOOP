using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using ZeldaOOP.Core.Scene;

namespace ZeldaOOP.Serialization.Scene
{
    public class SceneSerializer : ISerializer<SceneModel>
    {
        public List<byte> Serialize(SceneModel model, uint offset)
        {
            throw new NotImplementedException();
        }
    }
}
