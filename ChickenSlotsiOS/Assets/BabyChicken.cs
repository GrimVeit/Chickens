using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BabyChicken : MonoBehaviour
{
    public event Action<BabyChicken> OnEndMove;

    [SerializeField] private Image eggImage;
    [SerializeField] private Image chickenImage;
    [SerializeField] private Sprite chickenSpriteUp;
    [SerializeField] private Sprite chickenSpriteDown;

    private Transform moveOnFinishTransform;

    private IEnumerator babyChicken_IEnumerator;
    private IEnumerator egg_IEnumerator;

    private Tween moveTween;
    private Tween rotateTween;

    public void SetMoveTransformFinish(Transform transform)
    {
        this.moveOnFinishTransform = transform;
    }

    public void SetEggSprite(Sprite sprite)
    {
        eggImage.sprite = sprite;
    }

    public void ActivateAnimation(float duration, float speed, Vector3 vector)
    {
        eggImage.enabled = true;
        chickenImage.enabled = true;

        if (babyChicken_IEnumerator != null)
            StopCoroutine(babyChicken_IEnumerator);
        if (egg_IEnumerator != null)
            StartCoroutine(egg_IEnumerator);


        babyChicken_IEnumerator = BabyChicken_Coroutine(duration, speed, vector);
        egg_IEnumerator = Egg_Coroutine();
        StartCoroutine(babyChicken_IEnumerator);
        StartCoroutine(egg_IEnumerator);
    }

    private IEnumerator Egg_Coroutine()
    {
        yield return new WaitForSeconds(0.3f);
        eggImage.enabled = false;
    }

    private IEnumerator BabyChicken_Coroutine(float dura, float speed, Vector3 vector)
    {
        float duration = dura;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            chickenImage.sprite = chickenImage.sprite == chickenSpriteDown ? chickenSpriteUp : chickenSpriteDown;

            yield return new WaitForSeconds(speed);

            elapsedTime += speed;
        }

        chickenImage.sprite = chickenSpriteDown;

        MoveBabyChicken();
    }

    private void MoveBabyChicken()
    {
        if (rotateTween != null) rotateTween.Kill();
        if (moveTween != null) moveTween.Kill();

        int randomValue = UnityEngine.Random.Range(0, 2) == 0 ? 360 : -360;

        rotateTween = transform.DORotate(new Vector3(0, 0, randomValue), 1.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
        moveTween = transform.DOMove(moveOnFinishTransform.position, 1.6f).OnComplete(() => OnEndMove?.Invoke(this));
    }

    private void OnDestroy()
    {
        if (rotateTween != null) rotateTween.Kill();
        if (moveTween != null) moveTween.Kill();
    }
}
