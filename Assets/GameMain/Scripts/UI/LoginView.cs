using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NKG.NetWork;
using StarForce;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityGameFramework.Runtime;
using Xiu;
using GameEntry = StarForce.GameEntry;


//自动生成于：2024/8/30 16:07:06
namespace StarForce
{
	public class LoginView : UGuiForm
	{

		private Button m_btn_login;
		private TMP_Dropdown dropdownQu;
		private ProcedureMenu m_ProcedureMenu = null;
		private TMP_InputField input = null;
		private int quIndex = 0;

		public LoginData loginData;
		protected override void OnInit(object userData)
		{
			base.OnInit(userData);
			m_ProcedureMenu = (ProcedureMenu)userData;
			if (m_ProcedureMenu == null)
			{
				Log.Warning("ProcedureMenu is invalid when open MenuForm.");
				return;
			}
		
			// string descriptionText = GameEntry.Localization.GetString("UpdateResource.Tips");
			m_btn_login = transform.Find("ButtonEnter").GetComponent<Button>();
			dropdownQu = transform.Find("DropdownQF")?.GetComponent<TMP_Dropdown>();
			input = transform.Find("InputFieldName")?.GetComponent<TMP_InputField>();
			input.onValueChanged.AddListener(text =>
			{
				loginData.OpenId = text;
			});
			if (dropdownQu)
			{
				InitDropdownQu();
			}
			m_btn_login.onClick.AddListener(OnEnterGame);
			GameEntry.Net.AddHandler(ClientHotfixOpcode.ConfirmMsgRequestBody, OnfirmBack);
			initLoginData();
		}

		private void initLoginData()
		{
			loginData = LoginData.Instance;
			loginData.OpenId = "465132";
			loginData.Token = "5156465";
			loginData.PlayerId = 10000;
			loginData.UserId = 100000;
			loginData.ZoneId = "0";
		}

		protected override void OnClose(bool isShutdown,object userData)
		{
			GameEntry.Net.removeHandler(ClientHotfixOpcode.ConfirmMsgRequestBody, OnfirmBack);
			base.OnClose(isShutdown,userData);
		}

		private void OnfirmBack(object sender, EventArgs e)
		{
			ConfirmMsgResponseBody loginResult = sender as ConfirmMsgResponseBody;
			if (loginResult == null)
			{
				return;
			}

			m_ProcedureMenu.StartGame();
			DataPlayer dataPlayer = GameEntry.Data.GetData<DataPlayer>();
			dataPlayer.EnterGameMsgRes();
			this.Close();
			Log.Debug($"OnGetEnterGameMsg{sender}");
		}

		private void InitDropdownQu()
		{

			List<string> options = new List<string> { "乔娜" };
			dropdownQu.AddOptions(options);
			dropdownQu.onValueChanged.AddListener(delegate { OnDropChange(); });
		}

		private void OnDropChange()
		{
			loginData.ZoneId = dropdownQu.value.ToString();
			Debug.Log($"选项：{quIndex}");
		}

		private void OnEnterGame()
		{
			if (input.text == "")
			{
				return;
			}
		
			this.step10001();
		}

		private void step10001()
		{
		
			string deviceId = SystemInfo.deviceUniqueIdentifier;
			
			var json = new JObject();
			json["openId"] = loginData.OpenId;
			json["token"] = loginData.Token;
			json["deviceId"] = deviceId;
			string jsonString = json.ToString();
			// 定义成功回调
			Action<string> onSuccess = (response) => {
				Debug.Log("Response in onSuccess: " + response);
				// 处理响应数据
				HttpBack10001 data = JsonConvert.DeserializeObject<HttpBack10001>(response);
				if (data.code != 0)
				{
					return;
				}
				
				loginData.UserId = data.data.userId ;
				loginData.Token = data.data.token;
				step10002();
			};
			//发送web请求，获取服务器上的版本信息（version.txt）
			string http = $"http://192.168.1.138:50033/request/{10001}";
			StartCoroutine(GameEntry.WebRequest.SendPostRequest(http, jsonString, onSuccess));

		}
		
		private void step10002()
		{
			var json = new JObject();
			json["zoneId"] = loginData.ZoneId;
			json["nickName"] = loginData.NickName;
			json["openId"] = loginData.OpenId;
			string jsonString = json.ToString();
			// 定义成功回调
			Action<string> onSuccess = (response) => {
				Debug.Log("Response in onSuccess: " + response);
				// 处理响应数据
				HttpBack10002 data = JsonConvert.DeserializeObject<HttpBack10002>(response);
				if (data.code != 0)
				{
					return;
				}
				loginData.PlayerId = data.data.playerId;
				step10003();
			};
			//发送web请求，获取服务器上的版本信息（version.txt）
			string http = $"http://192.168.1.138:50033/request/{10002}";
			StartCoroutine(GameEntry.WebRequest.SendPostRequest(http, jsonString,onSuccess));
		}
		
		private void step10003()
		{
			
			var json = new JObject();
			json["openId"] = this.loginData.OpenId;
			json["playerId"] = this.loginData.PlayerId;
			json["userId"] = this.loginData.UserId;
			json["zoneId"] = this.loginData.ZoneId;
			string jsonString = json.ToString();
			// 定义成功回调
			Action<string> onSuccess = (response) => {
				Debug.Log("Response in onSuccess: " + response);
				// 处理响应数据
				HttpBack10003 data = JsonConvert.DeserializeObject<HttpBack10003>(response);
				if (data.code != 0)
				{
					return;
				}
				loginData.Ip = data.data.ip;
				loginData.Port = data.data.port;
				loginData.Token = data.data.token;
				connetSocket();
			};
			//发送web请求，获取服务器上的版本信息（version.txt）
			string http = $"http://192.168.1.138:50033/request/{10003}";
			StartCoroutine(GameEntry.WebRequest.SendPostRequest(http, jsonString,onSuccess));
		}

		private void connetSocket()
		{
			GameEntry.Net.Oninit();
			m_btn_login.enabled = false;
			GameEntry.Net.Connect(loginData.Ip,loginData.Port,ENetState.TC);
		}
		
	}
}
