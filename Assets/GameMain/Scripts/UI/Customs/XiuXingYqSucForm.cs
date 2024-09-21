using StarForce;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//自动生成于：2024/9/19 16:33:26
namespace StarForce
{
	public class XiuXingYqSucForm : UGuiForm
	{
		[HideInInspector]
		public Button m_OK;
		[HideInInspector]
		public TMP_Text m_XiuXiaolu;
		[HideInInspector]
		public TMP_Text m_Pingji;
		[HideInInspector]
		public TMP_Text m_ZuiXiaolu;
		[HideInInspector]
		public TMP_Text m_XiaoluDes;
		[HideInInspector]
		public Image m_NoActive;
		[HideInInspector]
		public Image m_Active;
		[HideInInspector]
		public TMP_Text m_Time;
		[HideInInspector]
		public TMP_Text m_Des;

		protected override void OnInit(object userData)
		{
			base.OnInit(userData);

			ReferenceCollector rc = GetComponent<ReferenceCollector>();

			m_OK = rc.GetComp<Button>("Button_OK");
			m_XiuXiaolu = rc.GetComp<TMP_Text>("TMP_Text_XiuXiaolu");
			m_Pingji = rc.GetComp<TMP_Text>("TMP_Text_Pingji");
			m_ZuiXiaolu = rc.GetComp<TMP_Text>("TMP_Text_ZuiXiaolu");
			m_XiaoluDes = rc.GetComp<TMP_Text>("TMP_Text_XiaoluDes");
			m_NoActive = rc.GetComp<Image>("Image_NoActive");
			m_Active = rc.GetComp<Image>("Image_Active");
			m_Time = rc.GetComp<TMP_Text>("TMP_Text_Time");
			m_Des = rc.GetComp<TMP_Text>("TMP_Text_Des");
		}
	}
}
