using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameIcon : MonoBehaviour, IPointerClickHandler
{
    public event Action<GameIcon> OnSelect;
    public RectTransform RectTransform => rectTransform;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Button buttonPlay;

    private Tween tweenScale;
    private Vector3 normalScale;
    private bool isSelect;

    public void Initialize()
    {
        normalScale = transform.localScale;
    }

    public void Dispose()
    {

    }

    public void Select()
    {
        if (tweenScale != null) tweenScale.Kill();

        tweenScale = transform.DOScale(normalScale * 1.15f, 0.3f).OnComplete(ActivateButton);
    }

    public void Deselect()
    {
        if (tweenScale != null) tweenScale.Kill();

        tweenScale = transform.DOScale(normalScale, 0.3f).OnComplete(DeactivateButton);
    }

    private void ActivateButton()
    {
        isSelect = true;
        buttonPlay.enabled = true;
    }

    private void DeactivateButton()
    {
        isSelect = false;
        buttonPlay.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSelect) return;

        OnSelect?.Invoke(this);
    }
}
