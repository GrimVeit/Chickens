using UnityEngine;

public class PerchingChicken : Chicken
{
    [SerializeField] private Transform eggToPosition;
    [SerializeField] private Transform eggTransformFinish;

    private NormalEgg currentEgg;

    private float moveTime;

    public override void SpawnEgg(EggPrefab prefab)
    {
        currentEgg = Instantiate(prefab.egg, spawnTransform) as NormalEgg;
        currentEgg.SetLocalPosition(spawnTransform.position);
        currentEgg.SetLocalRotation(Quaternion.identity);

        ActivateEvents(currentEgg);

        currentEgg.Initialize(prefab.eggValue);
        currentEgg.MoveTo(eggToPosition.position, moveTime, MoveEggToFinish);
        currentEgg.Rotate();

        if (changeSkinIEnumerator != null)
            StopCoroutine(changeSkinIEnumerator);

        chickenImage.sprite = chickenSpawn;
        changeSkinIEnumerator = ChangeSkin_Coroutine();
        StartCoroutine(changeSkinIEnumerator);
    }

    private void MoveEggToFinish()
    {
        currentEgg.MoveTo(eggTransformFinish.position, moveTime);
    }

    public override void SetMoveTime(float time)
    {
        moveTime = time;
    }
}
