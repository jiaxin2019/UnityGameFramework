using StarForce;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//自动生成于：2024/9/19 16:33:26
namespace StarForce
{
	public class XiuXingDujieForm : UGuiForm
	{
		[HideInInspector]
		public TMP_Text m_Tittle;
		[HideInInspector]
		public TMP_Text m_Des;
		[HideInInspector]
		public TMP_Text m_Dan;
		[HideInInspector]
		public TMP_Text m_Fabao;
		[HideInInspector]
		public TMP_Text m_Fubao;
		[HideInInspector]
		public Button m_Help;
		[HideInInspector]
		public TMP_Text m_Round;
		[HideInInspector]
		public TMP_Text m_Del;
		[HideInInspector]
		public TMP_Text m_Del1;
		[HideInInspector]
		public Image m_hu;
		[HideInInspector]
		public TMP_Text m_Progress1;
		[HideInInspector]
		public TMP_Text m_Progress2;

		protected override void OnInit(object userData)
		{
			base.OnInit(userData);

			ReferenceCollector rc = GetComponent<ReferenceCollector>();

			m_Tittle = rc.GetComp<TMP_Text>("TMP_Text_Tittle");
			m_Des = rc.GetComp<TMP_Text>("TMP_Text_Des");
			m_Dan = rc.GetComp<TMP_Text>("TMP_Text_Dan");
			m_Fabao = rc.GetComp<TMP_Text>("TMP_Text_Fabao");
			m_Fubao = rc.GetComp<TMP_Text>("TMP_Text_Fubao");
			m_Help = rc.GetComp<Button>("Button_Help");
			m_Round = rc.GetComp<TMP_Text>("TMP_Text_Round");
			m_Del = rc.GetComp<TMP_Text>("TMP_Text_Del");
			m_Del1 = rc.GetComp<TMP_Text>("TMP_Text_Del1");
			m_hu = rc.GetComp<Image>("Image_hu");
			m_Progress1 = rc.GetComp<TMP_Text>("TMP_Text_Progress1");
			m_Progress2 = rc.GetComp<TMP_Text>("TMP_Text_Progress2");
		}
	}
}
