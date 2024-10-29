using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicScrollModel
{
    public event Action<GameIcon> OnSelectGameIcon;

    private ISoundProvider soundProvider;

    public DynamicScrollModel(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void Select(GameIcon icon)
    {
        OnSelectGameIcon?.Invoke(icon);

        soundProvider.PlayOneShot("Select");
    }
}
