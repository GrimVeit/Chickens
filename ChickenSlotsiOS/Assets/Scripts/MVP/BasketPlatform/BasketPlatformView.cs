using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketPlatformView : View
{
    [Header("Basket")]
    [SerializeField] private Transform basket;
    [SerializeField] private List<Transform> listTransforms = new List<Transform>();
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    private Tween moveTween;

    public void Initialize()
    {
        leftButton.onClick.AddListener(HandlerClickToLeftButton);
        rightButton.onClick.AddListener(HandlerClickToRightButton);
    }

    public void Dispose()
    {
        leftButton.onClick.RemoveListener(HandlerClickToLeftButton);
        rightButton.onClick.RemoveListener(HandlerClickToRightButton);
    }

    public void MoveToIndex(int index)
    {
        MoveTo(listTransforms[index]);
    }

    public void MoveTo(Transform transformMove)
    {
        if (moveTween != null)
            moveTween.Kill();

        moveTween = basket.DOLocalMove(transformMove.localPosition, 0.08f);
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
