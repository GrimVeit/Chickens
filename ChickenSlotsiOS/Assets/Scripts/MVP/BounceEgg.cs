using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceEgg : Egg
{
    private List<Transform> bounceTransforms = new List<Transform>();
    private int currentBounceIndex = 0;

    private Tween tweenBounce;

    private bool canTriggerJump = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Basket>() && canTriggerJump)
        {
            canTriggerJump = false;
            StartCoroutine(ResetTrigger());
            StartJump();
        }

        if (other.GetComponent<Earth>())
        {
            OnEggDown?.Invoke(eggValues, transform.position);
            Dispose();
        }
    }

    private IEnumerator ResetTrigger()
    {
        yield return new WaitForSeconds(0.2f);
        canTriggerJump = true;
    }

    private void EggChecker()
    {
        if(currentBounceIndex == bounceTransforms.Count)
        {
            OnEggWin?.Invoke(eggValues);
            Dispose();
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

            OnEggJump?.Invoke();

            tweenBounce = transform.DOJump(bounceTransforms[currentBounceIndex].position, 5, 1, 2f).SetEase(Ease.OutQuad).OnComplete(EggChecker);

            currentBounceIndex += 1;
        }
    }

    #endregion
}
