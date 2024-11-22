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
        gameTrackerModel.OnAvailableLevel += gameTrackerView.AvailableLevel;
        gameTrackerModel.OnUnavailableLevel += gameTrackerView.UnavailableLevel;
        gameTrackerModel.OnCurrentLevel += gameTrackerView.CurrentLevel;
    }

    private void DeactivateEvents()
    {
        gameTrackerModel.OnAvailableLevel -= gameTrackerView.AvailableLevel;
        gameTrackerModel.OnUnavailableLevel -= gameTrackerView.UnavailableLevel;
        gameTrackerModel.OnCurrentLevel -= gameTrackerView.CurrentLevel;
    }

    #region Input

    public void SetData(List<GameData> data)
    {
        gameTrackerModel.SetData(data);
    }

    #endregion
}
