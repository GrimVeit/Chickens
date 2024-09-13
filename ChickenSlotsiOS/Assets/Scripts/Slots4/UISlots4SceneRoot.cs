using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlots4SceneRoot : MonoBehaviour
{
    [SerializeField] private MainPanel_Slots4Scene mainPanel;
    [SerializeField] private SettingsPanel_Slots4Scene settingsPanel;
    [SerializeField] private PrivacyPolicyPanel_Slots4Scene privacyPolicyPanel;
    [SerializeField] private AboutPanel_Slots4Scene aboutPanel;

    private ISoundProvider soundProvider;

    private Panel currentPanel;

    public void Initialize()
    {
        mainPanel.Initialize();
        settingsPanel.Initialize();
        privacyPolicyPanel.Initialize();
        aboutPanel.Initialize();
    }

    public void Activate()
    {
        mainPanel.GoToMainMenu_Action += HandlerGoToMainMenu;
        mainPanel.GoToSettings_Action += OpenSettingsPanel;
        settingsPanel.OnClickBackButton += CloseSettingsPanel;
        settingsPanel.OnClickPrivacyPolicy += OpenPrivacyPolicyPanel;
        settingsPanel.OnClickAbout += OpenAboutPanel;

        privacyPolicyPanel.OnClickBackButton += ClosePrivacyPolicyPanel;
        aboutPanel.OnClickBackButton += CloseAboutPanel;

        OpenPanel(mainPanel);
    }

    public void Deactivate()
    {
        mainPanel.GoToMainMenu_Action -= HandlerGoToMainMenu;
        mainPanel.GoToSettings_Action -= OpenSettingsPanel;
        settingsPanel.OnClickBackButton -= CloseSettingsPanel;
        settingsPanel.OnClickPrivacyPolicy -= OpenPrivacyPolicyPanel;
        settingsPanel.OnClickAbout -= OpenAboutPanel;

        privacyPolicyPanel.OnClickBackButton -= ClosePrivacyPolicyPanel;
        aboutPanel.OnClickBackButton -= CloseAboutPanel;
    }

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        settingsPanel.Dispose();
        privacyPolicyPanel.Dispose();
        aboutPanel.Dispose();
    }

    private void OpenPanel(Panel panel)
    {
        if (currentPanel != null)
            currentPanel.DeactivatePanel();

        soundProvider.PlayOneShot("ShoohPanel_Open");
        currentPanel = panel;
        currentPanel.ActivatePanel();

    }

    private void OpenOtherPanel(Panel panel)
    {
        soundProvider.PlayOneShot("ShoohPanel_Open");
        panel.ActivatePanel();
    }

    private void CloseOtherPanel(Panel panel)
    {
        soundProvider.PlayOneShot("ShoohPanel_Close");
        panel.DeactivatePanel();
    }

    private void CloseSettingsPanel()
    {
        CloseOtherPanel(settingsPanel);
    }

    private void OpenSettingsPanel()
    {
        OpenOtherPanel(settingsPanel);
    }

    private void OpenPrivacyPolicyPanel()
    {
        CloseOtherPanel(settingsPanel);
        OpenOtherPanel(privacyPolicyPanel);
    }

    public void ClosePrivacyPolicyPanel()
    {
        OpenOtherPanel(settingsPanel);
        CloseOtherPanel(privacyPolicyPanel);
    }

    private void OpenAboutPanel()
    {
        CloseOtherPanel(settingsPanel);
        OpenOtherPanel(aboutPanel);
    }

    public void CloseAboutPanel()
    {
        OpenOtherPanel(settingsPanel);
        CloseOtherPanel(aboutPanel);
    }

    private void HandlerGoToMainMenu()
    {
        currentPanel.DeactivatePanel();

        GoToMainMenu?.Invoke();
    }

    #region Input Actions

    public event Action GoToMainMenu;

    public event Action OnActivatePrivacyPolicyPanel
    {
        add { privacyPolicyPanel.OnActivatePrivacyPolicyPanel += value; }
        remove { privacyPolicyPanel.OnActivatePrivacyPolicyPanel -= value; }
    }

    public event Action OnDeactivatePrivacyPolicyPanel
    {
        add { privacyPolicyPanel.OnDeactivatePrivacyPolicyPanel += value; }
        remove { privacyPolicyPanel.OnDeactivatePrivacyPolicyPanel -= value; }
    }

    public event Action OnActivateAboutPanel
    {
        add { aboutPanel.OnActivateAboutPanel += value; }
        remove { aboutPanel.OnActivateAboutPanel -= value; }
    }

    public event Action OnDeactivateAboutPanel
    {
        add { aboutPanel.OnDeactivateAboutPanel += value; }
        remove { aboutPanel.OnDeactivateAboutPanel -= value; }
    }

    #endregion
}
