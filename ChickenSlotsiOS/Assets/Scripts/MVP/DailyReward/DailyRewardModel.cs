using System;
using System.Collections;
using UnityEngine;

public class DailyRewardModel
{
    public event Action<int> OnGetDailyReward_Count;
    public event Action OnGetDailyReward;

    private ISoundProvider soundProvider;
    private IParticleEffectProvider particleEffectProvider;

    private int reward = 1000;

    public DailyRewardModel(ISoundProvider soundProvider, IParticleEffectProvider particleEffectProvider)
    {
        this.soundProvider = soundProvider;
        this.particleEffectProvider = particleEffectProvider;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public void DailyReward()
    {
        //particleEffectProvider.Play("DailyReward");
        //soundProvider.PlayOneShot("DailyReward");
        OnGetDailyReward?.Invoke();
        OnGetDailyReward_Count?.Invoke(reward);
    }
}
