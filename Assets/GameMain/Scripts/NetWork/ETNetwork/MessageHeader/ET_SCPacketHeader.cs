using System;

namespace NKG.NetWork { 
    public class ET_SCPacketHeader : PacketHeaderBase
    {

        public override PacketType PacketType => PacketType.ServerToClient;
    }

}

