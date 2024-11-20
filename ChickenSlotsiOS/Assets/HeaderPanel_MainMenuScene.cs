using System;
using UnityEngine;
using UnityEngine.UI;

public class HeaderPanel_MainMenuScene : MovePanel
{
    [SerializeField] private Button leaderboard_Button;

    private ISoundProvider soundProvider;

    public override void Initialize()
    {
        base.Initialize();

        leaderboard_Button.onClick.AddListener(HandleGoToLeaderboard_ButtonClick);
    }

    public override void Dispose()
    {
        base.Dispose();

        leaderboard_Button.onClick.RemoveListener(HandleGoToLeaderboard_ButtonClick);
    }

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    #region Input

    public event Action GoToLeaderboard_Action;

    private void HandleGoToLeaderboard_ButtonClick()
    {
        soundProvider.PlayOneShot("ClickButton");
        GoToLeaderboard_Action?.Invoke();
    }

    #endregion
}
