using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class LevelMinigame1_Model : MonoBehaviour
{
    public event Action<float, float, float> OnSetSpawnerData;
    public event Action<int> OnChooseLevel;

    public LevelMinigame1_Datas minigame1_LevelDatas;

    public LevelMinigame1_Model(LevelMinigame1_Datas datas)
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
