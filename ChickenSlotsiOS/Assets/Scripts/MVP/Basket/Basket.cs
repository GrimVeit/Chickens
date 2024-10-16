using DG.Tweening;
using System;
using UnityEngine;

public class Basket : MonoBehaviour
{
    private Tween moveTween;

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public void MoveTo(Transform transformMove)
    {
        if (moveTween != null)
            moveTween.Kill();

        moveTween = transform.DOLocalMove(transformMove.localPosition, 0.08f);
    }
}
