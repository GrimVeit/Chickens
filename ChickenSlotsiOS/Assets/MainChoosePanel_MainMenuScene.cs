using System;
using UnityEngine;
using UnityEngine.UI;

public class MainChoosePanel_MainMenuScene : MovePanel
{
    [SerializeField] private Button chooseArcadaGameButton;
    [SerializeField] private Button chooseCompaignGameButton;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public override void Initialize()
    {
        base.Initialize();

        chooseArcadaGameButton.onClick.AddListener(HandlerClickToArcadaButton);
        chooseCompaignGameButton.onClick.AddListener(HandlerClickToCompaignButton);
    }

    public override void Dispose()
    {
        base.Dispose();

        chooseArcadaGameButton.onClick.RemoveListener(HandlerClickToArcadaButton);
        chooseCompaignGameButton.onClick.RemoveListener(HandlerClickToCompaignButton);
    }

    #region Input

    public event Action OnClickToChooseArcadaGameButton;
    public event Action OnClickToChooseCompaignGameButton;

    private void HandlerClickToArcadaButton()
    {
        soundProvider.PlayOneShot("ClickButton");
        OnClickToChooseArcadaGameButton?.Invoke();
    }

    private void HandlerClickToCompaignButton()
    {
        soundProvider.PlayOneShot("ClickButton");
        OnClickToChooseCompaignGameButton?.Invoke();
    }

    #endregion
}
