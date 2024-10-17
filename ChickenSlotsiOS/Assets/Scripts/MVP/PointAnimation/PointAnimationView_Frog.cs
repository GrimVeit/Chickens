using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAnimationView_Frog : View, IPointAnimationView
{
    [Header("Egg")]
    [SerializeField] private Frog frogPrefab;
    [SerializeField] private Transform parentSpawn;
    [SerializeField] private Cock cock;

    [SerializeField] private List<Frog> frogs;

    public void PlayAnimation()
    {
        cock.ActivateAnimation(1, 0.2f);
    }

    public void PlayAnimation(Vector3 vector)
    {
        SpawnFrog(vector);
    }

    public void PlayAnimation(EggValue eggValue, Vector3 vector)
    {
        
    }

    private void SpawnFrog(Vector3 vector)
    {
        Frog frog = Instantiate(frogPrefab, parentSpawn);
        frogs.Add(frog);
        frog.transform.SetPositionAndRotation(vector, frogPrefab.transform.rotation);
        frog.OnEnd += DestroyFrog;
        frog.ActivateAnimation();
    }

    private void DestroyFrog(Frog frog)
    {
        if (frog != null)
        {
            frogs.Remove(frog);
            frog.OnEnd -= DestroyFrog;
            Destroy(frog.gameObject);
        }
    }
}
