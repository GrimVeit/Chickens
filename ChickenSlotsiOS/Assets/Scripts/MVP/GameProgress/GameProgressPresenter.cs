using System;
using System.Collections.Generic;

public class GameProgressPresenter : IGameUnlocker
{
    private GameProgressModel gameProgressModel;

    public GameProgressPresenter(GameProgressModel gameProgressModel)
    {
        this.gameProgressModel = gameProgressModel;
    }

    public void Initialize()
    {
        gameProgressModel.Initialize();
    }

    public void Dispose()
    {
        gameProgressModel.Dispose();
    }

    #region Input

    public void UnlockGame(int number)
    {
        gameProgressModel.UnlockGame(number);
    }

    public void SelectGame(int number)
    {
        gameProgressModel.SelectGame(number);
    }

    public event Action<List<GameData>> OnGetData
    {
        add { gameProgressModel.OnGetData += value; }
        remove { gameProgressModel.OnGetData -= value; }
    }

    #endregion
}

public interface IGameUnlocker
{
    public void UnlockGame(int number);
}
