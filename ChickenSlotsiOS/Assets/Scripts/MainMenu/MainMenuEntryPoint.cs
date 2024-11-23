using Firebase.Database;
using Firebase.Extensions;
using Firebase.Auth;
using System;
using UnityEngine;
using Firebase;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UIMainMenuRoot menuRootPrefab;

    private UIMainMenuRoot sceneRoot;
    private ViewContainer viewContainer;

    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;
    private BankPresenter bankPresenter;
    private DailyRewardPresenter dailyRewardPresenter;
    private DailyBonusPresenter dailyBonusPresenter;
    private ShopPresenter shopPresenter;

    private FirebaseAuthenticationPresenter firebaseAuthenticationPresenter;
    private FirebaseDatabaseRealtimePresenter firebaseDatabaseRealtimePresenter;

    private CooldownPresenter cooldownDailyRewardPresenter;
    private CooldownPresenter cooldownDailyBonusPresenter;

    private DynamicScrollPresenter dynamicScrollPresenter;

    private GameProgressPresenter gameProgressPresenter;
    private GameTrackerPresenter gameTrackerPresenter;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = Instantiate(menuRootPrefab);
 
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {

            var dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                soundPresenter = new SoundPresenter
                    (new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS),
                    viewContainer.GetView<SoundView>());
                soundPresenter.Initialize();

                Debug.Log("Success");

                particleEffectPresenter = new ParticleEffectPresenter
                    (new ParticleEffectModel(),
                    viewContainer.GetView<ParticleEffectView>());
                particleEffectPresenter.Initialize();

                Debug.Log("Success");

                cooldownDailyRewardPresenter = new CooldownPresenter
                    (new CooldownModel(PlayerPrefsKeys.NEXT_DAILY_REWARD_TIME, TimeSpan.FromDays(1), soundPresenter),
                    viewContainer.GetView<CooldownView>("DailyReward"));
                cooldownDailyRewardPresenter.Initialize();

                Debug.Log("Success");

                cooldownDailyBonusPresenter = new CooldownPresenter
                    (new CooldownModel(PlayerPrefsKeys.NEXT_DAILY_BONUS_TIME, TimeSpan.FromDays(1), soundPresenter),
                    viewContainer.GetView<CooldownView>("DailyBonus"));
                cooldownDailyBonusPresenter.Initialize();

                Debug.Log("Success");

                bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
                bankPresenter.Initialize();

                Debug.Log("Success");

                dailyRewardPresenter = new DailyRewardPresenter
                    (new DailyRewardModel(soundPresenter, particleEffectPresenter),
                    viewContainer.GetView<DailyRewardView>());
                dailyRewardPresenter.Initialize();

                Debug.Log("Success");

                dailyBonusPresenter = new DailyBonusPresenter(new DailyBonusModel(soundPresenter), viewContainer.GetView<DailyBonusView>());
                dailyBonusPresenter.Initialize();

                Debug.Log("Success");

                shopPresenter = new ShopPresenter(new ShopModel(bankPresenter, soundPresenter), viewContainer.GetView<ShopView>());
                shopPresenter.Initialize();

                Debug.Log("Success");

                FirebaseAuth firebaseAuth = FirebaseAuth.DefaultInstance;
                FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
                FirebaseDatabase.DefaultInstance.GoOnline();
                DatabaseReference databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

                Debug.Log("Success");

                firebaseAuthenticationPresenter = new FirebaseAuthenticationPresenter
                    (new FirebaseAuthenticationModel(firebaseAuth, soundPresenter),
                    viewContainer.GetView<FirebaseAuthenticationView>());
                firebaseAuthenticationPresenter.Initialize();

                Debug.Log("Success");

                firebaseDatabaseRealtimePresenter = new FirebaseDatabaseRealtimePresenter
                    (new FirebaseDatabaseRealtimeModel(firebaseAuth, databaseReference),
                    viewContainer.GetView<FirebaseDatabaseRealtimeView>());
                firebaseDatabaseRealtimePresenter.Initialize();

                Debug.Log("Success");

                dynamicScrollPresenter = new DynamicScrollPresenter
                    (new DynamicScrollModel(soundPresenter), 
                    viewContainer.GetView<DynamicScrollView>());
                dynamicScrollPresenter.Initialize();

                Debug.Log("Success");

                gameProgressPresenter = new GameProgressPresenter(new GameProgressModel());
                gameProgressPresenter.UnselectAll();

                Debug.Log("Success");

                gameTrackerPresenter = new GameTrackerPresenter(new GameTrackerModel(), viewContainer.GetView<GameTrackerView>());
                gameTrackerPresenter.Initialize();

                Debug.Log("Success");

                sceneRoot.SetSoundProvider(soundPresenter);
                sceneRoot.SetParticleEffectProvider(particleEffectPresenter);
                sceneRoot.Initialize();

                ActivateTransitionsSceneEvents();
                ActivateEvents();

                Debug.Log("Success");

                gameProgressPresenter.Initialize();

                Debug.Log("Success");

                sceneRoot.Activate();
                cooldownDailyRewardPresenter.Activate();
                cooldownDailyBonusPresenter.Activate();

                if (firebaseAuthenticationPresenter.CheckAuthenticated())
                {
                    sceneRoot.OpenMainChoosePanel();
                    sceneRoot.OpenHeaderPanel();
                    sceneRoot.OpenFooterPanel();
                }
                else
                {
                    sceneRoot.OpenRegisterPanel();
                }
            }
            else
            {
                Debug.LogError(string.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    private void ActivateTransitionsSceneEvents()
    {
        sceneRoot.GoToMiniGame1_Action += HandleGoToMiniGame1;
        sceneRoot.GoToMiniGame2_Action += HandleGoToMiniGame2;
        sceneRoot.GoToMiniGame3_Action += HandleGoToMiniGame3;

        gameTrackerPresenter.OnGoToMiniGame1 += HandleGoToMiniGame1_Compaign;
        gameTrackerPresenter.OnGoToMiniGame2 += HandleGoToMiniGame1_Compaign;
        gameTrackerPresenter.OnGoToMiniGame3 += HandleGoToMiniGame1_Compaign;
    }

    private void DeactivateTransitionsSceneEvents()
    {
        sceneRoot.GoToMiniGame1_Action -= HandleGoToMiniGame1;
        sceneRoot.GoToMiniGame2_Action -= HandleGoToMiniGame2;
        sceneRoot.GoToMiniGame3_Action -= HandleGoToMiniGame3;

        gameTrackerPresenter.OnGoToMiniGame1 -= HandleGoToMiniGame1_Compaign;
        gameTrackerPresenter.OnGoToMiniGame2 -= HandleGoToMiniGame1_Compaign;
        gameTrackerPresenter.OnGoToMiniGame3 -= HandleGoToMiniGame1_Compaign;
    }

    private void ActivateEvents()
    {
        dailyRewardPresenter.OnGetDailyReward += cooldownDailyRewardPresenter.ActivateCooldown;
        cooldownDailyRewardPresenter.OnClickToActivatedButton += sceneRoot.OpenDailyRewardPanel;
        cooldownDailyRewardPresenter.OnClickToDeactivatedButton += sceneRoot.OpenCooldownDailyRewardPanel;
        cooldownDailyRewardPresenter.OnAvailable += sceneRoot.CloseCooldownDailyRewardPanel;

        dailyBonusPresenter.OnActivateSpin += cooldownDailyBonusPresenter.ActivateCooldown;
        cooldownDailyBonusPresenter.OnClickToActivatedButton += sceneRoot.OpenDailyBonusPanel;
        cooldownDailyBonusPresenter.OnClickToDeactivatedButton += sceneRoot.OpenCooldownDailyBonusPanel;
        cooldownDailyBonusPresenter.OnAvailable += sceneRoot.CloseCooldownDailyBonusPanel;

        cooldownDailyBonusPresenter.OnUnvailable += dailyBonusPresenter.SetUnvailable;
        cooldownDailyBonusPresenter.OnAvailable += dailyBonusPresenter.SetAvailable;

        dailyRewardPresenter.OnGetDailyReward_Count += bankPresenter.SendMoney;
        dailyBonusPresenter.OnGetBonus += bankPresenter.SendMoney;


        firebaseAuthenticationPresenter.OnSignUp += firebaseDatabaseRealtimePresenter.CreateEmptyDataToServer;
        firebaseAuthenticationPresenter.OnSignUp += firebaseDatabaseRealtimePresenter.DisplayUsersRecords;
        firebaseAuthenticationPresenter.OnSignUp += sceneRoot.OpenRegisterPanel;

        gameProgressPresenter.OnGetData += gameTrackerPresenter.SetData;
        gameTrackerPresenter.OnSelectGame += gameProgressPresenter.SelectGame;
    }

    private void DeactivateEvents()
    {
        dailyRewardPresenter.OnGetDailyReward -= cooldownDailyRewardPresenter.ActivateCooldown;
        cooldownDailyRewardPresenter.OnClickToActivatedButton -= sceneRoot.OpenDailyRewardPanel;
        cooldownDailyRewardPresenter.OnClickToDeactivatedButton -= sceneRoot.OpenCooldownDailyRewardPanel;
        cooldownDailyRewardPresenter.OnAvailable -= sceneRoot.CloseCooldownDailyRewardPanel;

        dailyBonusPresenter.OnActivateSpin -= cooldownDailyBonusPresenter.ActivateCooldown;
        cooldownDailyBonusPresenter.OnClickToActivatedButton -= sceneRoot.OpenDailyBonusPanel;
        cooldownDailyBonusPresenter.OnClickToDeactivatedButton -= sceneRoot.OpenCooldownDailyBonusPanel;
        cooldownDailyBonusPresenter.OnAvailable -= sceneRoot.CloseCooldownDailyBonusPanel;

        cooldownDailyBonusPresenter.OnUnvailable -= dailyBonusPresenter.SetUnvailable;
        cooldownDailyBonusPresenter.OnAvailable -= dailyBonusPresenter.SetAvailable;

        dailyRewardPresenter.OnGetDailyReward_Count -= bankPresenter.SendMoney;
        dailyBonusPresenter.OnGetBonus -= bankPresenter.SendMoney;



        firebaseAuthenticationPresenter.OnSignUp -= firebaseDatabaseRealtimePresenter.CreateEmptyDataToServer;
        firebaseAuthenticationPresenter.OnSignUp -= firebaseDatabaseRealtimePresenter.DisplayUsersRecords;
        firebaseAuthenticationPresenter.OnSignUp -= sceneRoot.OpenMainChoosePanel;

        gameProgressPresenter.OnGetData -= gameTrackerPresenter.SetData;
        gameTrackerPresenter.OnSelectGame += gameProgressPresenter.SelectGame;
    }

    private void OnDestroy()
    {
        Dispose();
    }

    private void Dispose()
    {
        DeactivateTransitionsSceneEvents();
        DeactivateEvents();
        sceneRoot.Deactivate();

        sceneRoot?.Dispose();
        particleEffectPresenter?.Dispose();
        soundPresenter?.Dispose();
        bankPresenter?.Dispose();
        dailyRewardPresenter?.Dispose();
        cooldownDailyRewardPresenter?.Dispose();
        cooldownDailyBonusPresenter?.Dispose();
        dailyBonusPresenter?.Dispose();
        shopPresenter?.Dispose();
        firebaseAuthenticationPresenter?.Dispose();
        firebaseDatabaseRealtimePresenter?.Dispose();
        dynamicScrollPresenter?.Dispose();

        gameProgressPresenter?.Dispose();
        gameTrackerPresenter?.Dispose();
    }

    #region Input actions

    public event Action GoToMiniGame1_Action;
    public event Action GoToMiniGame2_Action;
    public event Action GoToMiniGame3_Action;

    public event Action GoToMiniGame1_Compaign_Action;

    private void HandleGoToMiniGame1()
    {
        Dispose();
        GoToMiniGame1_Action?.Invoke();
    }

    private void HandleGoToMiniGame2()
    {
        Dispose();
        GoToMiniGame2_Action?.Invoke();
    }

    private void HandleGoToMiniGame3()
    {
        Dispose();
        GoToMiniGame3_Action?.Invoke();
    }


    private void HandleGoToMiniGame1_Compaign()
    {
        Dispose();
        GoToMiniGame1_Compaign_Action?.Invoke();
    }

    #endregion
}
