using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_MainMenuScene : MovePanel
{
    public event Action OnOpenPanel;
    public event Action OnClosePanel;

    [SerializeField] private Button slots1_Button;
    [SerializeField] private Button slots2_Button;
    [SerializeField] private Button slots3_Button;
    [SerializeField] private Button miniGame_Button;
    [SerializeField] private Button settings_Button;
    [SerializeField] private Button shop_Button;
    [SerializeField] private Button leaderboard_Button;
    [SerializeField] private Button comingSoon_Button;

    public event Action GoToSlots1_Action;
    public event Action GoToSlots2_Action;
    public event Action GoToSlots3_Action;
    public event Action GoToMiniGame_Action;
    public event Action GoToSettings_Action;
    public event Action GoToShop_Action;
    public event Action GoToLeaderboard_Action;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public override void ActivatePanel()
    {
        OnOpenPanel?.Invoke();

        base.ActivatePanel();

        slots1_Button.onClick.AddListener(HandleGoToSlots1_ButtonClick);
        slots2_Button.onClick.AddListener(HandleGoToSlots2_ButtonClick);
        slots3_Button.onClick.AddListener(HandleGoToSlots3_ButtonClick);
        miniGame_Button.onClick.AddListener(HandleGoToMiniGame_ButtonClick);
        settings_Button.onClick.AddListener(HandleGoToSettings_ButtonClick);
        shop_Button.onClick.AddListener(HandleGoToShop_ButtonClick);
        leaderboard_Button.onClick.AddListener(HandleGoToLeaderboard_ButtonClick);
        comingSoon_Button.onClick.AddListener(HandlerClickToComingSoon_ButtonClick);
    }

    public override void DeactivatePanel()
    {
        OnClosePanel?.Invoke();

        base.DeactivatePanel();

        slots1_Button.onClick.RemoveListener(HandleGoToSlots1_ButtonClick);
        slots2_Button.onClick.RemoveListener(HandleGoToSlots2_ButtonClick);
        slots3_Button.onClick.RemoveListener(HandleGoToSlots3_ButtonClick);
        miniGame_Button.onClick.RemoveListener(HandleGoToMiniGame_ButtonClick);
        settings_Button.onClick.RemoveListener(HandleGoToSettings_ButtonClick);
        shop_Button.onClick.RemoveListener(HandleGoToShop_ButtonClick);
        leaderboard_Button.onClick.RemoveListener(HandleGoToLeaderboard_ButtonClick);
        comingSoon_Button.onClick.RemoveListener(HandlerClickToComingSoon_ButtonClick);
    }

    private void HandleGoToSlots1_ButtonClick()
    {
        soundProvider.PlayOneShot("ClickButton");
        GoToSlots1_Action?.Invoke();
    }

    private void HandleGoToSlots2_ButtonClick()
    {
        soundProvider.PlayOneShot("ClickButton");
        GoToSlots2_Action?.Invoke();
    }

    private void HandleGoToSlots3_ButtonClick()
    {
        soundProvider.PlayOneShot("ClickButton");
        GoToSlots3_Action?.Invoke();
    }

    private void HandleGoToMiniGame_ButtonClick()
    {
        soundProvider.PlayOneShot("ClickButton");
        GoToMiniGame_Action?.Invoke();
    }

    private void HandleGoToSettings_ButtonClick()
    {
        soundProvider.PlayOneShot("ClickButton");
        GoToSettings_Action?.Invoke();
    }

    private void HandleGoToShop_ButtonClick()
    {
        soundProvider.PlayOneShot("ClickButton");
        GoToShop_Action?.Invoke();
    }

    private void HandleGoToLeaderboard_ButtonClick()
    {
        soundProvider.PlayOneShot("ClickButton");
        GoToLeaderboard_Action?.Invoke();
    }

    private void HandlerClickToComingSoon_ButtonClick()
    {
        soundProvider.PlayOneShot("ClickLocked");
    }
}
