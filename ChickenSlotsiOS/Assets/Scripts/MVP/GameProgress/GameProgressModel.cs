using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameProgressModel
{
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

        //for (int i = 0; i < Datas.Count; i++)
        //{
        //    Debug.Log($"NumberGame - {Datas[i].Number}, Unlocked - {Datas[i].IsOpen}");
        //}

        OnGetData?.Invoke(Datas);
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
            return;
        }
    }

    public void SelectGame(int number)
    {
        var gameData = Datas.FirstOrDefault(gd => gd.Number == number);

        if(gameData != null && !gameData.isSelect)
        {
            gameData.isSelect = true;
        }
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
