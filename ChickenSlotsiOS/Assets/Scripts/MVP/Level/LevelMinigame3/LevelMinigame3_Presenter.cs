using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMinigame3_Presenter
{
    private LevelMinigame3_Model levelModel;
    private LevelMinigame3_View levelView;

    public LevelMinigame3_Presenter(LevelMinigame3_Model levelModel, LevelMinigame3_View levelView)
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
