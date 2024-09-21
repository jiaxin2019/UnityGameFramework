using StarForce;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//自动生成于：2024/9/19 16:33:26
namespace StarForce
{
	public class XiuXingPjFailForm : UGuiForm
	{
		[HideInInspector]
		public TMP_Text m_Tittle;
		[HideInInspector]
		public TMP_Text m_Time;

		protected override void OnInit(object userData)
		{
			base.OnInit(userData);

			ReferenceCollector rc = GetComponent<ReferenceCollector>();

			m_Tittle = rc.GetComp<TMP_Text>("TMP_Text_Tittle");
			m_Time = rc.GetComp<TMP_Text>("TMP_Text_Time");
		}
	}
}
