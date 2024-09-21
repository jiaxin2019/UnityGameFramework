using System.Collections.Generic;
using GameFramework;
using StarForce;

namespace StarForce
{
    
    public class LoginData:DataBase
    {
        
        private static LoginData _Instance;
        public static LoginData Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new LoginData();
                }
                return _Instance;
            }
        }
     
        /// <summary>
        /// 当前的屈服列表数据
        /// </summary>
        private List<IQuFu> quFus = new List<IQuFu>();

        public void InitList(object param)
        {
            quFus = param as List<IQuFu>;
        }
        /// <summary>
        /// 用户的openid
        /// </summary>
        public string OpenId {get;set;}
        /// <summary>
        /// 用户的token，多用于三方的接入
        /// </summary>
        public string Token {get;set;}
        /// <summary>
        /// 分区id，默认："0"
        /// </summary>
        public string ZoneId {get;set;}
        public string lastZoneId {get;set;}
        
        public string NickName {get;set;}
        public long PlayerId {get;set;}
        public long UserId {get;set;}
        
        public string Ip {get;set;}
        public int Port{get;set;}

    }

    public interface IQuFu
    {
        /// <summary>
        /// 区服的id
        /// </summary>
        string Id {get;set;}
        /// <summary>
        /// 区服的名字
        /// </summary>
        string Name { get; }
         
        
    }

    public class HttpBack10001
    {
        public int code;
        public HttpData data;
        public string errorMsg;
    }
    
    

    public class HttpData
    {
        public long userId;
        public string token;
        public ZonePlayerMap zonePlayerMap; // 这里假设是一个空字典
        public object lastZoneId; // 这里使用 object 类型，因
    }
    
    public class ZonePlayerMap
    {
        // 由于这是一个空对象，我们不需要任何字段
        // 但我们需要确保它是可序列化的
    }
    
    public class HttpBack10002
    {
        public int code {get;set;}
        public LoginData10002 data {get;set;}
    }

    public class LoginData10002
    {
        public long playerId;
    }
    
    public class HttpBack10003
    {
        public int code {get;set;}
        public LoginData10003 data {get;set;}
    }

    public class LoginData10003
    {
        public string ip;
        public int port;
        public string token;
    }
}