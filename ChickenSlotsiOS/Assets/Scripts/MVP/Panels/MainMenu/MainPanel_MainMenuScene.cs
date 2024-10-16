using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_MainMenuScene : MovePanel
{
    public event Action OnOpenPanel;
    public event Action OnClosePanel;

    [SerializeField] private Button miniGame1_Button;
    [SerializeField] private Button miniGame2_Button;
    [SerializeField] private Button miniGame3_Button;
    [SerializeField] private Button settings_Button;
    [SerializeField] private Button shop_Button;
    [SerializeField] private Button leaderboard_Button;
    [SerializeField] private Button comingSoon_Button;

    public event Action GoToSlots1_Action;
    public event Action GoToSlots2_Action;
    public event Action GoToSlots3_Action;
    public event Action GoToMiniGame1_Action;
    public event Action GoToMiniGame2_Action;
    public event Action GoToMiniGame3_Action;
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

        miniGame1_Button.onClick.AddListener(HandleGoToMiniGame1_ButtonClick);
        miniGame2_Button.onClick.AddListener(HandleGoToMiniGame2_ButtonClick);
        miniGame3_Button.onClick.AddListener(HandleGoToMiniGame3_ButtonClick);

        settings_Button.onClick.AddListener(HandleGoToSettings_ButtonClick);
        shop_Button.onClick.AddListener(HandleGoToShop_ButtonClick);
        leaderboard_Button.onClick.AddListener(HandleGoToLeaderboard_ButtonClick);
        comingSoon_Button.onClick.AddListener(HandlerClickToComingSoon_ButtonClick);
    }

    public override void DeactivatePanel()
    {
        OnClosePanel?.Invoke();

        base.DeactivatePanel();

        miniGame1_Button.onClick.RemoveListener(HandleGoToMiniGame1_ButtonClick);
        miniGame2_Button.onClick.RemoveListener(HandleGoToMiniGame2_ButtonClick);
        miniGame3_Button.onClick.RemoveListener(HandleGoToMiniGame3_ButtonClick);

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

    private void HandleGoToMiniGame1_ButtonClick()
    {
        soundProvider.PlayOneShot("ClickButton");
        GoToMiniGame1_Action?.Invoke();
    }

    private void HandleGoToMiniGame2_ButtonClick()
    {
        soundProvider.PlayOneShot("ClickButton");
        GoToMiniGame2_Action?.Invoke();
    }

    private void HandleGoToMiniGame3_ButtonClick()
    {
        soundProvider.PlayOneShot("ClickButton");
        GoToMiniGame3_Action?.Invoke();
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
