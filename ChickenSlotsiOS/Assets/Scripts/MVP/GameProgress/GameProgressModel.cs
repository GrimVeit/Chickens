using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameProgressModel
{
    public event Action OnGoToGame1;
    public event Action OnGoToGame2;
    public event Action OnGoToGame3;

    public event Action OnUnlockSecondGame;
    public event Action OnNoneUnlockSecondGame;

    public event Action<int> OnGetSelectGame;
    public event Action<List<GameData>> OnGetData;

    public readonly string FilePath = Path.Combine(Application.persistentDataPath, "GameDatas.json");

    [SerializeField] private List<GameData> Datas = new List<GameData>();

    public void Initialize()
    {
        if (File.Exists(FilePath))
        {
            string loadedJson = File.ReadAllText(FilePath);
            GameDatas gameDatas = JsonUtility.FromJson<GameDatas>(loadedJson);

            Debug.Log("Success");

            Datas = gameDatas.Datas.ToList();
        }
        else
        {
            Datas = new List<GameData>();

            for (int i = 0; i < 30; i++)
            {
                if(i == 0)
                {
                    Datas.Add(new GameData(i, true, false, i % 3, false));
                }
                else
                {
                    Datas.Add(new GameData(i, false, false, i % 3, false));
                }
            }
        }


        Debug.Log("Start Game:");
        for (int i = 0; i < Datas.Count; i++)
        {
            Debug.Log($"NumberGame - {Datas[i].Number}, Unlocked - {Datas[i].IsOpen}, Select - {Datas[i].IsSelect}");
        }

        OnGetData?.Invoke(Datas);
        OnGetSelectGame?.Invoke(SelectedGame().Number);
    }

    public void Dispose()
    {
        string json = JsonUtility.ToJson(new GameDatas(Datas.ToArray()));
        File.WriteAllText(FilePath, json);
    }

    public void UnselectAll()
    {
        Datas
            .Where(data => data.IsSelect)
            .ToList()
            .ForEach(data => data.IsSelect = false);

        Debug.Log("Unselect All:");
        for (int i = 0; i < Datas.Count; i++)
        {
            Debug.Log($"NumberGame - {Datas[i].Number}, Unlocked - {Datas[i].IsOpen}, Select - {Datas[i].IsSelect}");
        }
    }

    public void UnlockGame(int number)
    {
        var gameData = Datas.FirstOrDefault(gd => gd.Number == number);

        if (gameData != null && !gameData.IsOpen)
        {
            gameData.IsOpen = true;

            Debug.Log("Unlock Game:" + number);
            for (int i = 0; i < Datas.Count; i++)
            {
                Debug.Log($"NumberGame - {Datas[i].Number}, Unlocked - {Datas[i].IsOpen}, Select - {Datas[i].IsSelect}");
            }

            return;
        }
    }

    public void UnlockSecondGame()
    {
        if(Datas.IndexOf(SelectedGame()) == Datas.Count - 1)
        {
            OnNoneUnlockSecondGame?.Invoke();

            return;
        }

        var gameData = Datas[Datas.IndexOf(SelectedGame()) + 1];

        if (gameData != null)
        {
            OnUnlockSecondGame?.Invoke();

            gameData.IsOpen = true;

            Debug.Log("Unlock Game:" + gameData.Number);
            for (int i = 0; i < Datas.Count; i++)
            {
                Debug.Log($"NumberGame - {Datas[i].Number}, Unlocked - {Datas[i].IsOpen}, Select - {Datas[i].IsSelect}");
            }

            return;
        }
    }

    public void OpenGame(int number)
    {
        UnselectAll();

        var gameData = Datas.FirstOrDefault(gd => gd.Number == number);

        if (gameData != null && !gameData.IsSelect)
        {
            gameData.IsSelect = true;

            Debug.Log("Select Game:" + number);
            for (int i = 0; i < Datas.Count; i++)
            {
                Debug.Log($"NumberGame - {Datas[i].Number}, Unlocked - {Datas[i].IsOpen}, Select - {Datas[i].IsSelect}");
            }

            OpenScene(gameData.Scene);
        }
    }

    public void OpenSecondGame()
    {
        var game = Datas[SelectedGame().Number + 1];

        if(game != null)
        {
            UnselectAll();

            game.IsSelect = true;

            OpenScene(game.Scene);
        }
    }

    public void CompleteGame()
    {
        SelectedGame().IsComplete = true;
    }

    public GameData SelectedGame()
    {
        return Datas.FirstOrDefault(data => data.IsSelect);
    }

    private void OpenScene(int scene)
    {
        switch (scene)
        {
            case 0:
                OnGoToGame1?.Invoke();
                break;
            case 1:
                OnGoToGame2?.Invoke();
                break;
            case 2:
                OnGoToGame3?.Invoke();
                break;
        }
    }
}

[Serializable]
public class GameData
{
    public int Number;
    public bool IsOpen;
    public bool IsSelect;
    public bool IsComplete;
    public int Scene;

    public GameData(int number, bool isOpen, bool isSelect, int scene, bool isComplete)
    {
        Number = number;
        IsOpen = isOpen;
        IsSelect = isSelect;
        Scene = scene;
        IsComplete = isComplete;
    }
}

[System.Serializable]
public class GameDatas
{
    public GameData[] Datas;

    public GameDatas(GameData[] datas)
    {
        Datas = datas;
    }
}
