using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasketView_ChooseButttonView : View, IBasketView
{
    [Header("Basket")]
    [SerializeField] private Basket basket;
    [SerializeField] private List<Transform> listTransforms = new List<Transform>();
    [SerializeField] private Button leftDownButton;
    [SerializeField] private Button rightDownButton;
    [SerializeField] private Button leftUpButton;
    [SerializeField] private Button rightUpButton;


    public void Initialize()
    {
        leftDownButton.onClick.AddListener(HandlerClickToLeftDownButton);
        leftUpButton.onClick.AddListener(HandlerClickToLeftUpButton);
        rightDownButton.onClick.AddListener(HandlerClickToRightDownButton);
        rightUpButton.onClick.AddListener(HandlerClickToRightUpButton);

        basket.Initialize();
    }

    public void Dispose()
    {
        leftDownButton.onClick.RemoveListener(HandlerClickToLeftDownButton);
        leftUpButton.onClick.RemoveListener(HandlerClickToLeftUpButton);
        rightDownButton.onClick.RemoveListener(HandlerClickToRightDownButton);
        rightUpButton.onClick.RemoveListener(HandlerClickToRightUpButton);

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

    private void HandlerClickToLeftDownButton()
    {
        OnSetPositionIndex?.Invoke(0);
    }

    private void HandlerClickToRightDownButton()
    {
        OnSetPositionIndex?.Invoke(3);
    }

    private void HandlerClickToLeftUpButton()
    {
        OnSetPositionIndex?.Invoke(1);
    }

    private void HandlerClickToRightUpButton()
    {
        OnSetPositionIndex?.Invoke(2);
    }

    #endregion
}
