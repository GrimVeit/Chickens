using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketModel
{
    public event Action<int> OnChangeAllCountCoins;
    public event Action<int> OnGetCoins;
    public event Action OnStartMove;
    public event Action<Vector2> OnMove;
    public event Action OnEndMove;

    private int record;
    private int currentRecord;

    private Dictionary<EggValue, Action> eggValueActions = new Dictionary<EggValue, Action>();

    private int coins = 0;

    private bool isActive = true;

    public void Initialize()
    {
        eggValueActions[EggValue.Ten] = HandlerEggTen;
        eggValueActions[EggValue.Hundred] = HandlerEggHundred;
        eggValueActions[EggValue.Thousand] = HandlerEggThousand;

        record = PlayerPrefs.GetInt(PlayerPrefsKeys.GAME_RECORD);
    }

    public void StartMove()
    {
        if (!isActive) return;

        OnStartMove?.Invoke();
    }

    public void Move(Vector2 vector)
    {
        if (!isActive) return;

        OnMove?.Invoke(vector);
    }

    public void EndMove()
    {
        if (!isActive) return;

        OnEndMove?.Invoke();
    }

    public void Activate()
    {
        currentRecord = 0;
        isActive = true;
    }

    public void Deactivate()
    {
        if(currentRecord > record)
        {
            record = currentRecord;
            PlayerPrefs.SetInt(PlayerPrefsKeys.GAME_RECORD, record);
        }
        isActive = false;
    }

    public void GetEgg(EggValues eggValues)
    {
        currentRecord += 1;
        eggValueActions[eggValues.EggValue]?.Invoke();
    }

    private void AddCoins(int coins)
    {
        this.coins += coins;
        OnGetCoins?.Invoke(coins);
        OnChangeAllCountCoins?.Invoke(this.coins);
        Debug.Log($"{this.coins} монет");

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
}
