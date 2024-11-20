using System;
using UnityEngine;
using UnityEngine.UI;

public class FooterPanel_MainMenuScene : MovePanel
{
    [SerializeField] private Button shop_Button;

    private ISoundProvider soundProvider;

    public override void Initialize()
    {
        base.Initialize();

        shop_Button.onClick.AddListener(HandleGoToShop_ButtonClick);
    }

    public override void Dispose()
    {
        base.Dispose();

        shop_Button.onClick.RemoveListener(HandleGoToShop_ButtonClick);
    }

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    #region Input

    public event Action GoToShop_Action;

    private void HandleGoToShop_ButtonClick()
    {
        soundProvider.PlayOneShot("ClickButton");
        GoToShop_Action?.Invoke();
    }

    #endregion
}
