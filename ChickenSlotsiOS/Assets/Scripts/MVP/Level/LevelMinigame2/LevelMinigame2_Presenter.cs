using System;

public class LevelMinigame2_Presenter
{
    private LevelMinigame2_Model levelModel;
    private LevelMinigame2_View levelView;

    public LevelMinigame2_Presenter(LevelMinigame2_Model levelModel, LevelMinigame2_View levelView)
    {
        this.levelModel = levelModel;
        this.levelView = levelView;
    }

    public void Initailize()
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
