using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour
{
    public string ID => id;

    [SerializeField] private string id;

    [SerializeField] private Image imageAnimate;
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    [SerializeField] private float timeMinReload;
    [SerializeField] private float timeMaxReload;
    [SerializeField] private bool isPlayOnAwake;

    private int indexSprite = 0;

    private IEnumerator spriteAnimator_Coroutine;

    public void Initialize()
    {
        if (isPlayOnAwake)
            StartAnimator();
    }

    public void Dispose()
    {
        StopAnimator();
    }

    public void StartAnimator()
    {
        if (spriteAnimator_Coroutine != null)
            Coroutines.Stop(spriteAnimator_Coroutine);

        spriteAnimator_Coroutine = SpriteAnimator_Coroutine();
        Coroutines.Start(spriteAnimator_Coroutine);
    }

    public void StopAnimator()
    {
        if (spriteAnimator_Coroutine != null)
            Coroutines.Stop(spriteAnimator_Coroutine);
    }

    private IEnumerator SpriteAnimator_Coroutine()
    {
        while (true)
        {
            imageAnimate.sprite = sprites[indexSprite];

            indexSprite += 1;

            if(indexSprite >= sprites.Count)
            {
                indexSprite = 0;
            }

            yield return new WaitForSeconds(Random.Range(timeMinReload, timeMaxReload));
        }
    }
}
