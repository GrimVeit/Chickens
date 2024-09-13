using System;
using UnityEngine;

public class UIMainMenuRoot : MonoBehaviour
{
    [SerializeField] private MainPanel_MainMenuScene mainPanel;
    [SerializeField] private DailyBonusPanel_MainMenuScene dailyBonusPanel;
    [SerializeField] private DailyRewardPanel_MainMenuScene dailyRewardPanel;
    [SerializeField] private SettingsPanel_MainMenuScene settingsPanel;
    [SerializeField] private PrivacyPolicyPanel_MainMenuScene privacyPolicyPanel;
    [SerializeField] private AboutPanel_MainMenuScene aboutPanel;
    [SerializeField] private CooldownDailyRewardPanel_MainMenuScene cooldownDailyRewardPanel;
    [SerializeField] private CooldownDailyBonusPanel_MainMenuScene cooldownDailyBonusPanel;
    [SerializeField] private ShopPanel_MainMenuScene shopPanel;
    [SerializeField] private RegisterPanel_MainMenuScene registerPanel;
    [SerializeField] private LeaderboardPanel_MainMenuScene leaderboardPanel;

    private bool isCooldownDailyRewardPanelActivated;
    private bool isCooldownDailyBonusPanelActivated;

    //private ISoundProvider soundProvider;
    //private IParticleEffectProvider particleEffectProvider;

    private Panel currentPanel;

    public void Initialize()
    {
        mainPanel.Initialize();
        dailyBonusPanel.Initialize();
        dailyRewardPanel.Initialize();
        settingsPanel.Initialize();
        privacyPolicyPanel.Initialize();
        aboutPanel.Initialize();
        cooldownDailyRewardPanel.Initialize();
        cooldownDailyBonusPanel.Initialize();
        shopPanel.Initialize();
        leaderboardPanel.Initialize();
    }

    public void Activate()
    {
        mainPanel.GoToSlots1_Action += HandlerGoToSlots1;
        mainPanel.GoToSlots2_Action += HandlerGoToSlots2;
        mainPanel.GoToSlots3_Action += HandlerGoToSlots3;
        mainPanel.GoToMiniGame_Action += HandlerGoToMiniGame;

        mainPanel.GoToSettings_Action += OpenSettingsPanel;

        dailyRewardPanel.OnClickBackButton += OpenMainPanel;
        dailyBonusPanel.OnClickBackButton += OpenMainPanel;

        settingsPanel.OnClickBackButton += OpenMainPanel;
        settingsPanel.OnClickPrivacyPolicy += OpenPrivacyPolicyPanel;
        settingsPanel.OnClickAbout += OpenAboutPanel;

        privacyPolicyPanel.OnClickBackButton += OpenSettingsPanel;
        aboutPanel.OnClickBackButton += OpenSettingsPanel;

        cooldownDailyBonusPanel.OnClickBackButton += CloseCooldownDailyBonusPanel;
        cooldownDailyRewardPanel.OnClickBackButton += CloseCooldownDailyRewardPanel;

        mainPanel.GoToShop_Action += OpenShopPanel;
        shopPanel.OnClickBackButton += OpenMainPanel;

        mainPanel.GoToLeaderboard_Action += OpenLeaderboardPanel;
        leaderboardPanel.OnClickBackButton += OpenMainPanel;
    }

