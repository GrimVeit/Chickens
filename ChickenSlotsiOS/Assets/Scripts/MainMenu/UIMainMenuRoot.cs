using System;
using UnityEngine;

public class UIMainMenuRoot : MonoBehaviour
{
    [SerializeField] private MainChoosePanel_MainMenuScene chooseGamePanel;
    [SerializeField] private ChooseArcadaGamePanel_MainMenuScene arcadaPanel;
    [SerializeField] private ChooseCompaignGamePanel_MainMenuScene compaignPanel;
    [SerializeField] private HeaderPanel_MainMenuScene headerPanel;
    [SerializeField] private FooterPanel_MainMenuScene footerPanel;

    [SerializeField] private DailyBonusPanel_MainMenuScene dailyBonusPanel;
    [SerializeField] private DailyRewardPanel_MainMenuScene dailyRewardPanel;
    [SerializeField] private CooldownDailyRewardPanel_MainMenuScene cooldownDailyRewardPanel;
    [SerializeField] private CooldownDailyBonusPanel_MainMenuScene cooldownDailyBonusPanel;
    [SerializeField] private ShopPanel_MainMenuScene shopPanel;
    [SerializeField] private RegisterPanel_MainMenuScene registerPanel;
    [SerializeField] private LeaderboardPanel_MainMenuScene leaderboardPanel;

    private bool isCooldownDailyRewardPanelActivated;
    private bool isCooldownDailyBonusPanelActivated;

    private ISoundProvider soundProvider;
    //private IParticleEffectProvider particleEffectProvider;

    private Panel currentPanel;

    public void Initialize()
    {
        chooseGamePanel.SetSoundProvider(soundProvider);
        arcadaPanel.SetSoundProvider(soundProvider);
        compaignPanel.SetSoundProvider(soundProvider);
        footerPanel.SetSoundProvider(soundProvider);
        headerPanel.SetSoundProvider(soundProvider);
        dailyBonusPanel.SetSoundProvider(soundProvider);
        dailyRewardPanel.SetSoundProvider(soundProvider);
        cooldownDailyBonusPanel.SetSoundProvider(soundProvider);
        cooldownDailyRewardPanel.SetSoundProvider(soundProvider);
        shopPanel.SetSoundProvider(soundProvider);
        leaderboardPanel.SetSoundProvider(soundProvider);

        chooseGamePanel.Initialize();
        arcadaPanel.Initialize();
        compaignPanel.Initialize();
        footerPanel.Initialize();
        headerPanel.Initialize();
        dailyBonusPanel.Initialize();
        dailyRewardPanel.Initialize();
        cooldownDailyRewardPanel.Initialize();
        cooldownDailyBonusPanel.Initialize();
        shopPanel.Initialize();
        leaderboardPanel.Initialize();
    }

    public void Activate()
    {
        chooseGamePanel.OnClickToChooseArcadaGameButton += OpenArcadaPanel;
        chooseGamePanel.OnClickToChooseCompaignGameButton += OpenCompaignPanel;

        arcadaPanel.OnGoToBack += OpenMainPanelPanel;
        compaignPanel.OnGoToBack += OpenMainPanelPanel;

        arcadaPanel.GoToMiniGame1_Action += HandlerGoToMiniGame1;
        arcadaPanel.GoToMiniGame2_Action += HandlerGoToMiniGame2;
        arcadaPanel.GoToMiniGame3_Action += HandlerGoToMiniGame3;

        dailyRewardPanel.OnClickBackButton += CloseDailyRewardPanel;
        dailyBonusPanel.OnClickBackButton += CloseDailyBonusPanel;

        cooldownDailyBonusPanel.OnClickBackButton += CloseCooldownDailyBonusPanel;
        cooldownDailyRewardPanel.OnClickBackButton += CloseCooldownDailyRewardPanel;

        footerPanel.GoToShop_Action += OpenShopPanel;
        shopPanel.OnClickBackButton += CloseShopPanel;

        headerPanel.GoToLeaderboard_Action += OpenLeaderboardPanel;
        leaderboardPanel.OnClickBackButton += CloseLeaderBoardPanel;
    }

    public void Deactivate()
    {
        chooseGamePanel.OnClickToChooseArcadaGameButton -= OpenArcadaPanel;
        chooseGamePanel.OnClickToChooseCompaignGameButton -= OpenCompaignPanel;

        arcadaPanel.OnGoToBack -= OpenMainPanelPanel;
        compaignPanel.OnGoToBack -= OpenMainPanelPanel;

        arcadaPanel.GoToMiniGame1_Action -= HandlerGoToMiniGame1;
        arcadaPanel.GoToMiniGame2_Action -= HandlerGoToMiniGame2;
        arcadaPanel.GoToMiniGame3_Action -= HandlerGoToMiniGame3;

        dailyRewardPanel.OnClickBackButton -= CloseDailyRewardPanel;
        dailyBonusPanel.OnClickBackButton -= CloseDailyBonusPanel;

        cooldownDailyBonusPanel.OnClickBackButton -= CloseCooldownDailyBonusPanel;
        cooldownDailyRewardPanel.OnClickBackButton -= CloseCooldownDailyRewardPanel;

        footerPanel.GoToShop_Action -= OpenShopPanel;
        shopPanel.OnClickBackButton -= CloseShopPanel;

        headerPanel.GoToLeaderboard_Action -= OpenLeaderboardPanel;
        leaderboardPanel.OnClickBackButton -= CloseLeaderBoardPanel;
    }

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void SetParticleEffectProvider(IParticleEffectProvider particleEffectProvider)
    {
        //this.particleEffectProvider = particleEffectProvider;
    }

