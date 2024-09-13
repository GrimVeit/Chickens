using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_Slots4Scene : MovePanel
{
    [SerializeField] private Button back_Button;
    [SerializeField] private Button settings_Button;

    public event Action GoToMainMenu_Action;
    public event Action GoToSettings_Action;

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        back_Button.onClick.AddListener(HandleGoToMainMenu_ButtonClick);
        settings_Button.onClick.AddListener(HandleGoToSettings_ButtonClick);
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        back_Button.onClick.RemoveListener(HandleGoToMainMenu_ButtonClick);
        settings_Button.onClick.RemoveListener(HandleGoToSettings_ButtonClick);
    }

    private void HandleGoToMainMenu_ButtonClick()
    {
        GoToMainMenu_Action?.Invoke();
    }

    private void HandleGoToSettings_ButtonClick()
    {
        GoToSettings_Action?.Invoke();
    }
}
