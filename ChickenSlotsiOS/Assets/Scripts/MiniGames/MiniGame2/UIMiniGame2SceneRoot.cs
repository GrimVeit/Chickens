using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMiniGame2SceneRoot : MonoBehaviour
{
    public event Action GoToMainMenu;

    [SerializeField] private MiniGamePanel_MiniGameScene miniGamePanel;
    [SerializeField] private FailGamePanel_MiniGameScene failGamePanel;

    private Panel currentPanel;
    private ISoundProvider soundProvider;

    public void Initialize()
    {
        miniGamePanel.SetSoundProvider(soundProvider);
        failGamePanel.SetSoundProvider(soundProvider);

        miniGamePanel.Initialize();
        failGamePanel.Initialize();

        miniGamePanel.GoToMainMenu += HandlerGoToMainMenu;
        failGamePanel.GoToMainMenu += HandlerGoToMainMenu;

        currentPanel = miniGamePanel;
        currentPanel.ActivatePanel();
    }

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
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
