using System.Collections.Generic;
using UnityEngine;

public class BankView : View
{
    [SerializeField] private List<MoneyDisplay> moneyDisplays = new List<MoneyDisplay>();

    public void Initialize()
    {
        for (int i = 0; i < moneyDisplays.Count; i++)
        {
            moneyDisplays[i].Initialize();
        }
    }

    public void OnAddMoney()
    {
        for (int i = 0; i < moneyDisplays.Count; i++)
        {
            moneyDisplays[i].OnAddMoney();
        }
    }

    public void OnRemoveMoney()
    {
        for (int i = 0; i < moneyDisplays.Count; i++)
        {
            moneyDisplays[i].OnRemoveMoney();
        }
    }

    public void OnSendMoney(float money)
    {
        for (int i = 0; i < moneyDisplays.Count; i++)
        {
            moneyDisplays[i].OnSendMoney(money);
        }
    }
}
