using System;
using UnityEngine;

public class PointAnimationModel
{
    public event Action<Vector3> OnPlayAnimation;
    public event Action<EggValue, Vector3> OnPlayAnimation_EggValue;

    public void PlayAnimation(Vector3 vector)
    {
        OnPlayAnimation?.Invoke(vector);
    }

    public void PlayAnimation(EggValue eggValue, Vector3 vector)
    {
        OnPlayAnimation_EggValue?.Invoke(eggValue, vector);
    }

}
