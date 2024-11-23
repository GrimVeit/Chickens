using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinGamePanel : MovePanel
{
    public event Action GoToMainMenu;

    [SerializeField] private Button backButton;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        backButton.onClick.AddListener(HandlerGoToMainMenu);
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        backButton.onClick.RemoveListener(HandlerGoToMainMenu);
    }

    private void HandlerGoToMainMenu()
    {
        soundProvider.PlayOneShot("Button");
        GoToMainMenu?.Invoke();
    }
}
