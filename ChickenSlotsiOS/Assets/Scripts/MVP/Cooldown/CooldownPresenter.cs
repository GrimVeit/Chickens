using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownPresenter
{
    private CooldownModel cooldownButtonModel;
    private CooldownView cooldownButtonView;

    public CooldownPresenter(CooldownModel cooldownButtonModel, CooldownView cooldownButtonView)
    {
        this.cooldownButtonModel = cooldownButtonModel;
        this.cooldownButtonView = cooldownButtonView;
    }

    public void Initialize()
    {
        cooldownButtonView.OnClickCooldownButton += cooldownButtonModel.ClickButton;

        cooldownButtonModel.OnCountdownTimer += cooldownButtonView.ChangeTimer;
        cooldownButtonModel.OnSetAvailableButton += cooldownButtonView.ActivateButton;
        cooldownButtonModel.OnSetUnvailableButton += cooldownButtonView.DeactivateButton;

        cooldownButtonModel.SetID(cooldownButtonView.GetID());
        cooldownButtonModel.Initialize();
        cooldownButtonView.Initialize();
    }

    public void Activate()
    {
        cooldownButtonModel.Activate();
    }

    public void Deactivate()
    {
        cooldownButtonModel.Deactivate();
    }

    public void Dispose()
    {
        //cooldownButtonView.OnClickCooldownButton -= cooldownButtonModel.ActivateCooldown;

        cooldownButtonModel.OnCountdownTimer -= cooldownButtonView.ChangeTimer;
        cooldownButtonModel.OnSetAvailableButton -= cooldownButtonView.ActivateButton;
        cooldownButtonModel.OnSetUnvailableButton -= cooldownButtonView.DeactivateButton;

        cooldownButtonModel.Dispose();
        cooldownButtonView.Dispose();
    }

    #region Input

    public void ActivateCooldown()
    {
        cooldownButtonModel.ActivateCooldown();
    }

    public event Action OnClickToActivatedButton
    {
        add { cooldownButtonModel.OnClickToActivatedButton += value; }
        remove { cooldownButtonModel.OnClickToActivatedButton -= value; }
    }

    public event Action OnClickToDeactivatedButton
    {
        add { cooldownButtonModel.OnClickToDeactivatedButton += value; }
        remove { cooldownButtonModel.OnClickToDeactivatedButton -= value; }
    }

    public event Action OnAvailable
    {
        add { cooldownButtonModel.OnSetAvailableButton += value; }
        remove { cooldownButtonModel.OnSetAvailableButton -= value; }
    }

    public event Action OnUnvailable
    {
        add { cooldownButtonModel.OnSetUnvailableButton += value; }
        remove { cooldownButtonModel.OnSetUnvailableButton -= value; }
    }

    #endregion
}
