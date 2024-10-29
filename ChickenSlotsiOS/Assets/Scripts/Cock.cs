using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cock : MonoBehaviour
{
    [SerializeField] private Image cockImage;
    [SerializeField] private Sprite cockSpriteDown;
    [SerializeField] private Sprite cockSpriteUp;

    private IEnumerator frog_IEnumerator;

    public void ActivateAnimation(float duration, float speed)
    {
        if (frog_IEnumerator != null)
            StopCoroutine(frog_IEnumerator);

        frog_IEnumerator = Frog_Coroutine(duration, speed);
        StartCoroutine(frog_IEnumerator);
    }

    private IEnumerator Frog_Coroutine(float dura, float speed)
    {
        float duration = dura;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            cockImage.sprite = cockImage.sprite == cockSpriteDown ? cockSpriteUp : cockSpriteDown;

            yield return new WaitForSeconds(speed);

            elapsedTime += speed;
        }

        cockImage.sprite = cockSpriteDown;
    }
}
