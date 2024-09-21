using System;

namespace NKG.NetWork
{
    public class ET_CSPacketHeader : PacketHeaderBase
    {
        public override PacketType PacketType => PacketType.ClientToServer;
    }

}
