using System;
using UnityEngine;

public class ShopModel
{
    public event Action<int> OnBuyHealth;

    private IMoneyProvider moneyProvider;
    private ISoundProvider soundProvider;

    private int currentCountHealth;

    public ShopModel(IMoneyProvider moneyProvider, ISoundProvider soundProvider)
    {
        this.moneyProvider = moneyProvider;
        this.soundProvider = soundProvider;
    }

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
        if (!moneyProvider.CanAfford(coins))
        {
            soundProvider.PlayOneShot("ClickLocked");
            return;
        }

        soundProvider.PlayOneShot("ClickUnlocked");

        moneyProvider.SendMoney(-coins);
        currentCountHealth = index + 2;
        OnBuyHealth?.Invoke(index);
    }
}
