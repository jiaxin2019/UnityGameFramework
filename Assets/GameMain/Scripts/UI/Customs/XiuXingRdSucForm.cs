using StarForce;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//自动生成于：2024/9/19 16:33:26
namespace StarForce
{
	public class XiuXingRdSucForm : UGuiForm
	{
		[HideInInspector]
		public TMP_Text m_Tittle;
		[HideInInspector]
		public TMP_Text m_Des;
		[HideInInspector]
		public TMP_Text m_Num;
		
		protected override void OnInit(object userData)
		{
			base.OnInit(userData);

			ReferenceCollector rc = GetComponent<ReferenceCollector>();

			m_Tittle = rc.GetComp<TMP_Text>("TMP_Text_Tittle");
			m_Des = rc.GetComp<TMP_Text>("TMP_Text_Des");
			m_Num = rc.GetComp<TMP_Text>("TMP_Text_Num");
		}
	}
}
