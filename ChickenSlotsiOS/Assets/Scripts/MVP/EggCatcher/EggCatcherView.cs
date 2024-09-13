using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCatcherView : View
{
    [SerializeField] private Transform parentEggsHealth;
    [SerializeField] private List<Chicken> chickens = new List<Chicken>();
    [SerializeField] private GameObject healthPrefab; 
    [SerializeField] private EggsPrefabs eggsPrefabs;

    public void Initialize()
    {
        for (int i = 0; i < chickens.Count; i++)
        {
            chickens[i].OnEggDown += HandlerEggDown;
            chickens[i].Initialize();
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < chickens.Count; i++)
        {
            chickens[i].OnEggDown -= HandlerEggDown;
        }
    }

    public void AddHealth(int countValue)
    {
        for (int i = 0; i < countValue; i++)
        {
            Instantiate(healthPrefab, parentEggsHealth);
        }
    }

    public void RemoveHealth()
    {
        Destroy(parentEggsHealth.GetChild(parentEggsHealth.childCount - 1).gameObject);
    }

    public void Spawn()
    {
        chickens[UnityEngine.Random.Range(0, chickens.Count)].SpawnEgg(eggsPrefabs.GetRandomEgg());
    }

    #region Input

    private void HandlerEggDown()
    {
        OnEggDown?.Invoke();
    }

    public event Action OnEggDown;

    #endregion

}
