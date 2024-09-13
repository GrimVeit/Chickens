using System;
using UnityEngine;

public class ShopModel
{
    public event Action<int> OnBuyHealth;

    //private IMoneyProvider moneyProvider;


    private int currentCountHealth;
    public void Initialize()
    {
        currentCountHealth = PlayerPrefs.GetInt(PlayerPrefsKeys.HEALTH_COUNT, 1);
        OnBuyHealth?.Invoke(currentCountHealth - 2);
    }

    public void Dispose()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.HEALTH_COUNT, currentCountHealth);
    }

    public void Buy(int index, int coins)
    {
        //if (moneyProvider.CanAfford(coins))
        //{
        //    moneyProvider.SendMoney(coins);
        //    currentCountHealth = index+2;
        //    OnBuyHealth?.Invoke(index);

        //}

        currentCountHealth = index + 2;
        OnBuyHealth?.Invoke(index);
    }
}
