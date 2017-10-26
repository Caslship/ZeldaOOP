using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZeldaOOP.Core.Scene.Room;

namespace ZeldaOOP.Serialization.Scene.Room
{
    public class RoomSerializer : IZSerializer<RoomModel>
    {
        public async Task<List<byte>> Serialize(RoomModel model, uint offset)
        {
            throw new NotImplementedException();
        }
    }
}
