using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggCatcherView : View
{
    [SerializeField] private List<Chicken> chickens = new List<Chicken>();
    [SerializeField] private EggsPrefabs eggsPrefabs;

    public void Initialize()
    {
        for (int i = 0; i < chickens.Count; i++)
        {
            chickens[i].OnEggDown += HandlerEggDown;
            chickens[i].OnEggWin += HandlerEggWin;
            chickens[i].Initialize();
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < chickens.Count; i++)
        {
            chickens[i].OnEggDown -= HandlerEggDown;
            chickens[i].OnEggWin -= HandlerEggWin;
        }
    }

    public void Spawn()
    {
        chickens[UnityEngine.Random.Range(0, chickens.Count)].SpawnEgg(eggsPrefabs.GetRandomEgg());
    }


    #region Input

    public event Action OnEggDown;
    public event Action<EggValues> OnEggWin;

    private void HandlerEggDown()
    {
        OnEggDown?.Invoke();
    }

    private void HandlerEggWin(EggValues eggValues)
    {
        OnEggWin?.Invoke(eggValues);
    }

    #endregion

}
