using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketPlatformPresenter
{
    private BasketPlatformModel basketPlatformModel;
    private BasketPlatformView basketPlatformView;

    public BasketPlatformPresenter(BasketPlatformModel basketPlatformModel, BasketPlatformView basketPlatformView)
    {
        this.basketPlatformModel = basketPlatformModel;
        this.basketPlatformView = basketPlatformView;
    }

    public void Initialize()
    {
        ActivateEvents();

        basketPlatformModel.Initialize();
        basketPlatformView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        basketPlatformModel.Dispose();
        basketPlatformView.Dispose();
    }

    private void ActivateEvents()
    {
        basketPlatformView.OnSetLeft += basketPlatformModel.MoveLeftIndex;
        basketPlatformView.OnSetRight += basketPlatformModel.MoveRightIndex;

        basketPlatformModel.OnMoveIndex += basketPlatformView.MoveToIndex;
    }

    private void DeactivateEvents()
    {
        basketPlatformView.OnSetLeft -= basketPlatformModel.MoveLeftIndex;
        basketPlatformView.OnSetRight -= basketPlatformModel.MoveRightIndex;

        basketPlatformModel.OnMoveIndex -= basketPlatformView.MoveToIndex;
    }
}
