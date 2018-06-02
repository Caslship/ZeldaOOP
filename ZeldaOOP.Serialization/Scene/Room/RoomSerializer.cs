using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZeldaOOP.Core.Scene.Room;

namespace ZeldaOOP.Serialization.Scene.Room
{
    public class RoomSerializer : ISerializer<RoomModel>
    {
        public List<byte> Serialize(RoomModel model, uint offset)
        {
            throw new NotImplementedException();
        }
    }
}
