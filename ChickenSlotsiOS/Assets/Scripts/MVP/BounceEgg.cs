using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BounceEgg : Egg
{
    public event Action OnEggWin;
    public event Action OnEggFallen;
    public event Action OnEggFallen_Index;

    private List<Transform> bounceTransforms = new List<Transform>();
    private int currentBounceIndex = 1;

    private Tween tweenBounce;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Basket"))
        {
            StartJump();
        }

        //if (other.transform.CompareTag("Earth"))
        //{
        //    OnEggDown?.Invoke();
        //    OnEggDown_Position?.Invoke(transform.position);
        //    Dispose();
        //}
    }

    private void EggChecker()
    {
        if(currentBounceIndex < bounceTransforms.Count)
        {
            OnEggWin?.Invoke();
        }
    }
    #region Bounce

    public void SetBounceTransforms(List<Transform> transforms)
    {
        bounceTransforms = transforms;
    }

    public void StartJump()
    {
        if(currentBounceIndex < bounceTransforms.Count)
        {
            if (tweenBounce != null) tweenBounce.Kill();

            tweenBounce = transform.DOJump(bounceTransforms[currentBounceIndex].position, 3, 1, 1.5f).SetEase(Ease.OutQuad).OnComplete(EggChecker);

            currentBounceIndex += 1;
        }
    }

    #endregion
}
