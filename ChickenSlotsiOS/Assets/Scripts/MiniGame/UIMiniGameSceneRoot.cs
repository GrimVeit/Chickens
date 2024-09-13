using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMiniGameSceneRoot : MonoBehaviour
{
    public event Action GoToMainMenu;

    [SerializeField] private MiniGamePanel_MiniGameScene miniGamePanel;
    [SerializeField] private FailGamePanel_MiniGameScene failGamePanel;

    private Panel currentPanel;

    public void Initialize()
    {

        miniGamePanel.Initialize();
        failGamePanel.Initialize();

        miniGamePanel.GoToMainMenu += HandlerGoToMainMenu;
        failGamePanel.GoToMainMenu += HandlerGoToMainMenu;

        currentPanel = miniGamePanel;
        currentPanel.ActivatePanel();
    }

    public void Dispose()
    {
        miniGamePanel.GoToMainMenu -= HandlerGoToMainMenu;
        failGamePanel.GoToMainMenu -= HandlerGoToMainMenu;

        miniGamePanel.Dispose();
        failGamePanel.Dispose();
    }

    private void OpenPanel(Panel panel)
    {
        if (currentPanel != null)
            currentPanel.DeactivatePanel();

        currentPanel = panel;
        currentPanel.ActivatePanel();

    }

    private void OpenOtherPanel(Panel panel)
    {
        panel.ActivatePanel();
    }

    public void OpenFailGamePanel()
    {
        OpenOtherPanel(failGamePanel);
    }

    private void HandlerGoToMainMenu()
    {
        currentPanel.DeactivatePanel();

        GoToMainMenu?.Invoke();
    }
}
