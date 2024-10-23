using System;
using UnityEngine;

public class UIMiniGame1SceneRoot : MonoBehaviour
{
    public event Action GoToMainMenu;

    [SerializeField] private MiniGamePanel_MiniGameScene miniGamePanel;
    [SerializeField] private FailGamePanel_MiniGameScene failGamePanel;

    private Panel currentPanel;

    private ISoundProvider soundProvider;
    private IParticleEffectProvider particleEffectProvider;

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

    public void SetParticleProvider(IParticleEffectProvider particleEffectProvider)
    {
        this.particleEffectProvider = particleEffectProvider;
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
        particleEffectProvider.Play("Win");
        soundProvider.PlayOneShot("Win");

        OpenOtherPanel(failGamePanel);
    }

    private void HandlerGoToMainMenu()
    {
        soundProvider.PlayOneShot("Button");

        currentPanel.DeactivatePanel();

        GoToMainMenu?.Invoke();
    }
}
