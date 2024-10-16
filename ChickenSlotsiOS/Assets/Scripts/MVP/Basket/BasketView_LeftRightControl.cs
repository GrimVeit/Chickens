using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.UI;

public class BasketView_LeftRightControl : View, IBasketView
{ 
    [Header("Basket")]
    [SerializeField] private Basket basket;
    [SerializeField] private List<Transform> listTransforms = new List<Transform>();
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;


    public void Initialize()
    {
        leftButton.onClick.AddListener(HandlerClickToLeftButton);
        rightButton.onClick.AddListener(HandlerClickToRightButton);

        basket.Initialize();
    }

    public void Dispose()
    {
        leftButton.onClick.RemoveListener(HandlerClickToLeftButton);
        rightButton.onClick.RemoveListener(HandlerClickToRightButton);

        basket.Dispose();
    }

    public void MoveToIndex(int index)
    {
        basket.MoveTo(listTransforms[index]);
    }

    #region Input

    public event Action<int> OnSetPositionIndex;
    public event Action OnSetLeft;
    public event Action OnSetRight;

    private void HandlerClickToLeftButton()
    {
        OnSetLeft?.Invoke();
    }

    private void HandlerClickToRightButton()
    {
        OnSetRight?.Invoke();
    }

    #endregion
}
