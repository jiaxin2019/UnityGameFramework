using StarForce;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

//自动生成于：2024/9/19 16:40:44
namespace StarForce
{
	public class XiuXingDjSucForm : UGuiForm
	{
		[HideInInspector]
		public ScrollView m_Attr;
		[HideInInspector]
		public TMP_Text m_PreLv;
		[HideInInspector]
		public TMP_Text m_CurLv;
		[HideInInspector]
		public Object m_AttrItem;

		protected override void OnInit(object userData)
		{
			base.OnInit(userData);

			ReferenceCollector rc = GetComponent<ReferenceCollector>();

			m_Attr = rc.GetComp<ScrollView>("ScrollView_Attr");
			m_PreLv = rc.GetComp<TMP_Text>("TMP_Text_PreLv");
			m_CurLv = rc.GetComp<TMP_Text>("TMP_Text_CurLv");
			m_AttrItem = rc.Get<Object>("Object_AttrItem");
		}
	}
}
