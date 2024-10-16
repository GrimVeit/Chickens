using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePresenter
{
    private ScoreModel scoreModel;
    private ScoreView scoreView;

    public ScorePresenter(ScoreModel scoreModel, ScoreView scoreView)
    {
        this.scoreModel = scoreModel;
        this.scoreView = scoreView;
    }

    public void Initialize()
    {
        ActivateEvents();

        scoreModel.Initialize();
        scoreView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        scoreModel.Dispose();
        scoreView.Dispose();
    }

    private void ActivateEvents()
    {
        scoreModel.OnAddHealth += scoreView.AddHealth;
        scoreModel.OnRemoveHealth += scoreView.RemoveHealth;

        scoreModel.OnChangeAllCountCoins += scoreView.DisplayCoins;
        scoreModel.OnGetCoins += scoreView.DisplayWin;
    }

    private void DeactivateEvents()
    {
        scoreModel.OnAddHealth -= scoreView.AddHealth;
        scoreModel.OnRemoveHealth -= scoreView.RemoveHealth;

        scoreModel.OnChangeAllCountCoins -= scoreView.DisplayCoins;
        scoreModel.OnGetCoins -= scoreView.DisplayWin;
    }

    #region Input

    public event Action OnGameFailed
    {
        add { scoreModel.OnGameFailed += value; }
        remove { scoreModel.OnGameFailed -= value; }
    }

    public void AddScore(EggValue eggValue)
    {
        scoreModel.AddScore(eggValue);
    }

    public void RemoveHealth()
    {
        scoreModel.RemoveHealth();
    }

    #endregion
}
