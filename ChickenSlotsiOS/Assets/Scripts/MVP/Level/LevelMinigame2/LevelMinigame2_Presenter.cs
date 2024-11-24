using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }

    private void DeactivateEvents()
    {

    }
}
