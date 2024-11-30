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
            chickens[i].OnEggJump += HandlerEggJump;
            chickens[i].OnEggWin += HandlerEggWin;
            chickens[i].Initialize();
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < chickens.Count; i++)
        {
            chickens[i].OnEggDown -= HandlerEggDown;
            chickens[i].OnEggJump -= HandlerEggJump;
            chickens[i].OnEggWin -= HandlerEggWin;
            chickens[i].Dispose();
        }
    }

    public void SetMoveTime(float time)
    {
        for (int i = 0; i < chickens.Count; i++)
        {
            chickens[i].SetMoveTime(time);
        }
    }

    public void Spawn()
    {
        chickens[UnityEngine.Random.Range(0, chickens.Count)].SpawnEgg(eggsPrefabs.GetRandomEgg());
    }


    #region Input

    public event Action<EggValues, Vector3> OnEggDown;
    public event Action<EggValues> OnEggWin;
    public event Action OnEggJump;

    private void HandlerEggDown(EggValues eggValues, Vector3 posDown)
    {
        OnEggDown?.Invoke(eggValues, posDown);
    }

    private void HandlerEggWin(EggValues eggValues)
    {
        OnEggWin?.Invoke(eggValues);
    }

    private void HandlerEggJump()
    {
        OnEggJump?.Invoke();
    }

    #endregion

}
