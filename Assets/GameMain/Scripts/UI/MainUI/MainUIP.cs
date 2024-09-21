using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using StarForce;
using UniRx;
using UnityEngine;

public class MainUIP : MonoBehaviour
{
    private MainUIForm m_MainUIform;
    private DataPlayer m_DataPlayer;
    // Start is called before the first frame update
    void Start()
    {
        m_MainUIform = GetComponent<MainUIForm>();
        m_DataPlayer = GameEntry.Data.GetData<DataPlayer>();
        
        m_MainUIform.m_JiaoSe.OnClickAsObservable().Subscribe(_=>OnClickJiaoSe()).AddTo(this);
        m_MainUIform.m_Shezhi.OnClickAsObservable().Subscribe(_=>OnClickShezhi()).AddTo(this);
        m_MainUIform.m_Xiuxing.OnClickAsObservable().Subscribe(_=>OnClickXiuxing()).AddTo(this);
    }

    private void OnClickJiaoSe()
    {
        Debug.Log("m_DataPlayer");
    }
    
    private void OnClickShezhi()
    {
        // GameEntry.UI.OpenUIForm(UIFormId.SettingForm);
    }
    [Button]
    private void OnClickXiuxing()
    {
        Debug.Log("xiuxin");
        GameEntry.UI.OpenUIForm(EnumUIForm.XiuXingForm);
    }
}

