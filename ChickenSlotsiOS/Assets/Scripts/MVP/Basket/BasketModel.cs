using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketModel : IBasketModel
{
    public event Action<int> OnMoveIndex;

    private bool isActive = true;

    private IMoneyProvider moneyProvider;
    private ISoundProvider soundProvider;

    private int[] indexTransforms;
    private int currentIndexTransform = 2;

    public BasketModel(int size, IMoneyProvider moneyProvider, ISoundProvider soundProvider)
    {
        indexTransforms = new int[size];

        this.moneyProvider = moneyProvider;
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public void MoveRightIndex()
    {
        if (!isActive) return;

        if (currentIndexTransform < indexTransforms.Length - 1)
        {
            currentIndexTransform += 1;

            OnMoveIndex?.Invoke(currentIndexTransform);
        }
    }

    public void MoveLeftIndex()
    {
        if(!isActive) return;

        if (currentIndexTransform > 0)
        {
            currentIndexTransform -= 1;

            OnMoveIndex?.Invoke(currentIndexTransform);
        }
    }

    public void SetPositionIndex(int index)
    {
        if (!isActive) return;

        if(index > indexTransforms.Length - 1)
        {
            Debug.LogError("Incorrect index");
            return;
        }

        OnMoveIndex?.Invoke(index);
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
