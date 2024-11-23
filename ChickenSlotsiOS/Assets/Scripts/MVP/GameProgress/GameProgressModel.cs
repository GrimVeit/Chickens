using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameProgressModel
{
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
                    Datas.Add(new GameData(i, true, false));
                }
                else
                {
                    Datas.Add(new GameData(i, false, false));
                }
            }
        }


        Debug.Log("Start Game:");
        for (int i = 0; i < Datas.Count; i++)
        {
            Debug.Log($"NumberGame - {Datas[i].Number}, Unlocked - {Datas[i].IsOpen}, Select - {Datas[i].isSelect}");
        }

        OnGetData?.Invoke(Datas);
        OnGetSelectGame?.Invoke(SelectGame());
    }

    public void Dispose()
    {
        string json = JsonUtility.ToJson(new GameDatas(Datas.ToArray()));
        File.WriteAllText(FilePath, json);
    }

    public void UnlockGame(int number)
    {
        var gameData = Datas.FirstOrDefault(gd => gd.Number == number);

        if(gameData != null && !gameData.IsOpen)
        {
            gameData.IsOpen = true;
            OnGetData?.Invoke(Datas);

            Debug.Log("Unlock Game:" + number);
            for (int i = 0; i < Datas.Count; i++)
            {
                Debug.Log($"NumberGame - {Datas[i].Number}, Unlocked - {Datas[i].IsOpen}, Select - {Datas[i].isSelect}");
            }

            return;
        }
    }

    public void SelectGame(int number)
    {
        UnselectAll();

        var gameData = Datas.FirstOrDefault(gd => gd.Number == number);

        if(gameData != null && !gameData.isSelect)
        {
            gameData.isSelect = true;

            Debug.Log("Select Game:" + number);
            for (int i = 0; i < Datas.Count; i++)
            {
                Debug.Log($"NumberGame - {Datas[i].Number}, Unlocked - {Datas[i].IsOpen}, Select - {Datas[i].isSelect}");
            }
        }
    }

    public void UnselectAll()
    {
        Datas
            .Where(data => data.isSelect)
            .ToList()
            .ForEach(data => data.isSelect = false);

        Debug.Log("Unselect All:");
        for (int i = 0; i < Datas.Count; i++)
        {
            Debug.Log($"NumberGame - {Datas[i].Number}, Unlocked - {Datas[i].IsOpen}, Select - {Datas[i].isSelect}");
        }
    }

    public void UnlockSecondGame()
    {
        var gameData = Datas.FirstOrDefault(data => data.isSelect);

        Datas[Datas.IndexOf(gameData) + 1].IsOpen = true;

        Debug.Log("Unlock Second Game:");
        for (int i = 0; i < Datas.Count; i++)
        {
            Debug.Log($"NumberGame - {Datas[i].Number}, Unlocked - {Datas[i].IsOpen}, Select - {Datas[i].isSelect}");
        }
    }

    public int SelectGame()
    {
        return Datas.FirstOrDefault(data => data.isSelect).Number;
    }
}

[System.Serializable]
public class GameData
{
    public int Number;
    public bool IsOpen;
    public bool isSelect;

    public GameData(int number, bool isOpen, bool isSelect)
    {
        Number = number;
        IsOpen = isOpen;
        this.isSelect = isSelect;
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
