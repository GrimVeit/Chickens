using System;
using UnityEngine;
using UnityEngine.UI;

public class MiniGamePanel_MiniGameScene : MovePanel
{
    public event Action GoToMainMenu;

    [SerializeField] private Button backButton;

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
        GoToMainMenu?.Invoke();
    }
}
