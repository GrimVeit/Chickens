using System;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCompaignGamePanel_MainMenuScene : MovePanel
{
    [SerializeField] private Button backButton;

    public event Action OnGoToBack;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public override void Initialize()
    {
        base.Initialize();

        backButton.onClick.AddListener(HandlerClickToBack);
    }

    public override void Dispose()
    {
        base.Dispose();

        backButton.onClick.RemoveListener(HandlerClickToBack);
    }

    #region Input

    private void HandlerClickToBack()
    {
        soundProvider.PlayOneShot("ClickButton");
        OnGoToBack?.Invoke();
    }

    #endregion
}
