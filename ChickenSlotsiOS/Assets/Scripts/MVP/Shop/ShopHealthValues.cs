using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ShopHealthValues : MonoBehaviour
{
    public event Action<int, int> OnClickToBuyButton;

    [SerializeField] private Button button;
    [SerializeField] private GameObject buttonGameobject;
    [SerializeField] private GameObject lockedText;
    [SerializeField] private GameObject purchasedText;
    [SerializeField] private int price;
    //[SerializeField] private int healthValue;

    private Dictionary<ShopType, Action> shopTypes = new Dictionary<ShopType, Action>();

    private int index;

    public void Initialize(int index)
    {
        this.index = index;

        shopTypes[ShopType.Active] = ActivatedBuy;
        shopTypes[ShopType.Locked] = LockedBuy;
        shopTypes[ShopType.Purchased] = PurchasedBuy;

        button.onClick.AddListener(HandlerClickToBuy);
    }

    public void Dispose()
    {
        button.onClick.RemoveListener(HandlerClickToBuy);
    }

    private void HandlerClickToBuy()
    {
        OnClickToBuyButton?.Invoke(index, price);
    }


    public void Activate(ShopType shopType)
    {
        shopTypes[shopType]?.Invoke();
    }

    private void ActivatedBuy()
    {
        buttonGameobject.SetActive(true);
        lockedText.SetActive(false);
        purchasedText.SetActive(false);
    }

    private void LockedBuy()
    {
        buttonGameobject.SetActive(false);
        lockedText.SetActive(true);
        purchasedText.SetActive(false);
    }

    private void PurchasedBuy()
    {
        buttonGameobject.SetActive(false);
        lockedText.SetActive(false);
        purchasedText.SetActive(true);
    }
}

public enum ShopType
{
    Active, Purchased, Locked
}
