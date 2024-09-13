using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggsPrefabs : MonoBehaviour
{
    [SerializeField] private List<EggPrefab> prefabs;

    private float totalWeight = 0;

    public void Initialize()
    {
       
    }

    public void Dispose()
    {

    }

    public EggPrefab GetRandomEgg()
    {
        totalWeight = 0;

        foreach (var egg in prefabs)
        {
            totalWeight += egg.eggValue.DropChance;
        }

        float randomValue = Random.Range(0, totalWeight);
        float currentValue = 0;

        foreach(var eggPrefab in prefabs)
        {
            currentValue += eggPrefab.eggValue.DropChance;

            if(randomValue <= currentValue)
            {
                return eggPrefab;
            }
        }

        return null;
    }
}
