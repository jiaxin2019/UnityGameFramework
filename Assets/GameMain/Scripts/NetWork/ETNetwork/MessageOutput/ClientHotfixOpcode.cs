using NKG.NetWork;
using Xiu;

namespace NKG.NetWork
{
	public partial class C2R_Login : CSPacketBase 
	{
		public override int Id => ClientHotfixOpcode.C2R_Login;
		public override void Clear()
		{

		}
	}
	public partial class R2C_Login : SCPacketBase 
	{
		public override int Id => ClientHotfixOpcode.R2C_Login;
		public override void Clear()
		{

		}
	}
	public partial class C2G_LoginGate : CSPacketBase 
	{
		public override int Id => ClientHotfixOpcode.C2G_LoginGate;
		public override void Clear()
		{

		}
	}
	public partial class G2C_LoginGate : SCPacketBase 
	{
		public override int Id => ClientHotfixOpcode.G2C_LoginGate;
		public override void Clear()
		{

		}
	}
	public partial class G2C_TestHotfixMessage : SCPacketBase 
	{
		public override int Id => ClientHotfixOpcode.G2C_TestHotfixMessage;
		public override void Clear()
		{

		}
	}
	public partial class C2M_TestActorRequest : CSPacketBase 
	{
		public override int Id => ClientHotfixOpcode.C2M_TestActorRequest;
		public override void Clear()
		{

		}
	}
	public partial class M2C_TestActorResponse : SCPacketBase 
	{
		public override int Id => ClientHotfixOpcode.M2C_TestActorResponse;
		public override void Clear()
		{

		}
	}
	public partial class PlayerInfo : CSPacketBase 
	{
		public override int Id => ClientHotfixOpcode.PlayerInfo;
		public override void Clear()
		{

		}
	}
	public partial class C2G_PlayerInfo : CSPacketBase 
	{
		public override int Id => ClientHotfixOpcode.C2G_PlayerInfo;
		public override void Clear()
		{

		}
	}
	public partial class G2C_PlayerInfo : SCPacketBase 
	{
		public override int Id => ClientHotfixOpcode.G2C_PlayerInfo;
		public override void Clear()
		{

		}
	}
	public partial class TestMsg : CSPacketBase 
	{
		public override int Id => ClientHotfixOpcode.TestMsg;
		public override void Clear()
		{

		}
	}
	public partial class GFTestMsg : CSPacketBase 
	{
		public override int Id => ClientHotfixOpcode.GFTestMsg;
		public override void Clear()
		{

		}
	}
	
	public partial class ConfirmMsgRequestBody : CSPacketBase 
	{
		public override int Id => ClientHotfixOpcode.ConfirmMsgRequestBody;
		public override void Clear()
		{

		}
	}
	
	public partial class ConfirmMsgResponseBody : SCPacketBase 
	{
		public override int Id => ClientHotfixOpcode.ConfirmMsgRequestBody;
		public override void Clear()
		{

		}
	}
	
	public partial class EnterGameMsgRequest: CSPacketBase 
	{
		public override int Id => ClientHotfixOpcode.EnterGameMsgResponse;
		public override void Clear()
		{

		}
	}
	
	public partial class EnterGameMsgResponse : SCPacketBase 
	{
		public override int Id => ClientHotfixOpcode.EnterGameMsgResponse;
		public override void Clear()
		{

		}
	}
	
	public partial class HeartbeatMsgRequest: CSPacketBase 
	{
		public override int Id => ClientHotfixOpcode.Heart;
		public override void Clear()
		{

		}
	}
	
	public partial class HeartbeatMsgResponseBody : SCPacketBase 
	{
		public override int Id => ClientHotfixOpcode.Heart;
		public override void Clear()
		{

		}
	}

}
namespace NKG.NetWork
{
	public static partial class ClientHotfixOpcode
	{
		 public const ushort C2R_Login = 10001;
		 public const ushort R2C_Login = 10002;
		 public const ushort C2G_LoginGate = 10003;
		 public const ushort G2C_LoginGate = 10004;
		 public const ushort G2C_TestHotfixMessage = 10005;
		 public const ushort C2M_TestActorRequest = 10006;
		 public const ushort M2C_TestActorResponse = 10007;
		 public const ushort PlayerInfo = 10008;
		 public const ushort C2G_PlayerInfo = 10009;
		 public const ushort G2C_PlayerInfo = 10010;
		 public const ushort TestMsg = 10011;
		 public const ushort GFTestMsg = 10012;
		 
		 //登录验证，获取token
		 public const ushort ConfirmMsgRequestBody = 1;
		 public const ushort Heart = 2;
		 //获取玩家信息
		 public const ushort EnterGameMsgResponse = 10001;
	}
}
