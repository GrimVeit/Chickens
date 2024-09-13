using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyBonusPresenter
{
    private DailyBonusModel dailyBonusModel;
    private DailyBonusView dailyBonusView;

    public DailyBonusPresenter(DailyBonusModel dailyBonusModel, DailyBonusView dailyBonusView)
    {
        this.dailyBonusModel = dailyBonusModel;
        this.dailyBonusView = dailyBonusView;
    }

    public void Initialize()
    {
        ActivateEvents();

        dailyBonusView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        dailyBonusView.Dispose();
    }

    private void ActivateEvents()
    {
        dailyBonusView.OnClickSpinButton += dailyBonusModel.Spin;
        dailyBonusView.OnGetBonus += dailyBonusModel.GetBonus;

        dailyBonusModel.OnGetBonus += dailyBonusView.DisplayCoins;
        dailyBonusModel.OnActivateSpin += dailyBonusView.StartSpin;
        dailyBonusModel.OnUnvailableBonusButton += dailyBonusView.DeactivateSpinButton;
        dailyBonusModel.OnAvailableBonusButton += dailyBonusView.ActivateSpinButton;
    }

    private void DeactivateEvents()
    {
        dailyBonusView.OnClickSpinButton -= dailyBonusModel.Spin;
        dailyBonusView.OnGetBonus -= dailyBonusModel.GetBonus;

        dailyBonusModel.OnGetBonus -= dailyBonusView.DisplayCoins;
        dailyBonusModel.OnActivateSpin -= dailyBonusView.StartSpin;
        dailyBonusModel.OnUnvailableBonusButton -= dailyBonusView.DeactivateSpinButton;
        dailyBonusModel.OnAvailableBonusButton -= dailyBonusView.ActivateSpinButton;
    }

    #region

    public void SetAvailable()
    {
        dailyBonusModel.SetAvailable();
    }

    public void SetUnvailable()
    {
        dailyBonusModel.SetUnvailable();
    }

    public event Action<int> OnGetBonus
    {
        add { dailyBonusModel.OnGetBonus += value; }
        remove { dailyBonusModel.OnGetBonus -= value; }
    }

    public event Action OnActivateSpin
    {
        add { dailyBonusModel.OnActivateSpin += value; }
        remove { dailyBonusModel.OnActivateSpin -= value; }
    }

    #endregion
}
