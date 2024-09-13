using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutPanel_Slots1Scene : MovePanel
{
    [SerializeField] private Button backButton;

    public event Action OnClickBackButton;

    public event Action OnActivateAboutPanel;
    public event Action OnDeactivateAboutPanel;

    public override void Initialize()
    {
        base.Initialize();

        backButton.onClick.AddListener(() => OnClickBackButton?.Invoke());
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();
        OnActivateAboutPanel?.Invoke();
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();
        OnDeactivateAboutPanel?.Invoke();
    }
}