    public void Dispose()
    {
        chooseGamePanel.Dispose();
        arcadaPanel.Dispose();
        compaignPanel.Dispose();
        footerPanel.Dispose();
        headerPanel.Dispose();
        dailyBonusPanel.Dispose();
        dailyRewardPanel.Dispose();
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



    public void OpenArcadaPanel()
    {
        OpenPanel(arcadaPanel);
    }

    public void OpenCompaignPanel()
    {
        OpenPanel(compaignPanel);
    }

    public void OpenMainPanelPanel()
    {
        OpenPanel(chooseGamePanel); 
    }

    public void OpenRegisterPanel()
    {
        OpenPanel(registerPanel);
    }



    public void OpenFooterPanel()
    {
        OpenOtherPanel(footerPanel);
    }

    public void CloseFooterPanel()
    {
        CloseOtherPanel(footerPanel);
    }



    public void OpenHeaderPanel()
    {
        Debug.Log("Open");
        OpenOtherPanel(headerPanel);
    }

    public void CloseHeaderPanel()
    {
        CloseOtherPanel(headerPanel);
    }




    private void OpenShopPanel()
    {
        CloseHeaderPanel();
        CloseFooterPanel();

        OpenOtherPanel(shopPanel);
    }

    private void CloseShopPanel()
    {
        OpenHeaderPanel();
        OpenFooterPanel();

        CloseOtherPanel(shopPanel);
    }




    private void OpenLeaderboardPanel()
    {
        CloseHeaderPanel();
        CloseFooterPanel();

        OpenOtherPanel(leaderboardPanel);
    }

    private void CloseLeaderBoardPanel()
    {
        OpenHeaderPanel();
        OpenFooterPanel();

        CloseOtherPanel(leaderboardPanel);
    }



    public void OpenDailyBonusPanel()
    {
        CloseHeaderPanel();
        CloseFooterPanel();

        OpenOtherPanel(dailyBonusPanel);
    }

    public void CloseDailyBonusPanel()
    {
        OpenHeaderPanel();
        OpenFooterPanel();

        CloseOtherPanel(dailyBonusPanel);
    }



    public void OpenDailyRewardPanel()
    {
        CloseHeaderPanel();
        CloseFooterPanel();

        OpenOtherPanel(dailyRewardPanel);
    }

    public void CloseDailyRewardPanel()
    {
        OpenHeaderPanel();
        OpenFooterPanel();

        CloseOtherPanel(dailyRewardPanel);
    }





    public void OpenCooldownDailyRewardPanel()
    {
        CloseHeaderPanel();
        CloseFooterPanel();

        isCooldownDailyRewardPanelActivated = true;
        OpenOtherPanel(cooldownDailyRewardPanel);
    }

    public void CloseCooldownDailyRewardPanel()
    {
        if (!isCooldownDailyRewardPanelActivated) return;

        OpenHeaderPanel();
        OpenFooterPanel();

        isCooldownDailyRewardPanelActivated = false;
        CloseOtherPanel(cooldownDailyRewardPanel);
    }




    public void OpenCooldownDailyBonusPanel()
    {
        CloseHeaderPanel();
        CloseFooterPanel();

        OpenOtherPanel(cooldownDailyBonusPanel);
    }

    public void CloseCooldownDailyBonusPanel()
    {
        OpenHeaderPanel();
        OpenFooterPanel();

        CloseOtherPanel(cooldownDailyBonusPanel);
    }




    private void HandlerGoToMiniGame1()
    {
        currentPanel.DeactivatePanel();

        GoToMiniGame1_Action?.Invoke();
    }

    private void HandlerGoToMiniGame2()
    {
        currentPanel.DeactivatePanel();

        GoToMiniGame2_Action?.Invoke();
    }

    private void HandlerGoToMiniGame3()
    {
        currentPanel.DeactivatePanel();

        GoToMiniGame3_Action?.Invoke();
    }

    #region Input Actions

    public event Action OnActivateMainMenuPanel
    {
        add { arcadaPanel.OnOpenPanel += value; }
        remove { arcadaPanel.OnOpenPanel -= value; }
    }
    public event Action OnDeactivateMainMenuPanel
    {
        add { arcadaPanel.OnClosePanel += value; }
        remove { arcadaPanel.OnClosePanel -= value; }
    }

    public event Action GoToMiniGame1_Action;

    public event Action GoToMiniGame2_Action;

    public event Action GoToMiniGame3_Action;

    #endregion
}
