using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteAnimatorView : View
{
    [SerializeField] private List<SpriteAnimator> spriteAnimators = new List<SpriteAnimator>();

    public void Initialize()
    {
        for (int i = 0; i < spriteAnimators.Count; i++)
        {
            spriteAnimators[i].Initialize();
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < spriteAnimators.Count; i++)
        {
            spriteAnimators[i].Dispose();
        }
    }

    public void StartAnimator(string id)
    {
        var animator = spriteAnimators.FirstOrDefault(data => data.ID == id);

        if(animator != null)
        {
            animator.StartAnimator();
        }
        else
        {
            Debug.LogError($"Animator id - {id} not found");
        }
    }

    public void StopAnimator(string id)
    {
        var animator = spriteAnimators.FirstOrDefault(data => data.ID == id);

        if (animator != null)
        {
            animator.StopAnimator();
        }
        else
        {
            Debug.LogError($"Animator id - {id} not found");
        }
    }
}
