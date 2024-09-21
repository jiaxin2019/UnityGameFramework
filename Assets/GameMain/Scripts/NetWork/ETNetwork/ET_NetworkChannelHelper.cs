using GameFramework;
using GameFramework.Event;
using GameFramework.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.IO;
using Xiu;
using Log = UnityGameFramework.Runtime.Log;
using StarForce;

namespace NKG.NetWork
{
   public class ET_NetworkChannelHelper : INetworkChannelHelper
    {
        private readonly Dictionary<int, Type> m_ServerToClientPacketTypes = new Dictionary<int, Type>();

        private List<byte[]> byteses = new List<byte[]>();
            // {new byte[ETPackets.ET_PacketSizeLength],  new byte[2]};

        public RecyclableMemoryStreamManager MemoryStreamManager = new RecyclableMemoryStreamManager();

        private INetworkChannel m_NetworkChannel = null;

        private MemoryStream memoryStream = new MemoryStream(1024 * 8);
        /// <summary>
        /// 加解密字节流字段
        /// </summary>
        private MemoryStream enMemoryStream = new MemoryStream(1024 * 8);

        private bool sendHeartBeat = true;
        /// <summary>
        /// 错过心跳的次数
        /// </summary>
        private int missCount = 3;
        
        /// <summary>
        /// 消息头长度
        /// </summary>
        public int PacketHeaderLength
        {
            get { return 30; }
        }

        /// <summary>
        /// 准备进行连接。
        /// </summary>
        public void PrepareForConnecting()
        {
            m_NetworkChannel.Socket.ReceiveBufferSize = 1024 * 64;
            m_NetworkChannel.Socket.SendBufferSize = 1024 * 64;
        }

