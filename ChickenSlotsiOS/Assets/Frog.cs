using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Frog : MonoBehaviour
{
    public event Action<Frog> OnEnd;

    [SerializeField] private Image frogImage;
    [SerializeField] private Sprite frogSpriteNone;
    [SerializeField] private Sprite frogSpriteEat;

    private IEnumerator frog_IEnumerator;

    public void ActivateAnimation()
    {
        if (frog_IEnumerator != null)
            StopCoroutine(frog_IEnumerator);

        frog_IEnumerator = Frog_Coroutine();
        StartCoroutine(frog_IEnumerator);
    }

    private IEnumerator Frog_Coroutine()
    {
        frogImage.enabled = true;
        frogImage.sprite = frogSpriteEat;
        yield return new WaitForSeconds(0.2f);
        frogImage.sprite = frogSpriteNone;
        yield return new WaitForSeconds(1);

        OnEnd?.Invoke(this);
    }
}
