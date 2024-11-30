using UnityEngine;

public class ImmediateFallChicken : Chicken
{
    [SerializeField] private Transform eggTransformFinish;


    private float moveTime;

    public override void SpawnEgg(EggPrefab prefab)
    {
        NormalEgg currentEgg = Instantiate(prefab.egg, spawnTransform) as NormalEgg;
        currentEgg.SetLocalPosition(spawnTransform.position);
        currentEgg.SetLocalRotation(Quaternion.identity);

        ActivateEvents(currentEgg);

        currentEgg.Initialize(prefab.eggValue);
        currentEgg.MoveTo(eggTransformFinish.position, moveTime);

        if (changeSkinIEnumerator != null)
            StopCoroutine(changeSkinIEnumerator);

        chickenImage.sprite = chickenSpawn;
        changeSkinIEnumerator = ChangeSkin_Coroutine();
        StartCoroutine(changeSkinIEnumerator);
    }

    public override void SetMoveTime(float time)
    {
        moveTime = time;
    }
}
