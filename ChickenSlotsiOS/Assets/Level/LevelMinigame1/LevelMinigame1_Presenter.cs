using System;

public class LevelMinigame1_Presenter
{
    private LevelMinigame1_Model levelModel;
    private LevelMinigame1_View levelView;

    public LevelMinigame1_Presenter(LevelMinigame1_Model levelModel, LevelMinigame1_View levelView)
    {
        this.levelModel = levelModel;
        this.levelView = levelView;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        levelModel.OnChooseLevel += levelView.DisplayLevel;
    }

    private void DeactivateEvents()
    {
        levelModel.OnChooseLevel -= levelView.DisplayLevel;
    }

    #region Input

    public event Action<float, float, float> OnSetSpawnerData
    {
        add { levelModel.OnSetSpawnerData += value; }
        remove { levelModel.OnSetSpawnerData -= value; }
    }

    public void ChooseLevel(int level)
    {
        levelModel.ChooseLevel(level);
    }

    #endregion
}
