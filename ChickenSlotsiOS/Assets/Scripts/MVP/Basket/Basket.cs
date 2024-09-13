using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Basket : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public event Action OnStartMove;
    public event Action OnEndMove;
    public event Action<Vector2> OnMove;
    public event Action<EggValues> OnGetEggValues;
    public RectTransform RectTransform => rectTransform;

    private RectTransform rectTransform;

    public void Initialize()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Dispose()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out IEgg egg))
        {
            OnGetEggValues?.Invoke(egg.GetEggValues());
            egg.Dispose();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnStartMove?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnMove?.Invoke(eventData.delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndMove?.Invoke();
    }
}
