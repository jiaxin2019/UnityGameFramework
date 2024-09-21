using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using GameFramework;
using GameFramework.Event;
using GameFramework.Network;
using NKG.NetWork;
using StarForce;
using UnityEngine;
namespace Xiu
{
    public enum ENetState
    {
        /// <summary>
        /// 默认通信通道
        /// </summary>
        TC ,
        
    }

    public class NetManager : UnityGameFramework.Runtime.GameFrameworkComponent
    {
        Dictionary<string, string> m_NetData = new Dictionary<string, string>();
        ET_NetworkChannelHelper netHelper = new ET_NetworkChannelHelper();
        private float heartBeatInterval = 3f;
        // Start is called before the first frame update
        public void Oninit()
        {
   
            GameFrameworkLog.Info(Application.dataPath);
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.NetworkConnectedEventArgs.EventId, OnConnected);

            // GameEntry.Event.Subscribe(ClientHotfixOpcode.EnterGameMsgResponse, OnGetEnterMsg);

        }

        private void OnConnected(object sender, EventArgs e)
        {
            var temp = e as UnityGameFramework.Runtime.NetworkConnectedEventArgs;
            string TCName = ENetState.TC.ToString();
            if (temp.NetworkChannel.Name.Equals(TCName))
            {
                Debug.Log("-----------连接服务器成功-----------:");
                
                ConfirmMsgRequestBody msg = new ConfirmMsgRequestBody() { Token = LoginData.Instance.Token};
                Send(msg, ENetState.TC);
            }
            else if (temp.NetworkChannel.Name.Equals("CG_TC"))
            {
                Debug.Log("-----------连接服务器再次成功-----------:");
                INetworkChannel channel = GameEntry.Network.GetNetworkChannel("CG_TC");
                R2C_Login r2c_Login = temp.UserData as R2C_Login;
                channel.Send(new C2G_LoginGate() { Key = r2c_Login == null ? 0 : r2c_Login.Key });
            }

        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="address">服务器地址：192.168.1.138</param>
        /// <param name="port">服务器端口：6003</param>
        public void Connect(string address, int port,ENetState state)
        {
            Debug.Log("-----------------Begin Connect--------------------:");
            IPAddress ip = null;
            if (IPAddress.TryParse(address, out ip))
            {
                string name = state.ToString();
                INetworkChannel nc = GameEntry.Network.CreateNetworkChannel(name, ServiceType.Tcp,netHelper);
                nc.HeartBeatInterval = heartBeatInterval;
                nc.Connect(ip, port);
            }
        }
        
        /// <summary>
        /// 添加监听
        /// </summary>
        /// <param name="id">事件id</param>
        /// <param name="handler">回调</param>
        public void AddHandler(int id, EventHandler<GameEventArgs> handler)
        {
            GameEntry.Event.Subscribe(id, handler);
        }
        
        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="id">事件id</param>
        /// <param name="handler">回调</param>
        public void removeHandler(int id, EventHandler<GameEventArgs> handler)
        {
            GameEntry.Event.Unsubscribe(id, handler);
        }
        /// <summary>
        /// 发送协议
        /// </summary>
        /// <param name="packet">protobuf 消息体</param>
        /// <param name="name">soket频道 </param>
        /// <typeparam name="T"></typeparam>
        public void Send<T>(T packet,ENetState name) where T : Packet
        {
            string netName = name.ToString();
            INetworkChannel channel = GameEntry.Network.GetNetworkChannel(netName);
            if (channel != null)
            {
                channel.Send(packet);
            }
            else
            {
                Debug.Log($"{netName} 未连接。。。。。");
            }
        }
    }
    
    
}
