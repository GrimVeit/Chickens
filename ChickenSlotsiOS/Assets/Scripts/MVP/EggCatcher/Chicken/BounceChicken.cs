using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceChicken : Chicken
{
    [SerializeField] private Transform eggToPosition;
    [SerializeField] private List<Transform> transformsBounces = new List<Transform>();

    public override void SpawnEgg(EggPrefab prefab)
    {
        BounceEgg currentEgg = Instantiate(prefab.egg, spawnTransform) as BounceEgg;
        currentEgg.SetBounceTransforms(transformsBounces);
        currentEgg.SetLocalPosition(spawnTransform.position);
        currentEgg.SetLocalRotation(Quaternion.identity);

        ActivateEvents(currentEgg);

        currentEgg.Initialize(prefab.eggValue);
        currentEgg.MoveTo(eggToPosition.position, 1, currentEgg.StartJump);
        currentEgg.Rotate();

        if (changeSkinIEnumerator != null)
            StopCoroutine(changeSkinIEnumerator);

        chickenImage.sprite = chickenSpawn;
        changeSkinIEnumerator = ChangeSkin_Coroutine();
        StartCoroutine(changeSkinIEnumerator);
    }
}
