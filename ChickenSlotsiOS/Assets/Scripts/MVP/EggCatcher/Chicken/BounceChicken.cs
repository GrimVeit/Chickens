using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceChicken : Chicken
{
    [SerializeField] private Transform eggToPosition;
    [SerializeField] private Transform eggFinishPosition;
    [SerializeField] private List<Transform> transformsBounces = new List<Transform>();

    private BounceEgg currentEgg;

    public override void SpawnEgg(EggPrefab prefab)
    {
        currentEgg = Instantiate(prefab.egg, spawnTransform) as BounceEgg;
        currentEgg.SetBounceTransforms(transformsBounces);
        currentEgg.SetLocalPosition(spawnTransform.position);
        currentEgg.SetLocalRotation(Quaternion.identity);

        ActivateEvents(currentEgg);

        currentEgg.Initialize(prefab.eggValue);
        currentEgg.Rotate();
        currentEgg.MoveTo(eggToPosition.position, 1, MoveEggToFinish);

        if (changeSkinIEnumerator != null)
            StopCoroutine(changeSkinIEnumerator);

        chickenImage.sprite = chickenSpawn;
        changeSkinIEnumerator = ChangeSkin_Coroutine();
        StartCoroutine(changeSkinIEnumerator);
    }

    private void MoveEggToFinish()
    {
        currentEgg.MoveTo(eggFinishPosition.position, 0.7f);
    }
}