        public void Initialize(INetworkChannel networkChannel)
        {
            m_NetworkChannel = networkChannel;

            // 反射注册包和包处理函数。
            Type packetBaseType = typeof(SCPacketBase);
            Type packetHandlerBaseType = typeof(PacketHandlerBase);
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                if (!types[i].IsClass || types[i].IsAbstract)
                {
                    continue;
                }

                if (types[i].BaseType == packetBaseType)
                {
                    PacketBase packetBase = (PacketBase) Activator.CreateInstance(types[i]);
                    Type packetType = GetServerToClientPacketType(packetBase.Id);
                    if (packetType != null)
                    {
                        Log.Warning("Already exist packet type '{0}', check '{1}' or '{2}'?.", packetBase.Id.ToString(),
                            packetType.Name, packetBase.GetType().Name);
                        continue;
                    }

                    m_ServerToClientPacketTypes.Add(packetBase.Id, types[i]);
                }
                else if (types[i].BaseType == packetHandlerBaseType)
                {
                    IPacketHandler packetHandler = (IPacketHandler) Activator.CreateInstance(types[i]);
                    m_NetworkChannel.RegisterHandler(packetHandler);
                }
            }

            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.NetworkConnectedEventArgs.EventId, OnNetworkConnected);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.NetworkClosedEventArgs.EventId, OnNetworkClosed);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.NetworkMissHeartBeatEventArgs.EventId,
                OnNetworkMissHeartBeat);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.NetworkErrorEventArgs.EventId, OnNetworkError);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.NetworkCustomErrorEventArgs.EventId,
                OnNetworkCustomError);
            GameEntry.Event.Subscribe(ClientHotfixOpcode.Heart,
                OnHeartBack);
        }

        private void OnHeartBack(object sender, GameEventArgs e)
        {
            HeartbeatMsgResponseBody data = sender as HeartbeatMsgResponseBody;
            long time = data.ServerTime;
            sendHeartBeat = false;
        }

        public Packet DeserializePacket(IPacketHeader packetHeader, Stream source, out object customErrorData)
        {
            customErrorData = null;
            ET_SCPacketHeader header = packetHeader as ET_SCPacketHeader;
            if (header == null)
            {
                Log.Warning("Packet header is invalid.");
                return null;
            }

            Packet packet = null;
            if (header.IsValid)
            {
                Type packetType = GetServerToClientPacketType(header.Id);
                if (packetType != null && source is MemoryStream)
                {
                    object instance = Activator.CreateInstance(packetType);
                    if (header.Id  !=  ClientHotfixOpcode.ConfirmMsgRequestBody)
                    {
                        int pos = (int)source.Position;
                        int length = (int)source.Length;
                        byte[] bytes = (source as MemoryStream).GetBuffer();
                        byte[] enBytes = EncryptionUtility.Decrypt(GetHeaderByte1(bytes, pos, length));
                      
                        enMemoryStream.Write(enBytes);
                        enMemoryStream.Position = pos;
                        
                        packet = (Packet) ProtobufHelper.FromStream(instance,enMemoryStream);
                    }else
                    {
                        packet = (Packet) ProtobufHelper.FromStream(instance, (MemoryStream) source);
                    }
                    
                    // packet = (Packet) ProtobufHelper.FromStream(instance, (MemoryStream) source);
                }
                else
                {
                    Log.Warning("Can not deserialize packet for packet id '{0}'.", header.Id.ToString());
                }
            }
            else
            {
                Log.Warning("Packet header is invalid.");
            }

            ReferencePool.Release(header);
            return packet;
        }

        public IPacketHeader DeserializePacketHeader(Stream source, out object customErrorData)
        {
            customErrorData = null;

            ET_SCPacketHeader scHeader = ReferencePool.Acquire<ET_SCPacketHeader>();
            MemoryStream memoryStream = source as MemoryStream;
            if (memoryStream != null)
            {
                byte[] buffer = memoryStream.GetBuffer();
            
                // 从 fullBuffer 中复制 4 个字节到 fourBytes 数组
                // int packetSize = (int)memoryStream.Length;
                int packetSize = BitConverter.ToInt32(GetHeaderByte(buffer,0,4), 0);
             
                int ClientSeqId =  BitConverter.ToInt32(GetHeaderByte(buffer,4,4), 0);
                int opcode = BitConverter.ToInt32(GetHeaderByte(buffer,8,4), 0);
                int MessageId = opcode;
                long ServerSendTime = BitConverter.ToInt64(GetHeaderByte(buffer,12,8), 0);
                int Version = BitConverter.ToInt32(GetHeaderByte(buffer,20,4), 0);
                ushort Compress =  BitConverter.ToChar(GetHeaderByte(buffer,24,2), 0);
                int Error = BitConverter.ToInt32(buffer, 26);

                //这里需要用服务端发过来的packetSize的值减去消息包中opcode的长度，
                //因为服务端在发送消息时设置的packetSize的值是包含opcode的，而
                //客户端在解析包头的时候已经解析了opcode，因此剩余要解析的数据长度要减去2（opcode的总长度是2个字节）
                scHeader.PacketLength = packetSize - ETPackets.ET_MessageIdentifyLength;
                scHeader.Id = (ushort)opcode;
                scHeader.ClientSeqId = ClientSeqId;
                scHeader.MessageId = MessageId;
                scHeader.ClientSendTime = ServerSendTime;
                scHeader.Version = Version;
                scHeader.Compress = Compress;
                return scHeader;
            }

            return null;
        }

        /// <summary>
        /// 获取返回字节数据，并转为小端序字节
        /// </summary>
        /// <param name="buffer">源数组</param>
        /// <param name="sourceIndex">原数组的开始截取的位置</param>
        /// <param name="len">要复制的字节数</param>
        /// <returns></returns>
        private byte[] GetHeaderByte( byte[] buffer,int sourceIndex  ,int len )
        {
            byte[] fourBytes = new byte[len];
            Array.Copy(buffer, sourceIndex, fourBytes, 0, len);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(fourBytes);
            }
            return fourBytes;
        }
        
        private byte[] GetHeaderByte1( byte[] buffer,int sourceIndex  ,int len )
        {
            byte[] fourBytes = new byte[len];
            Array.Copy(buffer, sourceIndex, fourBytes, 0, len);
            // if (BitConverter.IsLittleEndian)
            // {
            //     Array.Reverse(fourBytes);
            // }
            return fourBytes;
        }


        public bool SendHeartBeat()
        {
            HeartbeatMsgRequest msgRequest = new HeartbeatMsgRequest(){};
            GameEntry.Net.Send(msgRequest,ENetState.TC );
            sendHeartBeat = true;
            return sendHeartBeat;
        }

        public bool Serialize<T>(T packet, Stream destination) where T : Packet
        {
            PacketBase packetImpl = packet as PacketBase;
            if (packetImpl == null)
            {
                Log.Warning("Packet is invalid.");
                return false;
            }

            if (packetImpl.PacketType != PacketType.ClientToServer)
            {
                Log.Warning("Send packet invalid.");
                return false;
            }

            int packetLength = 32;
            memoryStream.GetBuffer();
            memoryStream.Seek(packetLength, SeekOrigin.Begin);
            memoryStream.SetLength(packetLength);
            
            
            var protoBytes =  ProtobufHelper.ToBytes(packet);
            int bodyLength = (int)protoBytes.Length;
            protoBytes = EncryptionUtility.Encrypt(protoBytes);
            if (protoBytes != null)
            {
                bodyLength = protoBytes.Length;
                memoryStream.SetLength(bodyLength + packetLength);
                Array.Copy(protoBytes, 0, memoryStream.GetBuffer(), packetLength, protoBytes.Length);
            }
            else
            {
                ProtobufHelper.ToStream(packet, memoryStream);
            }
       
            // 头部消息
            ET_CSPacketHeader packetHeader = ReferencePool.Acquire<ET_CSPacketHeader>();
            packetHeader.PacketLength = bodyLength; // 消息内容长度需要减去头部消息长度,只包含packetSize一个字段
            packetHeader.ClientSeqId = 2;
            packetHeader.MessageId = packet.Id;
            packetHeader.ServiceId = 1;
            packetHeader.ClientSendTime =  (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
            packetHeader.Version = 1;
            packetHeader.ServerId = 1;
            packetHeader.Compress = 0;
            
            memoryStream.Position = 0;

            this.byteses.Clear();
            this.byteses.Add(ConvertToBigEndian(packetHeader.PacketLength));
            this.byteses.Add(ConvertToBigEndian(packetHeader.ClientSeqId));
            this.byteses.Add(ConvertToBigEndian(packetHeader.MessageId));
            this.byteses.Add(ConvertToBigEndian(packetHeader.ServiceId));
            this.byteses.Add(ConvertToBigEndian(packetHeader.ClientSendTime));
            this.byteses.Add(ConvertToBigEndian(packetHeader.Version));
            this.byteses.Add(ConvertToBigEndian(packetHeader.ServerId));
            this.byteses.Add(ConvertToBigEndian(packetHeader.Compress));
            int index = 0;
            for (int i = 0; i < this.byteses.Count; i++)
            {   
                var bytes = this.byteses[i];
                Array.Copy(bytes, 0, memoryStream.GetBuffer(), index, bytes.Length);
                index += bytes.Length ;
            }
            
            ReferencePool.Release(packetHeader);

            memoryStream.WriteTo(destination);

            long len = destination.Length;
            long pos = destination.Position;
            byte[] temp = (destination as MemoryStream).GetBuffer();
            return true;
        }
        
        /// <summary>
        /// 与java的对应：将小端序转为大端序
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// 
        static byte[] ConvertToBigEndian(int value)
        {
             byte[] bytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
            {
                return bytes;
            }
            else
            {
                Array.Reverse(bytes);
                return bytes;
            }
        }
        
        static byte[] ConvertToBigEndian(ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
            {
                return bytes;
            }
            else
            {
                Array.Reverse(bytes);
                return bytes;
            }
        }

        static byte[] ConvertToBigEndian(long value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
            {
                return bytes;
            }
            else
            {
                Array.Reverse(bytes);
                return bytes;
            }
        }

        static byte[] ConvertToBigEndian(byte value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
            {
                return bytes;
            }
            else
            {
                Array.Reverse(bytes);
                return bytes;
            }
        }
        
        public void Shutdown()
        {
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkConnectedEventArgs.EventId,
                OnNetworkConnected);
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkClosedEventArgs.EventId, OnNetworkClosed);
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkMissHeartBeatEventArgs.EventId,
                OnNetworkMissHeartBeat);
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkErrorEventArgs.EventId, OnNetworkError);
            GameEntry.Event.Unsubscribe(UnityGameFramework.Runtime.NetworkCustomErrorEventArgs.EventId,
                OnNetworkCustomError);
            GameEntry.Event.Unsubscribe(ClientHotfixOpcode.Heart,
                OnHeartBack);
            m_NetworkChannel = null;
            memoryStream.Dispose();
        }


        private Type GetServerToClientPacketType(int id)
        {
            Type type = null;
            if (m_ServerToClientPacketTypes.TryGetValue(id, out type))
            {
                return type;
            }

            return null;
        }


        private void OnNetworkConnected(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkConnectedEventArgs ne =
                (UnityGameFramework.Runtime.NetworkConnectedEventArgs) e;
            if (ne.NetworkChannel != m_NetworkChannel)
            {
                return;
            }

            Log.Info("Network channel '{0}' connected, local address '{1}:{2}', remote address '{3}:{4}'.",
                ne.NetworkChannel.Name, ne.NetworkChannel.LocalIPAddress, ne.NetworkChannel.LocalPort,
                ne.NetworkChannel.RemoteIPAddress, ne.NetworkChannel.RemotePort.ToString());
        }

        private void OnNetworkClosed(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkClosedEventArgs
                ne = (UnityGameFramework.Runtime.NetworkClosedEventArgs) e;
            if (ne.NetworkChannel != m_NetworkChannel)
            {
                return;
            }

            Log.Info("Network channel '{0}' closed.", ne.NetworkChannel.Name);
        }


        private void OnNetworkMissHeartBeat(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkMissHeartBeatEventArgs ne =
                (UnityGameFramework.Runtime.NetworkMissHeartBeatEventArgs) e;
            if (ne.NetworkChannel != m_NetworkChannel)
            {
                return;
            }

            Log.Info("Network channel '{0}' miss heart beat '{1}' times.", ne.NetworkChannel.Name,
                ne.MissCount.ToString());

            if (ne.MissCount < missCount)
            {
                return;
            }

            ne.NetworkChannel.Close();
        }

        private void OnNetworkError(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkErrorEventArgs ne = (UnityGameFramework.Runtime.NetworkErrorEventArgs) e;
            if (ne.NetworkChannel != m_NetworkChannel)
            {
                return;
            }

            Log.Info("Network channel '{0}' error, error code is '{1}', error message is '{2}'.",
                ne.NetworkChannel.Name, ne.ErrorCode.ToString(), ne.ErrorMessage);

            ne.NetworkChannel.Close();
        }

        private void OnNetworkCustomError(object sender, GameEventArgs e)
        {
            UnityGameFramework.Runtime.NetworkCustomErrorEventArgs ne =
                (UnityGameFramework.Runtime.NetworkCustomErrorEventArgs) e;
            if (ne.NetworkChannel != m_NetworkChannel)
            {
                return;
            }
        }
    }
}