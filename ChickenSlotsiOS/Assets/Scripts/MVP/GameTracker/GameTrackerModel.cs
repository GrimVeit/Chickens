using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameTrackerModel
{
    public event Action OnGoToMiniGame1;
    public event Action OnGoToMiniGame2;
    public event Action OnGoToMiniGame3;

    public event Action<int> OnSelectGame;

    public event Action<int> OnAvailableLevel;
    public event Action<int> OnUnavailableLevel;
    public event Action<int> OnCurrentLevel;

    private List<GameData> gameDatas = new List<GameData>();

    private ISpriteAnimator spriteAnimator;

    public GameTrackerModel(ISpriteAnimator spriteAnimator)
    {
        this.spriteAnimator = spriteAnimator;
    }

    public void SetData(List<GameData> data)
    {
        gameDatas = data;

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
                        spriteAnimator.StartAnimator($"Chicken_{gameDatas[i].Number}");
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
        var gameData = gameDatas.FirstOrDefault(gr => gr.Number == level - 1);

        if (gameData == null)
            Debug.LogWarning($"Not found {level} level");

        if (!gameData.IsOpen)
            Debug.Log($"Level {level} not open");

        OnSelectGame?.Invoke(gameData.Number);

        switch (typeGame)
        {
            case 0:
                OnGoToMiniGame1?.Invoke(); break;
            case 1:
                OnGoToMiniGame2?.Invoke(); break;
            case 2:
                OnGoToMiniGame3?.Invoke(); break;
            default: 
                Debug.LogError($"Game {typeGame} not found"); break;
        }
    }
}
