using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopView : View
{
    public event Action<int, int> OnClickToBuyButton;

    [SerializeField] private List<ShopHealthValues> healthValues;

    public void Initialize()
    {
        for (int i = 0; i < healthValues.Count; i++)
        {
            healthValues[i].OnClickToBuyButton += ClickToBuyButton;
            healthValues[i].Initialize(i);
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < healthValues.Count; i++)
        {
            healthValues[i].OnClickToBuyButton -= ClickToBuyButton;
            healthValues[i].Dispose();
        }
    }

    public void OnBuy(int index)
    {
        for (int i = 0; i < healthValues.Count; i++)
        {
            if (i <= index)
            {
                healthValues[i].Activate(ShopType.Purchased);
            }
            else if(i == index + 1)
            {
                healthValues[i].Activate(ShopType.Active);
            }
            else
            {
                healthValues[i].Activate(ShopType.Locked);
            }
        }
    }

    private void ClickToBuyButton(int index, int coins)
    {
        OnClickToBuyButton?.Invoke(index, coins);
    }
}
