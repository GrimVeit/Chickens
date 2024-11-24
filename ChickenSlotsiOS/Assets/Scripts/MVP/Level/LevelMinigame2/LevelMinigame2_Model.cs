using System;
using System.Linq;

public class LevelMinigame2_Model
{
    public event Action<float, float, float> OnSetSpawnerData;
    public event Action<int> OnChooseLevel;

    public LevelMinigame2_Datas minigame1_LevelDatas;

    public LevelMinigame2_Model(LevelMinigame2_Datas datas)
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
