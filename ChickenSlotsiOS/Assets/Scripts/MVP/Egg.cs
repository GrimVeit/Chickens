using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Egg : MonoBehaviour
{
    public Action<Egg> OnEggDestroyed;
    public Action<EggValues, Vector3> OnEggDown;
    public Action<EggValues> OnEggWin;
    public Action OnEggJump;

    private protected EggValues eggValues;

    private protected Tween moveTween;
    private protected Tween rotateTween;

    [SerializeField] private protected Image image;

    public void Initialize(EggValues eggValue)
    {
        this.eggValues = eggValue;
        this.image.sprite = eggValue.SpriteEgg;
    }


    public void MoveTo(Vector3 to, float time, Action action = null)
    {
        if (moveTween != null) moveTween.Kill();

        moveTween = transform.DOMove(to, time).SetEase(Ease.Linear).OnComplete(() => action?.Invoke());
    }

    public void Rotate()
    {
        if (rotateTween != null) rotateTween.Kill();

        int randomValue = UnityEngine.Random.Range(0, 2) == 0 ? 360 : -360;

        rotateTween = transform.DORotate(new Vector3(0, 0, randomValue), 0.7f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }

    public void Dispose()
    {
        if (moveTween != null) moveTween.Kill();

        if (rotateTween != null) rotateTween.Kill();

        OnEggDestroyed?.Invoke(this);
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.GetComponent<Earth>())
    //    {
    //        OnEggDown?.Invoke();
    //        OnEggDown_Position?.Invoke(transform.position);
    //        Dispose();
    //    }

    //    if (other.GetComponent<Basket>())
    //    {
    //        OnEggWin_EggValues?.Invoke(eggValues);
    //        OnEggDown_Position?.Invoke(transform.position);
    //        Dispose();
    //    }
    //}

    public void SetLocalPosition(Vector3 vector)
    {
        transform.localPosition = vector;
    }

    public void SetLocalRotation(Quaternion quaternion)
    {
        transform.localRotation = quaternion;
    }

    public void DestroyEgg()
    {
        Destroy(gameObject);
    }
}
