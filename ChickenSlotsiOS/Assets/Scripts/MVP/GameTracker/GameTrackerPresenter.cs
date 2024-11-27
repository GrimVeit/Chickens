using System;
using System.Collections.Generic;

public class GameTrackerPresenter
{
    private GameTrackerModel gameTrackerModel;
    private GameTrackerView gameTrackerView;

    public GameTrackerPresenter(GameTrackerModel gameTrackerModel, GameTrackerView gameTrackerView)
    {
        this.gameTrackerModel = gameTrackerModel;
        this.gameTrackerView = gameTrackerView;
    }

    public void Initialize()
    {
        ActivateEvents();

        gameTrackerView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        gameTrackerView.Dispose();
    }

    private void ActivateEvents()
    {
        gameTrackerView.OnSelectGame += gameTrackerModel.SelectGame;

        gameTrackerModel.OnAvailableLevel += gameTrackerView.AvailableLevel;
        gameTrackerModel.OnUnavailableLevel += gameTrackerView.UnavailableLevel;
        gameTrackerModel.OnCurrentLevel += gameTrackerView.CurrentLevel;
    }

    private void DeactivateEvents()
    {
        gameTrackerView.OnSelectGame -= gameTrackerModel.SelectGame;

        gameTrackerModel.OnAvailableLevel -= gameTrackerView.AvailableLevel;
        gameTrackerModel.OnUnavailableLevel -= gameTrackerView.UnavailableLevel;
        gameTrackerModel.OnCurrentLevel -= gameTrackerView.CurrentLevel;
    }

    #region Input

    public event Action<int> OnSelectGame
    {
        add { gameTrackerModel.OnSelectGame += value; }
        remove { gameTrackerModel.OnSelectGame -= value; }
    }

    public void SetData(List<GameData> data)
    {
        gameTrackerModel.SetData(data);
    }

    #endregion
}
