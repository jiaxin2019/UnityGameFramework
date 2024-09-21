using StarForce;
using UnityEngine;
using UnityEngine.UI;

//自动生成于：2024/9/19 13:47:24
namespace StarForce
{
	public class MainUIForm : UGuiForm
	{
		[HideInInspector]
		public Button m_JiaoSe;
		[HideInInspector]
		public Button m_Dongfu;
		[HideInInspector]
		public Button m_Shezhi;
		[HideInInspector]
		public Button m_Xiuxing;

		protected override void OnInit(object userData)
		{
			base.OnInit(userData);

			ReferenceCollector rc = GetComponent<ReferenceCollector>();

			m_JiaoSe = rc.GetComp<Button>("Button_JiaoSe");
			m_Dongfu = rc.GetComp<Button>("Button_Dongfu");
			m_Shezhi = rc.GetComp<Button>("Button_Shezhi");
			m_Xiuxing = rc.GetComp<Button>("Button_Xiuxing");
		}
	}
}
