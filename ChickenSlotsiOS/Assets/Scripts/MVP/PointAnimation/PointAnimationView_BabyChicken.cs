using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointAnimationView_BabyChicken : View, IPointAnimationView
{
    [Header("Egg")]
    [SerializeField] private BabyChicken babyChickenPrefab;
    [SerializeField] private Transform parentSpawn;
    [SerializeField] private List<Transform> transformsChickenMoveInEnd;
    [SerializeField] private Sprite tenEgg;
    [SerializeField] private Sprite hundredEgg;
    [SerializeField] private Sprite thousandEgg;

    [SerializeField] private List<BabyChicken> babyChickens;

    public void PlayAnimation()
    {

    }

    public void PlayAnimation(Vector3 vector)
    {
        
    }

    public void PlayAnimation(EggValue eggValue, Vector3 vector)
    {
        switch (eggValue)
        {
            case EggValue.Ten:
                SpawnBabyChicken(vector, tenEgg);
                break;
            case EggValue.Hundred:
                SpawnBabyChicken(vector, hundredEgg);
                break;
            case EggValue.Thousand:
                SpawnBabyChicken(vector, thousandEgg);
                break;
        }
    }

    private void SpawnBabyChicken(Vector3 vector, Sprite sprite)
    {
        BabyChicken babyChicken = Instantiate(babyChickenPrefab, parentSpawn);
        babyChickens.Add(babyChicken);
        babyChicken.SetEggSprite(sprite);
        babyChicken.transform.SetPositionAndRotation(vector, babyChickenPrefab.transform.rotation);
        babyChicken.OnEndMove += DestroyBabyChicken;
        babyChicken.SetMoveTransformFinish(GetRandomTransformToMoveFinish());
        babyChicken.ActivateAnimation(2, 0.15f);
    }

    private void DestroyBabyChicken(BabyChicken babyChicken)
    {
        if(babyChicken != null)
        {
            babyChickens.Remove(babyChicken);
            babyChicken.OnEndMove -= DestroyBabyChicken;
            Destroy(babyChicken.gameObject);
        }
    }

    private Transform GetRandomTransformToMoveFinish()
    {
        int index = Random.Range(0, transformsChickenMoveInEnd.Count);
        return transformsChickenMoveInEnd[index];
    }
}
