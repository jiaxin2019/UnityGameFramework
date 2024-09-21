using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ILRuntime.Runtime;
using NKG.NetWork;
using StarForce;
using UnityEngine;
using Xiu;

namespace StarForce
{
    public sealed class DataXiuXing : DataBase
    {
        private List<NKG. NetWork. PlayerStage> m_PlayerStage = null;

        public NKG.NetWork.PlayerStage CurPlayerStage(EStateType type)
        {
            NKG.NetWork.PlayerStage playerStage = PlayerStage.Find(value => value.Type == type.ToInt32());
            return playerStage;
        }
        
        public List<NKG. NetWork. PlayerStage> PlayerStage
        {
            get
            {
                m_PlayerStage = GameEntry.Data.GetData<DataPlayer>().PlayerStage;
                return m_PlayerStage;
            }

            private set
            {
                m_PlayerStage = value;
            }
        }
        // Start is called before the first frame update
        protected override void OnLoad()
        {
            GameEntry.Net.AddHandler(ClientHotfixOpcode.EnterGameMsgResponse,OnEnterGameMsgResponse);
        }

        public void EnterGameMsgRes()
        {
            EnterGameMsgRequest msg = new EnterGameMsgRequest();
            GameEntry.Net.Send(msg, ENetState.TC);
            Debug.Log($"进入游戏请求");
        }

        private void OnEnterGameMsgResponse(object sender, EventArgs e)
        {
            EnterGameMsgResponse ne = sender as EnterGameMsgResponse;
            if (ne == null)
            {
                return;
            }
            Debug.Log($"进入游戏请求返回：{ne.ToString()}");
        }
    }

}
