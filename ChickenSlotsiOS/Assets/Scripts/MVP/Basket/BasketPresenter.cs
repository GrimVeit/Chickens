using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketPresenter
{
    private BasketModel basketModel;
    private BasketView basketView;

    public BasketPresenter(BasketModel basketModel, BasketView basketView)
    {
        this.basketModel = basketModel;
        this.basketView = basketView;
    }

    public void Initialize()
    {
        ActivateEvents();

        basketModel.Initialize();
        basketView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        basketView.Dispose();
    }

    private void ActivateEvents()
    {
        basketView.OnStartMove_Action += basketModel.StartMove;
        basketView.OnEndMove_Action += basketModel.EndMove;
        basketView.OnMove_Action += basketModel.Move;

        basketView.OnGetEgg_Action += basketModel.GetEgg;

        basketModel.OnMove += basketView.Move;
        basketModel.OnStartMove += basketView.StartMove;
        basketModel.OnEndMove += basketView.EndMove;
        basketModel.OnGetCoins += basketView.DisplayWin;
        basketModel.OnChangeAllCountCoins += basketView.DisplayCoins;
    }

    private void DeactivateEvents()
    {
        basketView.OnStartMove_Action += basketModel.StartMove;
        basketView.OnEndMove_Action += basketModel.EndMove;
        basketView.OnMove_Action -= basketModel.Move;

        basketView.OnGetEgg_Action -= basketModel.GetEgg;

        basketModel.OnMove -= basketView.Move;
        basketModel.OnStartMove -= basketView.StartMove;
        basketModel.OnEndMove -= basketView.EndMove;

        basketModel.OnGetCoins -= basketView.DisplayWin;
        basketModel.OnChangeAllCountCoins += basketView.DisplayCoins;
    }

    #region Input

    public event Action OnStartMove
    {
        add { basketModel.OnStartMove += value; }
        remove { basketModel.OnStartMove -= value; }
    }

    public event Action OnStopMove
    {
        add { basketModel.OnEndMove += value; }
        remove { basketModel.OnEndMove -= value; }
    }

    public void Start()
    {
        basketModel.Activate();
    }

    public void Stop()
    {
        basketModel.Deactivate();
    }

    #endregion
}
