using StarForce;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//自动生成于：2024/9/19 16:33:26
namespace StarForce
{
	public class XiuXingPjSucForm : UGuiForm
	{
		[HideInInspector]
		public TMP_Text m_PreLv;
		[HideInInspector]
		public TMP_Text m_CurLv;

		protected override void OnInit(object userData)
		{
			base.OnInit(userData);

			ReferenceCollector rc = GetComponent<ReferenceCollector>();

			m_PreLv = rc.GetComp<TMP_Text>("TMP_Text_PreLv");
			m_CurLv = rc.GetComp<TMP_Text>("TMP_Text_CurLv");
		}
	}
}
