using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelMinigame3_Model
{
    public event Action<float, float, float> OnSetSpawnerData;
    public event Action<int> OnChooseLevel;

    public LevelMinigame3_Datas minigame1_LevelDatas;

    public LevelMinigame3_Model(LevelMinigame3_Datas datas)
    {
        this.minigame1_LevelDatas = datas;
    }

    public void ChooseLevel(int level)
    {
        var data = minigame1_LevelDatas.Datas.FirstOrDefault(data => data.Level == level + 1);

        OnChooseLevel?.Invoke(data.Level);
        OnSetSpawnerData?.Invoke(data.InitialDelay, data.MinDelay, data.DecreaseAmount);
    }
}