    public void Deactivate()
    {
        mainPanel.GoToSlots1_Action -= HandlerGoToSlots1;
        mainPanel.GoToSlots2_Action -= HandlerGoToSlots2;
        mainPanel.GoToSlots3_Action -= HandlerGoToSlots3;
        mainPanel.GoToMiniGame_Action -= HandlerGoToMiniGame;

        mainPanel.GoToSettings_Action -= OpenSettingsPanel;

        dailyRewardPanel.OnClickBackButton -= OpenMainPanel;
        dailyBonusPanel.OnClickBackButton -= OpenMainPanel;

        settingsPanel.OnClickBackButton -= OpenMainPanel;
        settingsPanel.OnClickPrivacyPolicy -= OpenPrivacyPolicyPanel;
        settingsPanel.OnClickAbout -= OpenAboutPanel;

        privacyPolicyPanel.OnClickBackButton -= OpenSettingsPanel;
        aboutPanel.OnClickBackButton -= OpenSettingsPanel;

        cooldownDailyBonusPanel.OnClickBackButton -= CloseCooldownDailyBonusPanel;
        cooldownDailyRewardPanel.OnClickBackButton -= CloseCooldownDailyRewardPanel;

        mainPanel.GoToShop_Action -= OpenShopPanel;
        shopPanel.OnClickBackButton -= OpenMainPanel;

        mainPanel.GoToLeaderboard_Action -= OpenLeaderboardPanel;
        leaderboardPanel.OnClickBackButton -= OpenMainPanel;
    }

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        //this.soundProvider = soundProvider;
    }

    public void SetParticleEffectProvider(IParticleEffectProvider particleEffectProvider)
    {
        //this.particleEffectProvider = particleEffectProvider;
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        privacyPolicyPanel.Dispose();
        aboutPanel.Dispose();
        dailyBonusPanel.Dispose();
        dailyRewardPanel.Dispose();
        settingsPanel.Dispose();
        cooldownDailyBonusPanel.Dispose();
        cooldownDailyRewardPanel.Dispose();
        shopPanel.Dispose();
        leaderboardPanel.Dispose();
    }


    private void OpenPanel(Panel panel)
    {
        if (currentPanel != null)
            currentPanel.DeactivatePanel();

        //soundProvider.PlayOneShot("ShoohPanel_Open");
        currentPanel = panel;
        currentPanel.ActivatePanel();

    }

    private void OpenOtherPanel(Panel panel)
    {
        panel.ActivatePanel();
    }

    private void CloseOtherPanel(Panel panel)
    {
        panel.DeactivatePanel();
    }

    public void OpenMainPanel()
    {
        OpenPanel(mainPanel);
    }

    public void OpenMainPanel_Other()
    {
        OpenOtherPanel(mainPanel);
    }

    public void OpenRegisterPanel()
    {
        OpenPanel(registerPanel);
    }

    public void OpenSettingsPanel()
    {
        OpenPanel(settingsPanel);
    }

    private void OpenShopPanel()
    {
        OpenPanel(shopPanel);
    }

    private void OpenLeaderboardPanel()
    {
        OpenPanel(leaderboardPanel);
    }

    private void OpenPrivacyPolicyPanel()
    {
        OpenPanel(privacyPolicyPanel);
    }

    private void OpenAboutPanel()
    {
        OpenPanel(aboutPanel);
    }

    public void OpenDailyBonusPanel()
    {
        OpenPanel(dailyBonusPanel);
    }

    public void OpenDailyRewardPanel()
    {
        OpenPanel(dailyRewardPanel);
    }





    public void OpenCooldownDailyRewardPanel()
    {
        isCooldownDailyRewardPanelActivated = true;
        OpenOtherPanel(cooldownDailyRewardPanel);
    }

    public void CloseCooldownDailyRewardPanel()
    {
        if (!isCooldownDailyRewardPanelActivated) return;

        isCooldownDailyRewardPanelActivated = false;
        CloseOtherPanel(cooldownDailyRewardPanel);
    }

    public void OpenCooldownDailyBonusPanel()
    {
        OpenOtherPanel(cooldownDailyBonusPanel);
    }

    public void CloseCooldownDailyBonusPanel()
    {
        CloseOtherPanel(cooldownDailyBonusPanel);
    }





    private void HandlerGoToSlots1()
    {
        currentPanel.DeactivatePanel();

        GoToSlots1_Action?.Invoke();
    }

    private void HandlerGoToSlots2()
    {
        currentPanel.DeactivatePanel();

        GoToSlots2_Action?.Invoke();
    }

    private void HandlerGoToSlots3()
    {
        currentPanel.DeactivatePanel();

        GoToSlots3_Action?.Invoke();
    }

    private void HandlerGoToMiniGame()
    {
        currentPanel.DeactivatePanel();

        GoToMiniGame_Action?.Invoke();
    }

    #region Input Actions

    public event Action OnActivateMainMenuPanel
    {
        add { mainPanel.OnOpenPanel += value; }
        remove { mainPanel.OnOpenPanel -= value; }
    }
    public event Action OnDeactivateMainMenuPanel
    {
        add { mainPanel.OnClosePanel += value; }
        remove { mainPanel.OnClosePanel -= value; }
    }

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

    public event Action GoToSlots1_Action;

    public event Action GoToSlots2_Action;

    public event Action GoToSlots3_Action;

    public event Action GoToMiniGame_Action;

    #endregion
}
