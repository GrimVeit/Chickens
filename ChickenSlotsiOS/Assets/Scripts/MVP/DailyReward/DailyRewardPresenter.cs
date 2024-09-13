using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardPresenter
{
    private DailyRewardModel dailyRewardModel;
    private DailyRewardView dailyRewardView;

    public DailyRewardPresenter(DailyRewardModel dailyRewardModel, DailyRewardView dailyRewardView)
    {
        this.dailyRewardModel = dailyRewardModel;
        this.dailyRewardView = dailyRewardView;
    }

    public void Initialize()
    {
        dailyRewardView.OnClickDailyReward += dailyRewardModel.DailyReward;

        dailyRewardModel.Initialize();
        dailyRewardView.Initialize();

    }

    public void Dispose()
    {
        dailyRewardView.OnClickDailyReward -= dailyRewardModel.DailyReward;

        dailyRewardModel.Dispose();
        dailyRewardView.Dispose();
    }

    #region Input Actions

    public event Action<int> OnGetDailyReward_Count
    {
        add { dailyRewardModel.OnGetDailyReward_Count += value; }
        remove { dailyRewardModel.OnGetDailyReward_Count -= value; }
    }

    public event Action OnGetDailyReward
    {
        add { dailyRewardModel.OnGetDailyReward += value; }
        remove { dailyRewardModel.OnGetDailyReward -= value; }
    }

    //public event Action OnSetAvailableDailyReward
    //{
    //    add { dailyRewardModel.OnSetAvailableDailyReward += value; }
    //    remove { dailyRewardModel.OnSetAvailableDailyReward -= value; }
    //}

    //public event Action OnSetUnvailableDailyReward
    //{
    //    add { dailyRewardModel.OnSetUnvailableDailyReward += value; }
    //    remove { dailyRewardModel.OnSetUnvailableDailyReward -= value; }
    //}

    #endregion
}
