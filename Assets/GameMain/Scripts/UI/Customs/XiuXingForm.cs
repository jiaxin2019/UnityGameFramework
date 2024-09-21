using StarForce;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//自动生成于：2024/9/19 16:33:26
namespace StarForce
{
	public class XiuXingForm : UGuiForm
	{
		[HideInInspector]
		public TMP_Text m_Tittle;
		[HideInInspector]
		public TMP_Text m_LvTittle;
		[HideInInspector]
		public TMP_Text m_BookLv;
		[HideInInspector]
		public TMP_Text m_Msg;
		[HideInInspector]
		public Button m_Yin;
		[HideInInspector]
		public Button m_Help;
		[HideInInspector]
		public Button m_Back;
		[HideInInspector]
		public ToggleGroup m_BG;
		[HideInInspector]
		public Image m_BackIcon;
		[HideInInspector]
		public Image m_BookBg ;
		[HideInInspector]
		public Button m_BookChange;
		[HideInInspector]
		public TMP_Text m_Sudu;
		[HideInInspector]
		public TMP_Text m_NextLv;
		[HideInInspector]
		public Button m_Help1;
		[HideInInspector]
		public Button m_Help2;
		[HideInInspector]
		public Button m_Dujie;
		[HideInInspector]
		public Button m_HuiFu;
		[HideInInspector]
		public Button m_Tupo;
		[HideInInspector]
		public Image m_XXBg;
		[HideInInspector]
		public TMP_Text m_Lv;

		protected override void OnInit(object userData)
		{
			base.OnInit(userData);

			ReferenceCollector rc = GetComponent<ReferenceCollector>();

			m_Tittle = rc.GetComp<TMP_Text>("TMP_Text_Tittle");
			m_LvTittle = rc.GetComp<TMP_Text>("TMP_Text_LvTittle");
			m_BookLv = rc.GetComp<TMP_Text>("TMP_Text_BookLv");
			m_Msg = rc.GetComp<TMP_Text>("TMP_Text_Msg");
			m_Yin = rc.GetComp<Button>("Button_Yin");
			m_Help = rc.GetComp<Button>("Button_Help");
			m_Back = rc.GetComp<Button>("Button_Back");
			m_BG = rc.GetComp<ToggleGroup>("ToggleGroup_BG");
			m_BackIcon = rc.GetComp<Image>("Image_BackIcon");
			m_BookBg  = rc.GetComp<Image>("Image_BookBg ");
			m_BookChange = rc.GetComp<Button>("Button_BookChange");
			m_Sudu = rc.GetComp<TMP_Text>("TMP_Text_Sudu");
			m_NextLv = rc.GetComp<TMP_Text>("TMP_Text_NextLv");
			m_Help1 = rc.GetComp<Button>("Button_Help1");
			m_Help2 = rc.GetComp<Button>("Button_Help2");
			m_Dujie = rc.GetComp<Button>("Button_Dujie");
			m_HuiFu = rc.GetComp<Button>("Button_HuiFu");
			m_Tupo = rc.GetComp<Button>("Button_Tupo");
			m_XXBg = rc.GetComp<Image>("Image_XXBg");
			m_Lv = rc.GetComp<TMP_Text>("TMP_Text_Lv");
		}
	}
}
