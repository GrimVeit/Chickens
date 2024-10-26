using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceChicken : Chicken
{
    [SerializeField] private Transform eggToPosition;
    [SerializeField] private Transform eggFinishPosition;
    [SerializeField] private List<Transform> transformsBounces = new List<Transform>();
    [SerializeField] private float durationChanges;
    [SerializeField] private float maxJumpPower;
    [SerializeField] private float minJumpPower;
    [SerializeField] private float maxJumpDuration;
    [SerializeField] private float minJumpDuration;

    private float currentJumpPower;
    private float currentJumpDuration;

    private BounceEgg currentEgg;

    private IEnumerator changeJumpPowerAndDuration_IEnumerator;

    public override void Initialize()
    {
        base.Initialize();

        currentJumpDuration = maxJumpDuration;
        currentJumpPower = maxJumpPower;
        changeJumpPowerAndDuration_IEnumerator = ChangeJumpPowerAndDuration();
        Coroutines.Start(changeJumpPowerAndDuration_IEnumerator);
    }

    public override void Dispose()
    {
        if (changeJumpPowerAndDuration_IEnumerator != null)
            Coroutines.Stop(changeJumpPowerAndDuration_IEnumerator);

        base.Dispose();
    }

    public override void SpawnEgg(EggPrefab prefab)
    {
        currentEgg = Instantiate(prefab.egg, spawnTransform) as BounceEgg;
        currentEgg.SetJumpData(transformsBounces, currentJumpPower, currentJumpDuration);
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

    private IEnumerator ChangeJumpPowerAndDuration()
    {
        float elapsedTime = 0;

        while(elapsedTime < durationChanges)
        {
            currentJumpPower = Mathf.Lerp(maxJumpPower, minJumpPower, elapsedTime / durationChanges);
            currentJumpDuration = Mathf.Lerp(maxJumpDuration, minJumpDuration, elapsedTime / durationChanges);

            Debug.Log("Power - " + currentJumpPower + ", Duration in air - " + currentJumpDuration);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
