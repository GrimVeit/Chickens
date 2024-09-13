using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPresenter
{
    private ShopModel shopModel;
    private ShopView shopView;

    public ShopPresenter(ShopModel shopModel, ShopView shopView)
    {
        this.shopModel = shopModel;
        this.shopView = shopView;
    }

    public void Initialize()
    {
        ActivateEvents();

        shopView.Initialize();
        shopModel.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        shopModel.Dispose();
        shopView.Dispose();
    }

    private void ActivateEvents()
    {
        shopView.OnClickToBuyButton += shopModel.Buy;

        shopModel.OnBuyHealth += shopView.OnBuy;
    }

    private void DeactivateEvents()
    {
        shopView.OnClickToBuyButton -= shopModel.Buy;

        shopModel.OnBuyHealth -= shopView.OnBuy;
    }
}
