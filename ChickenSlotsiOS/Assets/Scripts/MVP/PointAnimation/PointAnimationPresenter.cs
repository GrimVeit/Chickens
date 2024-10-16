using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAnimationPresenter
{
    private PointAnimationModel pointAnimationModel;
    private IPointAnimationView pointAnimationView;

    public PointAnimationPresenter(PointAnimationModel pointAnimationModel, PointAnimationView_BabyChicken pointAnimationView)
    {
        this.pointAnimationModel = pointAnimationModel;
        this.pointAnimationView = pointAnimationView;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        pointAnimationModel.OnPlayAnimation += pointAnimationView.PlayAnimation;
        pointAnimationModel.OnPlayAnimation_EggValue += pointAnimationView.PlayAnimation;
    }

    private void DeactivateEvents()
    {
        pointAnimationModel.OnPlayAnimation -= pointAnimationView.PlayAnimation;
        pointAnimationModel.OnPlayAnimation_EggValue -= pointAnimationView.PlayAnimation;
    }

    #region Input

    public void PlayAnimation(Vector3 vector)
    {
        pointAnimationModel.PlayAnimation(vector);
    }

    public void PlayAnimation(EggValue value, Vector3 vector)
    {
        pointAnimationModel.PlayAnimation(value, vector);
    }

    #endregion
}

public interface IPointAnimationView
{
    void PlayAnimation(Vector3 vector);
    void PlayAnimation(EggValue eggValue, Vector3 vector);
}
