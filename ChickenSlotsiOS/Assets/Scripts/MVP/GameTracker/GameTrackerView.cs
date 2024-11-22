using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameTrackerView : View
{
    public event Action<int, int> OnSelectGame;

    [SerializeField] private List<GameTrackerConstruct> gameTrackerConstructs;

    public void Initialize()
    {
        for (int i = 0; i < gameTrackerConstructs.Count; i++)
        {
            //gameTrackerConstructs[i].OnChooseLevel += HandlerChooseLevel;
            gameTrackerConstructs[i].Initialize();
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < gameTrackerConstructs.Count; i++)
        {
            //gameTrackerConstructs[i].OnChooseLevel -= HandlerChooseLevel;
            gameTrackerConstructs[i].Dispose();
        }
    }

    public void CurrentLevel(int level)
    {

        Debug.Log("Current level - " + level);
        gameTrackerConstructs.FirstOrDefault(cnstr => cnstr.Level == level + 1).CurrentLevel();
    }

    public void AvailableLevel(int level)
    {
        Debug.Log("Available level - " + level);
        gameTrackerConstructs.FirstOrDefault(cnstr => cnstr.Level == level + 1).AvailableLevel();
    }

    public void UnavailableLevel(int level)
    {
        Debug.Log("Unavailable level - " + level);
        gameTrackerConstructs.FirstOrDefault(cnstr => cnstr.Level == level + 1).UnavailableLevel();
    }

    #region Input

    private void HandlerChooseLevel(int level, int typeGame)
    {
        OnSelectGame(level, typeGame);
    }

    #endregion
}
