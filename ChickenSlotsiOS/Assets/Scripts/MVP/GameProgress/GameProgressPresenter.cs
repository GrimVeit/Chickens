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

    public void UnlockSecondGame()
    {
        gameProgressModel.UnlockSecondGame();
    }

    public void UnlockGame(int number)
    {
        gameProgressModel.UnlockGame(number);
    }

    public void SelectGame(int number)
    {
        gameProgressModel.SelectGame(number);
    }

    public void UnselectAll()
    {
        gameProgressModel.UnselectAll();
    }

    public event Action<int> OnGetSelectGame
    {
        add { gameProgressModel.OnGetSelectGame += value; }
        remove { gameProgressModel.OnGetSelectGame -= value; }
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
