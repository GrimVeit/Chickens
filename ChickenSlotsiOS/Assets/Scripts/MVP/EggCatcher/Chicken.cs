using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chicken : MonoBehaviour
{
    public event Action OnEggDown;

    [SerializeField] private Transform spawnTransform;
    [SerializeField] private Image chickenImage;
    [SerializeField] private Sprite chickenSpawn;
    [SerializeField] private Sprite chickenUnspawn;

    private Egg currentEgg;
    private IEnumerator testIEnumerator;

    public void Initialize()
    {
        chickenImage.sprite = chickenUnspawn;
    }

    public void SpawnEgg(EggPrefab prefab)
    {
        currentEgg = Instantiate(prefab.egg, spawnTransform);
        currentEgg.OnEggSpawned += ActivateEvents;
        currentEgg.OnEggDestroyed += DeactivateEvents;
        currentEgg.transform.SetLocalPositionAndRotation(spawnTransform.position, Quaternion.identity);
        currentEgg.Initialize(prefab.eggValue);

        if (testIEnumerator != null)
            StopCoroutine(testIEnumerator);

        chickenImage.sprite = chickenSpawn;
        testIEnumerator = Test_Coroutine();
        StartCoroutine(testIEnumerator);
    }

    private void ActivateEvents()
    {
        currentEgg.OnEggDown += HandlerOnEggDown;
    }

    private void DeactivateEvents()
    {
        currentEgg.OnEggSpawned -= ActivateEvents;
        currentEgg.OnEggDestroyed -= DeactivateEvents;
        currentEgg.OnEggDown -= HandlerOnEggDown;
    }

    private void Test()
    {
        chickenImage.sprite = chickenUnspawn;
    }

    private IEnumerator Test_Coroutine()
    {
        yield return new WaitForSeconds(0.3f);
        Test();
    }

    private void HandlerOnEggDown()
    {
        OnEggDown?.Invoke();
    }
}
