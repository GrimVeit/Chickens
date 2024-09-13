using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrivacyPolicyPanel_Slots3Scene : MovePanel
{
    [SerializeField] private Button backButton;

    public event Action OnClickBackButton;

    public event Action OnActivatePrivacyPolicyPanel;
    public event Action OnDeactivatePrivacyPolicyPanel;

    public override void Initialize()
    {
        base.Initialize();

        backButton.onClick.AddListener(() => OnClickBackButton?.Invoke());
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();
        OnActivatePrivacyPolicyPanel?.Invoke();
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();
        OnDeactivatePrivacyPolicyPanel?.Invoke();
    }
}
