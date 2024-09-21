using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameFramework.Data;
using GameFramework.DataTable;
using NKG.NetWork;
using UnityGameFramework.Runtime;
using Xiu;

namespace StarForce
{
    public sealed class DataPlayer : DataBase
    {
        // 角色id
        public long PlayerId { get; set; }
        // 昵称
        public string NickName { get; set; }
        // 性别
        public int Sex { get; set; }
        // 性别
        public long Age { get; set; }
        //寿元上限
        public long MaxAge { get; set; }
        //当前境界类型
        public  int CurStageType { get; set; }
        //境界列表
        List<PlayerStage> PlayerStages { get; set; }
        //称号
        public int Title { get; set; }
        //头像框
        public int HeadFrame { get; set; }
        //头像
        public int Avatar { get; set; }
        //时装
        public int  Fashion { get; set; }
        //称号集合key-id value-有效期
        public Dictionary<int, long> TitleMap { get; set; }
        //头像集合
        public List<int> Avatars { get; set; }
        //头像框集合
        public List<int> HeadFrames { get; set; }
        //时装集合
        public List<int>  Fashions { get; set; }
        public Dictionary<int, FunctionInfo> Functions { get; set; }
        //客户端数据
        public Dictionary<string, string> ClientData { get; set; }
        //下次刷新时间
        public long DayRefreshDate  { get; set; }
        //下次月刷新时间
        public long MonthRefreshDate  { get; set; }
        //下次周刷新时间
        public long  WeekRefreshData { get; set; }
        //创建角色时间
        public long CreateTime { get; set; }
        //下次增加年龄时间
        public long NextAddAgeTime { get; set; }
        //道具
        public Bag Bag { get; set; }
        //灵根集合
        public List<int> LingGens { get; set; }
        //玩家属性集合
        public Dictionary<int, long> TotalAttrs { get; set; }
        //玩家 各类修行的 数据（默认是练气）
        public List<NKG. NetWork. PlayerStage>  PlayerStage { get; set; }
        
        public int HP { get; private set; }

        private float energy;

        public float Energy
        {
            get
            {
                //DataLevel dataLevel = GameEntry.Data.GetData<DataLevel>();
                //if (!dataLevel.IsInLevel)
                //{
                //    Log.Error("Is invaild to get player energy outsiede level scene");
                //    return 0;
                //}

                return energy;
            }

            private set
            {
                energy = value;
            }
        }


        public bool IsEnableDebugEnergy { get; private set; }
        public float DebugAddEnergyCount { get; private set; }

        protected override void OnInit()
        {
        }

        protected override void OnPreload()
        {
            
        }

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
            this.PlayerId = ne.PlayerId;
            this.NickName = ne.NickName;
            this.Sex = ne.Sex;
            this.Age = ne.Age;
            this.MaxAge = ne.MaxAge;
            this.CurStageType = ne.CurStageType;
            this.Title = ne.Title;
            this.HeadFrame = ne.HeadFrame;
            this.Avatar = ne.Avatar;
            this.Fashion = ne.Fashion;
            this.TitleMap =  ne.TitleMap.ToDictionary(entry => entry.Key, entry => entry.Value);
            this.Avatars = ne.Avatars.ToList();
            this.HeadFrames = ne.HeadFrames.ToList();
            this.Fashions = ne.Fashions.ToList();
            this.Functions = ne.Functions.ToDictionary(entry => entry.Key, entry => mapToFunctionInfo(entry.Value));
            this.ClientData = ne.ClientData.ToDictionary(entry => entry.Key, entry => entry.Value);
            this.DayRefreshDate = ne.DayRefreshDate;
            this.MonthRefreshDate= ne.MonthRefreshDate;
            this.WeekRefreshData= ne.WeekRefreshData;
            this.CreateTime= ne.CreateTime;
            this.NextAddAgeTime= ne.NextAddAgeTime;
            // this.Bag = ne.Bag.ToDictionary(entry => entry.Key, entry => MapToBag(entry.Value));
            this.LingGens = ne.LingGens.ToList();
            this.TotalAttrs = ne.TotalAttrs.ToDictionary(entry => entry.Key, entry => entry.Value);
            this.PlayerStage = ne.PlayerStages.ToList();
        }

        private FunctionInfo mapToFunctionInfo(NKG.NetWork.FunctionInfo oldValue)
        {
            FunctionInfo newValue = new FunctionInfo();
            newValue.Id = oldValue.Id;
            return newValue;
        }
    
        private Bag MapToBag(NKG.NetWork.Bag oldValue)
        {
            Bag newValue = new Bag();
            newValue.BagNum = oldValue.BagNum;
            return newValue;
        }
        
        public void Damage(int value)
        {
            if (value == 0)
                return;

            int lastHP = HP;
            HP -= value;

            bool gameover = false;

            if (HP <= 0)
            {
                HP = 0;
                gameover = true;
            }

            //GameEntry.Event.Fire(this, PlayerHPChangeEventArgs.Create(lastHP, HP));

            if (gameover)
                GameOver();
        }

        public void AddEnergy(float value)
        {
            if (value == 0)
                return;

            float lastEnergy = Energy;
            Energy += value;

            //GameEntry.Event.Fire(this, PlayerEnergyChangeEventArgs.Create(lastEnergy, Energy));
        }

        public void DebugAddEnergy()
        {
            AddEnergy(DebugAddEnergyCount);
        }

        public void Reset()
        {
            int lastHP = HP;
            //HP = GameEntry.Config.GetInt(Constant.Config.PlayerHP);
            //HP = 100;
            //GameEntry.Event.Fire(this, PlayerHPChangeEventArgs.Create(lastHP, HP));

            float lastEnergy = Energy;
            //DataLevel dataLevel = GameEntry.Data.GetData<DataLevel>();
            //if (!dataLevel.IsInLevel)
            //{
            //    Log.Error("Is invaild to get player energy outsiede level scene");
            //    Energy = lastEnergy;
            //}
            //else
            //{
            //    LevelData levelData = dataLevel.GetLevelData(dataLevel.CurrentLevelIndex);
            //    Energy = levelData.InitEnergy;
            //}

            //GameEntry.Event.Fire(this, PlayerEnergyChangeEventArgs.Create(lastEnergy, Energy));
        }

        public bool BuyTower(int towerId)
        {
            return false;
        }

        public void SellTower()
        {

        }

        private void GameOver()
        {
            //GameEntry.Data.GetData<DataLevel>().GameFail();
        }

        protected override void OnUnload()
        {
        }

        protected override void OnShutdown()
        {

        }
    }
    public class PlayerStage
    {
        public int Type { get; set; }
        //境界
        public int Stage { get; set; }
        //重
        public int Level { get; set; }
        //获得的经验值
        public long Exp { get; set; }
    }
    
    public class FunctionInfo{
        //功能ID
        public int Id  { get; set; }
        //是否解锁
        public bool Unlock  { get; set; }
    }
    
    public class Bag{
        public int BagNum  { get; set; }
        //道具集合
        public  List<Prop> Props { get; set; }
    }
    
    public class  Prop{
        public  int Id  { get; set; }
        public  long Num  { get; set; }
    }

    public enum EStateType:int
    {
        //修行类型：1 -炼体
        Type1 = 1,
        //修行类型：2-练气
        Type2,
        //修行类型：3 - 练识
        Type3
    }
}


