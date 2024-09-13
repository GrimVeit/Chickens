using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BankView : View
{
    [SerializeField] private TextMeshProUGUI textMoney;
    [SerializeField] private Transform moneyDisplay;

    private Vector3 defaultMoneyTableScale;

    public void Initialize()
    {
        defaultMoneyTableScale = moneyDisplay.localScale;
    }

    public void AddMoney()
    {
        moneyDisplay.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f).OnComplete(() => moneyDisplay.DOScale(defaultMoneyTableScale, 0.2f));
    }

    public void RemoveMoney()
    {
        moneyDisplay.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).OnComplete(() => moneyDisplay.DOScale(defaultMoneyTableScale, 0.2f));
    }

    public void SendMoneyDisplay(float money)
    {
        textMoney.text = money.ToString();
    }
}
