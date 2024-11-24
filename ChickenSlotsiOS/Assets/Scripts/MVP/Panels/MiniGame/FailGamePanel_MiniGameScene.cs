using System;
using UnityEngine;
using UnityEngine.UI;

public class FailGamePanel_MiniGameScene : MovePanel
{
    public event Action GoToMainMenu;
    public event Action OnTryAgain;

    [SerializeField] private Button backButton;
    [SerializeField] private Button tryAgainButton;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public override void Initialize()
    {
        base.Initialize();

        backButton.onClick.AddListener(HandlerGoToMainMenu);
        tryAgainButton.onClick.AddListener(HandlerTryAgain);
    }

    public override void Dispose()
    {
        base.Dispose();

        backButton.onClick.RemoveListener(HandlerGoToMainMenu);
        tryAgainButton.onClick.RemoveListener(HandlerTryAgain);
    }

    private void HandlerGoToMainMenu()
    {
        soundProvider.PlayOneShot("Button");
        GoToMainMenu?.Invoke();
    }

    private void HandlerTryAgain()
    {
        soundProvider.PlayOneShot("Button");
        OnTryAgain?.Invoke();
    }
}
