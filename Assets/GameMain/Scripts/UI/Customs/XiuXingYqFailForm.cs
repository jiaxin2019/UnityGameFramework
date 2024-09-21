using StarForce;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//自动生成于：2024/9/19 16:33:26
namespace StarForce
{
	public class XiuXingYqFailForm : UGuiForm
	{
		[HideInInspector]
		public TMP_Text m_Des;
		[HideInInspector]
		public Button m_Back;
		[HideInInspector]
		public Button m_Ok;

		protected override void OnInit(object userData)
		{
			base.OnInit(userData);

			ReferenceCollector rc = GetComponent<ReferenceCollector>();

			m_Des = rc.GetComp<TMP_Text>("TMP_Text_Des");
			m_Back = rc.GetComp<Button>("Button_Back");
			m_Ok = rc.GetComp<Button>("Button_Ok");
		}
	}
}
