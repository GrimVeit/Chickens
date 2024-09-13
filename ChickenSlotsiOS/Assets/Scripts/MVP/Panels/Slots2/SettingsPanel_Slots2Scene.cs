using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel_Slots2Scene : MovePanel
{
    public event Action OnClickBackButton;
    public event Action OnClickPrivacyPolicy;
    public event Action OnClickAbout;

    [SerializeField] private Button aboutButton;
    [SerializeField] private Button privacyPolicyButton;
    [SerializeField] private Button backButton;

    public override void Initialize()
    {
        base.Initialize();

        backButton.onClick.AddListener(HandlerClickToBackButton);
        privacyPolicyButton.onClick.AddListener(HandlerClickToPrivacyPolicy);
        aboutButton.onClick.AddListener(HandlerClickToAbout);
    }

    public override void Dispose()
    {
        base.Dispose();

        backButton.onClick.RemoveListener(HandlerClickToBackButton);
        privacyPolicyButton.onClick.RemoveListener(HandlerClickToPrivacyPolicy);
        aboutButton.onClick.RemoveListener(HandlerClickToAbout);
    }

    private void HandlerClickToBackButton()
    {
        OnClickBackButton?.Invoke();
    }

    private void HandlerClickToAbout()
    {
        OnClickAbout?.Invoke();
    }

    private void HandlerClickToPrivacyPolicy()
    {
        OnClickPrivacyPolicy?.Invoke();
    }
}
