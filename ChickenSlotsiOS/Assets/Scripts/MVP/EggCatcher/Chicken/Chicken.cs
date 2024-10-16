using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Chicken : MonoBehaviour
{
    public event Action<EggValues, Vector3> OnEggDown;
    public event Action<EggValues> OnEggWin;

    [SerializeField] private protected Transform spawnTransform;
    [SerializeField] private protected Image chickenImage;
    [SerializeField] private protected Sprite chickenSpawn;
    [SerializeField] private protected Sprite chickenUnspawn;

    private protected List<Egg> eggs = new List<Egg>();
    private protected IEnumerator changeSkinIEnumerator;

    public void Initialize()
    {
        chickenImage.sprite = chickenUnspawn;
    }

    public abstract void SpawnEgg(EggPrefab prefab);

    private protected void DestroyEgg(Egg egg)
    {
        DeactivateEvents(egg);

        egg.DestroyEgg();
    }

    private protected void ActivateEvents(Egg egg)
    {
        egg.OnEggDestroyed += DestroyEgg;
        egg.OnEggWin += HandlerOnEggWin;
        egg.OnEggDown += HandlerOnEggDown;
    }

    private protected void DeactivateEvents(Egg egg)
    {
        if(egg != null)
        {
            egg.OnEggDestroyed -= DestroyEgg;
            egg.OnEggWin -= HandlerOnEggWin;
            egg.OnEggDown -= HandlerOnEggDown;
        }
    }

    private protected void ChangeSkin()
    {
        chickenImage.sprite = chickenUnspawn;
    }

    private protected IEnumerator ChangeSkin_Coroutine()
    {
        yield return new WaitForSeconds(0.3f);
        ChangeSkin();
    }

    private protected void HandlerOnEggWin(EggValues eggValues)
    {
        OnEggWin?.Invoke(eggValues);
    }

    private protected void HandlerOnEggDown(EggValues eggValues, Vector3 vector)
    {
        OnEggDown?.Invoke(eggValues, vector);
    }
}
