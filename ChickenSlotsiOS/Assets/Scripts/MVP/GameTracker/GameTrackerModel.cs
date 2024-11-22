using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameTrackerModel
{
    public event Action<int> OnAvailableLevel;
    public event Action<int> OnUnavailableLevel;
    public event Action<int> OnCurrentLevel;

    private List<GameData> gameDatas = new List<GameData>();

    public void SetData(List<GameData> data)
    {
        gameDatas = data;

        for (int i = 0; i < gameDatas.Count; i++)
        {
            Debug.Log($"NumberGame - {gameDatas[i].Number}, Unlocked - {gameDatas[i].IsOpen}");
        }

        GameData currentGameData = gameDatas.LastOrDefault(level => level.IsOpen);

        Debug.Log(currentGameData.Number);

        if (currentGameData != null)
        {
            int curentLevelIndex = gameDatas.IndexOf(currentGameData);
            OnCurrentLevel?.Invoke(curentLevelIndex);

            for (int i = 0; i < gameDatas.Count; i++)
            {
                if(curentLevelIndex != i)
                {
                    if (gameDatas[i].IsOpen)
                    {
                        OnAvailableLevel?.Invoke(i);
                    }
                    else
                    {
                        OnUnavailableLevel?.Invoke(i);
                    }
                }
            }

            Debug.Log("curentLevelIndex");
        }
    }

    public void SelectGame(int level, int typeGame)
    {
        var gameData = gameDatas.FirstOrDefault(gr => gr.Number == level);

        if (gameData == null)
            Debug.LogWarning($"Not found {level} level");

        if (!gameData.IsOpen)
            Debug.Log($"Level {level} not open");
    }
}
