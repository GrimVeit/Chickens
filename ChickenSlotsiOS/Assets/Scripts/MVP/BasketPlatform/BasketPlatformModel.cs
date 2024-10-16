using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketPlatformModel
{
    public event Action<int> OnMoveIndex;
    private bool isActive = true;
    private ISoundProvider soundProvider;

    private int[] indexTransforms;
    private int currentIndexTransform = 2;

    public BasketPlatformModel(int countBasketTransforms, ISoundProvider soundProvider)
    {
        indexTransforms = new int[countBasketTransforms];
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        currentIndexTransform = 0;
        OnMoveIndex?.Invoke(currentIndexTransform);
    }

    public void Dispose()
    {

    }

    public void MoveRightIndex()
    {
        if (currentIndexTransform < indexTransforms.Length - 1)
        {
            currentIndexTransform += 1;

            OnMoveIndex?.Invoke(currentIndexTransform);
        }
    }

    public void MoveLeftIndex()
    {
        if (currentIndexTransform > 0)
        {
            currentIndexTransform -= 1;

            OnMoveIndex?.Invoke(currentIndexTransform);
        }
    }

    public void SetPositionIndex(int index)
    {
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
