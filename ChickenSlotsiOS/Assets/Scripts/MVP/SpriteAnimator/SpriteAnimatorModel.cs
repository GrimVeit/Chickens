using System;

public class SpriteAnimatorModel
{
    public event Action<string> OnStartAnimator;
    public event Action<string> OnStopAnimator;

    public void StartAnimator(string id)
    {
        OnStartAnimator?.Invoke(id); 
    }

    public void StopAnimator(string id)
    {
        OnStopAnimator?.Invoke(id);
    }
}
