
using GameFramework.Network;
using UnityEngine;
using System.Net;
using GameFramework.Event;
using StarForce;


namespace NKG.NetWork
{
    public partial class ConfirmMsgHandler : PacketHandlerBase
    {
        public override int Id
        {
            get { return ClientHotfixOpcode.ConfirmMsgRequestBody; }
        }

        ConfirmMsgResponseBody confirm;

        public override void Handle(object sender, Packet packet)
        {
            Debug.Log("Get Message From the Server:ConfirmMsgHandler");
            ConfirmMsgResponseBody loginResult = packet as ConfirmMsgResponseBody;
            if (loginResult != null)
            {
                // Debug.Log(string.Format("Message Content-> Address:{0}, Key->{1} ", loginResult.Address,
                //     loginResult.Key));
            }

            confirm = loginResult;
            //NetworkComponent network = MGame.GameEntry.Network;
            //INetworkChannelHelper helper = new ET_NetworkChannelHelper();
            //INetworkChannel nc = network.CreateNetworkChannel("CG_TC", helper);
            //nc.HeartBeatInterval = 0f;

            //IPEndPoint ipPoint = NetworkHelper.ToIPEndPoint(loginResult.Address);
            //nc.Connect(ipPoint.Address, ipPoint.Port,r2c_Login);
            GameEntry.Event.Fire(confirm, new MGameEvetArgs(ClientHotfixOpcode.ConfirmMsgRequestBody));
        }
    }
    
    public partial class EnterGameMsgHandler : PacketHandlerBase
    {
        public override int Id
        {
            get { return ClientHotfixOpcode.EnterGameMsgResponse; }
        }

        EnterGameMsgResponse confirm;

        public override void Handle(object sender, Packet packet)
        {
            Debug.Log("Get Message From the Server:EnterGameMsgHandler");
            EnterGameMsgResponse loginResult = packet as EnterGameMsgResponse;
            if (loginResult != null)
            {
                // Debug.Log(string.Format("Message Content-> Address:{0}, Key->{1} ", loginResult.Address,
                //     loginResult.Key));
            }

            confirm = loginResult;
            //NetworkComponent network = MGame.GameEntry.Network;
            //INetworkChannelHelper helper = new ET_NetworkChannelHelper();
            //INetworkChannel nc = network.CreateNetworkChannel("CG_TC", helper);
            //nc.HeartBeatInterval = 0f;

            //IPEndPoint ipPoint = NetworkHelper.ToIPEndPoint(loginResult.Address);
            //nc.Connect(ipPoint.Address, ipPoint.Port,r2c_Login);
            GameEntry.Event.Fire(confirm, new MGameEvetArgs(ClientHotfixOpcode.EnterGameMsgResponse));
        }
    }
    
     public partial class HeartHandler : PacketHandlerBase
        {
            public override int Id
            {
                get { return ClientHotfixOpcode.Heart; }
            }
    
            HeartbeatMsgResponseBody confirm;
    
            public override void Handle(object sender, Packet packet)
            {
                Debug.Log("Get Message From the Server:HeartHandler");
                HeartbeatMsgResponseBody loginResult = packet as HeartbeatMsgResponseBody;
                if (loginResult != null)
                {
                    // Debug.Log(string.Format("Message Content-> Address:{0}, Key->{1} ", loginResult.Address,
                    //     loginResult.Key));
                }
    
                confirm = loginResult;
                //NetworkComponent network = MGame.GameEntry.Network;
                //INetworkChannelHelper helper = new ET_NetworkChannelHelper();
                //INetworkChannel nc = network.CreateNetworkChannel("CG_TC", helper);
                //nc.HeartBeatInterval = 0f;
    
                //IPEndPoint ipPoint = NetworkHelper.ToIPEndPoint(loginResult.Address);
                //nc.Connect(ipPoint.Address, ipPoint.Port,r2c_Login);
                GameEntry.Event.Fire(confirm, new MGameEvetArgs(ClientHotfixOpcode.Heart));
            }
        }

    
    public partial class R2C_LoginHandler : PacketHandlerBase
    {
        public override int Id
        {
            get { return 10002; }
        }

        R2C_Login r2c_Login;

        public override void Handle(object sender, Packet packet)
        {
            Debug.Log("Get Message From the Server");
            R2C_Login loginResult = packet as R2C_Login;
            if (loginResult != null)
            {
                Debug.Log(string.Format("Message Content-> Address:{0}, Key->{1} ", loginResult.Address,
                    loginResult.Key));
            }

            r2c_Login = loginResult;
            //NetworkComponent network = MGame.GameEntry.Network;
            //INetworkChannelHelper helper = new ET_NetworkChannelHelper();
            //INetworkChannel nc = network.CreateNetworkChannel("CG_TC", helper);
            //nc.HeartBeatInterval = 0f;

            //IPEndPoint ipPoint = NetworkHelper.ToIPEndPoint(loginResult.Address);
            //nc.Connect(ipPoint.Address, ipPoint.Port,r2c_Login);
            GameEntry.Event.Fire(r2c_Login, new MGameEvetArgs(10001));
        }
    }

    public partial class G2C_LoginGateHandler : PacketHandlerBase
    {
        public override int Id
        {
            get { return 10004; }
        }

        public override void Handle(object sender, Packet packet)
        {
            G2C_LoginGate result = packet as G2C_LoginGate;
            if (result != null)
            {
                Debug.Log(string.Format("Message:{0},PlayerId:{1}", result.Message, result.PlayerId));
                
            }
        }
    }

    public partial class G2C_TestHotfixMessageHandler : PacketHandlerBase
    {
        public override int Id
        {
            get { return 10005; }
        }

        public override void Handle(object sender, Packet packet)
        {
            G2C_TestHotfixMessage result = packet as G2C_TestHotfixMessage;
            if (result != null)
            {
                Debug.Log(string.Format("Message:{0},Id:{1}", result.Info, result.Id));
            }
        }
    }


    public sealed class MGameEvetArgs : GameEventArgs
    {
        int id = 0;

        public override int Id
        {
            get { return id; }
        }

        public override void Clear()
        {
        }

        public MGameEvetArgs(int id)
        {
            this.id = id;
        }
    }

    public static class NetworkHelper
    {
        public static IPEndPoint ToIPEndPoint(string host, int port)
        {
            return new IPEndPoint(IPAddress.Parse(host), port);
        }

        public static IPEndPoint ToIPEndPoint(string address)
        {
            int index = address.LastIndexOf(':');
            string host = address.Substring(0, index);
            string p = address.Substring(index + 1);
            int port = int.Parse(p);
            return ToIPEndPoint(host, port);
        }
    }
}