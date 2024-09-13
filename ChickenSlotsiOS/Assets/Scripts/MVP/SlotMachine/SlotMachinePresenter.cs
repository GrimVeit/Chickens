using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachinePresenter
{
    private SlotMachineModel slotMachineModel;
    private SlotMachineView slotMachineView;

    public SlotMachinePresenter(SlotMachineModel slotMachineModel, SlotMachineView slotMachineView)
    {
        this.slotMachineModel = slotMachineModel;
        this.slotMachineView = slotMachineView;
    }

    public void Initialize()
    {
        slotMachineView.Initialize();

        ActivateInputEvents();
        slotMachineModel.OnActivateMachine += slotMachineView.ActivateMachine;
        ActivateDisplayEvents();
    }

    public void Dispose()
    {
        DeactivateInputEvents();
        slotMachineModel.OnActivateMachine -= slotMachineView.ActivateMachine;
        DeactivateDisplayEvents();
    }

    private void ActivateInputEvents()
    {
        slotMachineView.OnStopSpinSlot += slotMachineModel.StopSpinSlot;
        slotMachineView.OnClickSpin += slotMachineModel.ActivateMachine;
        slotMachineView.OnClickIncreaseBet += slotMachineModel.IncreaseBet;
        slotMachineView.OnClickDecreaseBet += slotMachineModel.DecreaseBet;
        slotMachineView.OnClickMaxBet += slotMachineModel.MaxBet;
        slotMachineView.OnClickAutoSpin += slotMachineModel.Autospin;
        //slotMachineView.OnWheelSpeed += slotMachineModel.WheelSpeed;
    }

    private void ActivateDisplayEvents()
    {
        slotMachineModel.OnChangedBet += slotMachineView.SendBetDisplay;
        slotMachineModel.OnWin += slotMachineView.WinMoney;
        slotMachineModel.OnFail += slotMachineView.FailMoney;
        slotMachineModel.OnActivateAutoSpin += slotMachineView.StartAutoSpin;
        slotMachineModel.OnDeactivateAutoSpin += slotMachineView.StopAutoSpin;
    }

    private void DeactivateInputEvents()
    {
        slotMachineView.OnStopSpinSlot -= slotMachineModel.StopSpinSlot;
        slotMachineView.OnClickSpin -= slotMachineModel.ActivateMachine;
        slotMachineView.OnClickIncreaseBet -= slotMachineModel.IncreaseBet;
        slotMachineView.OnClickDecreaseBet -= slotMachineModel.DecreaseBet;
        slotMachineView.OnClickMaxBet -= slotMachineModel.MaxBet;
        slotMachineView.OnClickAutoSpin -= slotMachineModel.Autospin;
        //slotMachineView.OnWheelSpeed -= slotMachineModel.WheelSpeed;
    }

    private void DeactivateDisplayEvents()
    {
        slotMachineModel.OnChangedBet -= slotMachineView.SendBetDisplay;
        slotMachineModel.OnWin -= slotMachineView.WinMoney;
        slotMachineModel.OnFail -= slotMachineView.FailMoney;
        slotMachineModel.OnActivateAutoSpin -= slotMachineView.StartAutoSpin;
        slotMachineModel.OnDeactivateAutoSpin -= slotMachineView.StopAutoSpin;
    }

    #region PublicEvents

    public event Action<float> OnWin
    {
        add { slotMachineModel.OnWin += value; }
        remove { slotMachineModel.OnWin -= value; }
    }

    public event Action OnFail
    {
        add { slotMachineModel.OnFail += value; }
        remove { slotMachineModel.OnFail -= value; }
    }

    #endregion
}
