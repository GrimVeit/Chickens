using System;
using UnityEngine;

public class UIMiniGame2SceneRoot : MonoBehaviour
{
    public event Action GoToMainMenu;

    [SerializeField] private MiniGamePanel_MiniGameScene miniGamePanel;
    [SerializeField] private WinGamePanel winGamePanel;

    private Panel currentPanel;
    private ISoundProvider soundProvider;
    private IParticleEffectProvider particleEffectProvider;

    public void Initialize()
    {
        miniGamePanel.SetSoundProvider(soundProvider);
        winGamePanel.SetSoundProvider(soundProvider);

        miniGamePanel.Initialize();
        winGamePanel.Initialize();

        miniGamePanel.GoToMainMenu += HandlerGoToMainMenu;
        winGamePanel.GoToMainMenu += HandlerGoToMainMenu;

        currentPanel = miniGamePanel;
        currentPanel.ActivatePanel();
    }

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void SetParticleProvider(IParticleEffectProvider particleEffectProvider)
    {
        this.particleEffectProvider = particleEffectProvider;
    }

    public void Dispose()
    {
        miniGamePanel.GoToMainMenu -= HandlerGoToMainMenu;
        winGamePanel.GoToMainMenu -= HandlerGoToMainMenu;

        miniGamePanel.Dispose();
        winGamePanel.Dispose();
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

    public void OpenWinGamePanel()
    {
        particleEffectProvider.Play("Win");
        soundProvider.PlayOneShot("Win");

        OpenOtherPanel(winGamePanel);
    }

    private void HandlerGoToMainMenu()
    {
        currentPanel.DeactivatePanel();

        GoToMainMenu?.Invoke();
    }
}
