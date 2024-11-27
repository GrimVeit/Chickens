using System;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressPresenter : IGameUnlocker
{
    private GameProgressModel gameProgressModel;
    private IGameProgressView gameProgressView;

    public GameProgressPresenter(GameProgressModel gameProgressModel, IGameProgressView gameProgressView)
    {
        this.gameProgressModel = gameProgressModel;
        this.gameProgressView = gameProgressView;
    }

    public void Initialize()
    {
        ActivateEvents();

        gameProgressModel.Initialize();
        gameProgressView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        gameProgressModel.Dispose();
        gameProgressView.Dispose();
    }

    private void ActivateEvents()
    {
        gameProgressView.OnClickToSecondGame += gameProgressModel.OpenSecondGame;

        gameProgressModel.OnNoneUnlockSecondGame += gameProgressView.DeactivateSecondGameButton;
        gameProgressModel.OnUnlockSecondGame += gameProgressView.ActivateSecondGameButton;
    }

    private void DeactivateEvents()
    {
        gameProgressView.OnClickToSecondGame += gameProgressModel.OpenSecondGame;

        gameProgressModel.OnNoneUnlockSecondGame += gameProgressView.DeactivateSecondGameButton;
        gameProgressModel.OnUnlockSecondGame += gameProgressView.ActivateSecondGameButton;
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

    public void OpenGame(int number)
    {
        gameProgressModel.OpenGame(number);
    }

    public void OpenSecondGame()
    {
        gameProgressModel.OpenSecondGame();
    }

    public void UnselectAll()
    {
        gameProgressModel.UnselectAll();
    }

    public event Action OnGoToGame1
    {
        add { gameProgressModel.OnGoToGame1 += value; }
        remove { gameProgressModel.OnGoToGame1 -= value; }
    }

    public event Action OnGoToGame2
    {
        add { gameProgressModel.OnGoToGame2 += value; }
        remove { gameProgressModel.OnGoToGame2 -= value; }
    }

    public event Action OnGoToGame3
    {
        add { gameProgressModel.OnGoToGame3 += value; }
        remove { gameProgressModel.OnGoToGame3 -= value; }
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

public interface IGameProgressView
{
    public event Action OnClickToSecondGame;

    public void Initialize();
    public void Dispose();

    public void ActivateSecondGameButton();

    public void DeactivateSecondGameButton();
}
