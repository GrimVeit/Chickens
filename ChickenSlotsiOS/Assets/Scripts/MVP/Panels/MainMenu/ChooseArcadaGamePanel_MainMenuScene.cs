using System;
using UnityEngine;
using UnityEngine.UI;

public class ChooseArcadaGamePanel_MainMenuScene : MovePanel
{
    public event Action OnOpenPanel;
    public event Action OnClosePanel;

    [SerializeField] private Button backButton;
    [SerializeField] private Button miniGame1_Button;
    [SerializeField] private Button miniGame2_Button;
    [SerializeField] private Button miniGame3_Button;
    [SerializeField] private Button comingSoon_Button;

    public event Action OnGoToBack;
    public event Action GoToMiniGame1_Action;
    public event Action GoToMiniGame2_Action;
    public event Action GoToMiniGame3_Action;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public override void Initialize()
    {
        base.Initialize();

        backButton.onClick.AddListener(HandlerClickToBack);
        miniGame1_Button.onClick.AddListener(HandleGoToMiniGame1_ButtonClick);
        miniGame2_Button.onClick.AddListener(HandleGoToMiniGame2_ButtonClick);
        miniGame3_Button.onClick.AddListener(HandleGoToMiniGame3_ButtonClick);
        comingSoon_Button.onClick.AddListener(HandlerClickToComingSoon_ButtonClick);
    }

    public override void Dispose()
    {
        base.Dispose();

        backButton.onClick.RemoveListener(HandlerClickToBack);
        miniGame1_Button.onClick.RemoveListener(HandleGoToMiniGame1_ButtonClick);
        miniGame2_Button.onClick.RemoveListener(HandleGoToMiniGame2_ButtonClick);
        miniGame3_Button.onClick.RemoveListener(HandleGoToMiniGame3_ButtonClick);
        comingSoon_Button.onClick.RemoveListener(HandlerClickToComingSoon_ButtonClick);
    }

    public override void ActivatePanel()
    {
        OnOpenPanel?.Invoke();

        base.ActivatePanel();
    }

    public override void DeactivatePanel()
    {
        OnClosePanel?.Invoke();

        base.DeactivatePanel();
    }

    #region Input

    private void HandlerClickToBack()
    {
        soundProvider.PlayOneShot("ClickButton");
        OnGoToBack?.Invoke();
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

    private void HandlerClickToComingSoon_ButtonClick()
    {
        soundProvider.PlayOneShot("ClickLocked");
    }

    #endregion
}
