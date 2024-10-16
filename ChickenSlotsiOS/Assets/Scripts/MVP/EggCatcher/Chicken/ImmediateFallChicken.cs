using UnityEngine;

public class ImmediateFallChicken : Chicken
{
    [SerializeField] private Transform eggTransformFinish;
    public override void SpawnEgg(EggPrefab prefab)
    {
        NormalEgg currentEgg = Instantiate(prefab.egg, spawnTransform) as NormalEgg;
        currentEgg.SetLocalPosition(spawnTransform.position);
        currentEgg.SetLocalRotation(Quaternion.identity);

        ActivateEvents(currentEgg);

        currentEgg.Initialize(prefab.eggValue);
        currentEgg.MoveTo(eggTransformFinish.position, 1);

        if (changeSkinIEnumerator != null)
            StopCoroutine(changeSkinIEnumerator);

        chickenImage.sprite = chickenSpawn;
        changeSkinIEnumerator = ChangeSkin_Coroutine();
        StartCoroutine(changeSkinIEnumerator);
    }
}
