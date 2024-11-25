using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimatorPresenter : ISpriteAnimator
{
    private SpriteAnimatorModel spriteAnimatorModel;
    private SpriteAnimatorView spriteAnimatorView;

    public SpriteAnimatorPresenter(SpriteAnimatorModel spriteAnimatorModel, SpriteAnimatorView spriteAnimatorView)
    {
        this.spriteAnimatorModel = spriteAnimatorModel;
        this.spriteAnimatorView = spriteAnimatorView;
    }

    public void Initialize()
    {
        ActivateEvents();

        spriteAnimatorView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        spriteAnimatorView.Dispose();
    }

    private void ActivateEvents()
    {
        spriteAnimatorModel.OnStartAnimator += spriteAnimatorView.StartAnimator;
        spriteAnimatorModel.OnStopAnimator += spriteAnimatorView.StopAnimator;
    }

    private void DeactivateEvents()
    {
        spriteAnimatorModel.OnStartAnimator -= spriteAnimatorView.StartAnimator;
        spriteAnimatorModel.OnStopAnimator -= spriteAnimatorView.StopAnimator;
    }

    #region Input

    public void StartAnimator(string id)
    {
        spriteAnimatorModel.StartAnimator(id);
    }

    public void StopAnimator(string id)
    {
        spriteAnimatorModel.StopAnimator(id);
    }

    #endregion
}

public interface ISpriteAnimator
{
    public void StartAnimator(string id);
    public void StopAnimator(string id);
}
