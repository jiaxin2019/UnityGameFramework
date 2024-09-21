using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using StarForce;
using UniRx;
using UnityEngine;

namespace StarForce
{
    public class XiuXingFormPresenter : MonoBehaviour
    {
        private XiuXingForm m_XiuXingForm;
        private DataXiuXing m_DataXiuXing;
        // Start is called before the first frame update
        void Start()
        {
            m_XiuXingForm = GetComponent<XiuXingForm>();
            m_DataXiuXing = GameEntry.Data.GetData<DataXiuXing>();
            OnClick();
            AddEvent();
            iniData();
        }

        private void OnClick()
        {
            m_XiuXingForm.m_Back.OnClickAsObservable().Subscribe(_=>OnCloseForm()).AddTo(this);
            m_XiuXingForm.m_Help.OnClickAsObservable().Subscribe(_=>OnHelp()).AddTo(this);
            m_XiuXingForm.m_Yin.OnClickAsObservable().Subscribe(_=>OnYin()).AddTo(this);
            m_XiuXingForm.m_BookChange.OnClickAsObservable().Subscribe(_=>OnBookChange()).AddTo(this);
            m_XiuXingForm.m_Tupo.OnClickAsObservable().Subscribe(_=>OnTuPO()).AddTo(this);
            m_XiuXingForm.m_Dujie.OnClickAsObservable().Subscribe(_=>OnDujie()).AddTo(this);
            m_XiuXingForm.m_HuiFu.OnClickAsObservable().Subscribe(_=>OnHuiFu()).AddTo(this);

        }

        private void OnHuiFu()
        {
            
        }

        private void OnDujie()
        {
            GameEntry.UI.OpenUIForm(EnumUIForm.XiuXingDujieForm);
        }
        
        [Button]
        private void OnTuPO()
        {
            
            Debug.Log("OnTuPO");
        }
        
       
        /// <summary>
        /// 切换 突破按钮状态
        /// </summary>
        /// <param name="b"></param>
        private void ChaTuPoBtn(bool b)
        {
            m_XiuXingForm.m_Tupo.gameObject.SetActive(b);
        }
        
        /// <summary>
        /// 切换 渡劫按钮状态
        /// </summary>
        /// <param name="b"></param>
        private void ChaDujieBtn(bool b)
        {
            m_XiuXingForm.m_Dujie.gameObject.SetActive(b);
        }
        
        /// <summary>
        /// 切换 恢复按钮状态
        /// </summary>
        /// <param name="b"></param>
        private void ChaHuiFuBtn(bool b)
        {
            m_XiuXingForm.m_HuiFu.gameObject.SetActive(b);
        }
        
        [Button]
        private void OnBookChange()
        {
            Debug.Log("切换功法");
        }
        
        [Button]
        private void OnYin()
        {
            Debug.Log("引气");
        }

        
        [Button]
        private void OnHelp()
        {
            Debug.Log("关闭");
        }
        
        [Button]
        private void OnCloseForm()
        {
            m_XiuXingForm.Close();
        }
        
        private void AddEvent()
        {
            
        }

        private void iniData()
        {
            m_XiuXingForm.m_LvTittle.text = m_DataXiuXing.CurPlayerStage(EStateType.Type2).Level.ToString();
        }
        
    }
}

