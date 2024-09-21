//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace StarForce
{
    /// <summary>
    /// 界面编号。
    /// </summary>
    public enum UIFormId11 : byte
    {
        Undefined = 0,

        /// <summary>
        /// 弹出框。
        /// </summary>
        DialogForm = 1,
        /// <summary>
        /// 热更测试
        /// </summary>
        TestForm2 ,
        /// <summary>
        /// 登录界面
        /// </summary>
        LoginView,
        /// <summary>
        /// 主Ui
        /// </summary>
        MainUIForm,
        
        /// <summary>
        /// 主菜单。
        /// </summary>
        MenuForm = 100,

        /// <summary>
        /// 设置。
        /// </summary>
        SettingForm = 101,

        /// <summary>
        /// 关于。
        /// </summary>
        AboutForm = 102,
        
        /// <summary>
        /// 修行
        /// </summary>
        XiuXingForm = 103,
        /// <summary>
        /// 渡劫
        /// </summary>
        XiuXingDujieForm,
        /// <summary>
        ///  渡劫 成功
        /// </summary>
        XiuXingDjSucForm,
        /// <summary>
        ///  渡劫 失败
        /// </summary>
        XiuXingDjFailForm,
        /// <summary>
        /// 破镜 成功
        /// </summary>
        XiuXingPjSucForm,
        /// <summary>
        /// 破镜 失败
        /// </summary>
        XiuXingPjFailForm,
        /// <summary>
        /// 引气 成功
        /// </summary>
        XiuXingYqSucForm,
        /// <summary>
        /// 引气 失败
        /// </summary>
        XiuXingYqFailForm,
        /// <summary>
        /// 入定
        /// </summary>
        XiuXingRdSucForm
        
    }
}
