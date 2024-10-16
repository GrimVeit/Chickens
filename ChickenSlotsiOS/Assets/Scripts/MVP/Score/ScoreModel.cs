using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreModel
{
    public event Action OnGameFailed;
    public event Action OnRemoveHealth;
    public event Action<int> OnAddHealth;

    public event Action<int> OnChangeAllCountCoins;
    public event Action<int> OnGetCoins;

    private Dictionary<EggValue, Action> eggValueActions = new Dictionary<EggValue, Action>();

    private int record;
    private int currentRecord;
    private int coins = 0;

    private int health;
    private int currentHealth;

    private IMoneyProvider moneyProvider;
    private ISoundProvider soundProvider;
     
    public ScoreModel(IMoneyProvider moneyProvider, ISoundProvider soundProvider)
    {
        this.moneyProvider = moneyProvider;
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        eggValueActions[EggValue.Ten] = HandlerEggTen;
        eggValueActions[EggValue.Hundred] = HandlerEggHundred;
        eggValueActions[EggValue.Thousand] = HandlerEggThousand;

        record = PlayerPrefs.GetInt(PlayerPrefsKeys.GAME_RECORD);
        health = PlayerPrefs.GetInt(PlayerPrefsKeys.HEALTH_COUNT, 1);

        currentHealth = health;
        OnAddHealth?.Invoke(currentHealth);
    }


    public void Dispose()
    {
        if (currentRecord > record)
        {
            record = currentRecord;
            PlayerPrefs.SetInt(PlayerPrefsKeys.GAME_RECORD, record);
        }

        moneyProvider.SendMoney(coins);
    }


    public void RemoveHealth()
    {
        currentHealth -= 1;

        if (currentHealth > 0)
        {
            Debug.Log("Минус жизка");
            OnRemoveHealth?.Invoke();
            //soundProvider.PlayOneShot("FallEgg");
            return;
        }

        if (currentHealth == 0)
        {
            Debug.Log("Проигрыш");
            OnRemoveHealth?.Invoke();
            OnGameFailed?.Invoke();
            //soundProvider.PlayOneShot("Success");
            //particleEffectProvider.Play("Win");
        }
    }
    
    public void AddScore(EggValue eggValue)
    {
        currentRecord += 1;
        eggValueActions[eggValue]?.Invoke();
    }

    private void HandlerEggTen()
    {
        AddCoins(10);
    }

    private void HandlerEggHundred()
    {
        AddCoins(100);
    }

    private void HandlerEggThousand()
    {
        AddCoins(1000);
    }

    private void AddCoins(int coins)
    {
        this.coins += coins;
        OnGetCoins?.Invoke(coins);
        OnChangeAllCountCoins?.Invoke(this.coins);
        Debug.Log($"{this.coins} монет");

    }
}
