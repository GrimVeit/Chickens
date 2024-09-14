using DG.Tweening;
using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMoney;
    [SerializeField] private Transform moneyDisplay;

    private Vector3 defaultMoneyTableScale;

    public void Initialize()
    {
        defaultMoneyTableScale = moneyDisplay.localScale;
    }

    public void OnAddMoney()
    {
        moneyDisplay.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f).OnComplete(() => moneyDisplay.DOScale(defaultMoneyTableScale, 0.2f));
    }

    public void OnRemoveMoney()
    {
        moneyDisplay.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).OnComplete(() => moneyDisplay.DOScale(defaultMoneyTableScale, 0.2f));
    }

    public void OnSendMoney(float money)
    {
        textMoney.text = money.ToString();
    }
}
