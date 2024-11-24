using System;
using UnityEngine;

public class UIMiniGame2SceneRoot_Compaign : MonoBehaviour
{
    public event Action GoToMainMenu;
    public event Action TryAgain;

    [SerializeField] private MiniGamePanel_MiniGameScene miniGamePanel;
    [SerializeField] private FailGamePanel_MiniGameScene failGamePanel;
    [SerializeField] private WinGamePanel winGamePanel;

    private Panel currentPanel;

    private ISoundProvider soundProvider;
    private IParticleEffectProvider particleEffectProvider;

    public void Initialize()
    {
        miniGamePanel.SetSoundProvider(soundProvider);
        failGamePanel.SetSoundProvider(soundProvider);
        winGamePanel.SetSoundProvider(soundProvider);

        miniGamePanel.Initialize();
        failGamePanel.Initialize();
        winGamePanel.Initialize();

        miniGamePanel.GoToMainMenu += HandlerGoToMainMenu;
        failGamePanel.GoToMainMenu += HandlerGoToMainMenu;
        failGamePanel.OnTryAgain += HandlerTryAgain;
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
        failGamePanel.GoToMainMenu -= HandlerGoToMainMenu;
        failGamePanel.OnTryAgain -= HandlerTryAgain;
        winGamePanel.GoToMainMenu -= HandlerGoToMainMenu;

        miniGamePanel.Dispose();
        failGamePanel.Dispose();
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

    public void OpenFailGamePanel()
    {
        //particleEffectProvider.Play("Win");
        //soundProvider.PlayOneShot("Win");

        OpenOtherPanel(failGamePanel);
    }

    public void OpenWinGamePanel()
    {
        particleEffectProvider.Play("Win");
        soundProvider.PlayOneShot("Win");

        OpenOtherPanel(winGamePanel);
    }

    private void HandlerGoToMainMenu()
    {
        soundProvider.PlayOneShot("Button");

        currentPanel.DeactivatePanel();

        GoToMainMenu?.Invoke();
    }

    private void HandlerTryAgain()
    {
        soundProvider.PlayOneShot("Button");

        currentPanel.DeactivatePanel();

        TryAgain?.Invoke();
    }
}
