using System;
using Unity.Mathematics;
using UnityEngine;

public class GameHistoryModel
{
    public event Action OnNoneLastGame;
    public event Action OnLastGameIsArcada;
    public event Action OnLastGameIsCampaign;

    private readonly string key;

    private TypeGame lastTypeGame;

    public GameHistoryModel(string key)
    {
        this.key = key;
    }

    public void Initialize()
    {
        if (!PlayerPrefs.HasKey(key))
        {
            lastTypeGame = TypeGame.None;
            OnNoneLastGame?.Invoke();
        }
        else
        {
            if(PlayerPrefs.GetInt(key) == (int)TypeGame.Arcada)
            {
                lastTypeGame = TypeGame.Arcada;
                OnLastGameIsArcada?.Invoke();
            }
            else if(PlayerPrefs.GetInt(key) == (int)TypeGame.Campaign)
            {
                lastTypeGame = TypeGame.Campaign;
                OnLastGameIsCampaign?.Invoke();
            }
        }
    }

    public void Dispose()
    {

    }

    public void SetCurrentTypeGame(TypeGame type)
    {
        lastTypeGame = type;
        PlayerPrefs.SetInt(key, (int)type);
    }

    public void DeleteHistory()
    {
        PlayerPrefs.DeleteKey(key);
    }
}

public enum TypeGame
{
    None = 0, 
    Arcada = 1, 
    Campaign = 2
}
