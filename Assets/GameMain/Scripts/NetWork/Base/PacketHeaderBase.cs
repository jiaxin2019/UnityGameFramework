//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2019 Jiang Yin. All rights reserved.
// Homepage: http://gameframework.cn/
// Feedback: mailto:jiangyin@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using GameFramework.Network;

namespace NKG.NetWork
{
    public abstract class PacketHeaderBase : IPacketHeader, IReference
    {
        public abstract PacketType PacketType
        {
            get;
        }

        public ushort Id { get; set; }

        public int PacketLength
        {
            get;
            set;
        }

        public bool IsValid
        {
            get
            {
                return PacketType != PacketType.Undefined && Id > 0 && PacketLength >= 0;
            }
        }

        public void Clear()
        {
            Id = 0;
            PacketLength = 0;
            ClientSeqId = 0;
            MessageId = 0;
            ServiceId = 0;
            ClientSendTime = 0;
            Version = 0;
            ServerId = 0;
            Compress = 0;
        }
        public int ClientSeqId { get; set; }
        public int MessageId { get; set; }
        public ushort ServiceId { get; set; }
        public long ClientSendTime { get; set; }
        public int Version { get; set; }
        public int ServerId { get; set; }
        public ushort Compress { get; set; }
       
    }
}
