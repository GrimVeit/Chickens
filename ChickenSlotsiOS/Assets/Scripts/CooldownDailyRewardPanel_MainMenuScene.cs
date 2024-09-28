using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownDailyRewardPanel_MainMenuScene : MovePanel
{
    [SerializeField] private Button backButton;

    public event Action OnClickBackButton;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        backButton.onClick.AddListener(HandlerClickToBackButton);
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        backButton.onClick.RemoveListener(HandlerClickToBackButton);
    }

    private void HandlerClickToBackButton()
    {
        soundProvider.PlayOneShot("ClickButton");

        OnClickBackButton?.Invoke();
    }
}
