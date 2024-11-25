using System;
using UnityEngine;

public class MiniGame1SceneEntryPoint_Compaign : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private LevelMinigame1_Datas levelMinigame1_Datas;
    [SerializeField] private UIMiniGame1SceneRoot_Compaign sceneRootPrefab;

    private UIMiniGame1SceneRoot_Compaign sceneRoot;
    private ViewContainer viewContainer;

    private SoundPresenter soundPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private BankPresenter bankPresenter;

    private EggCatcherPresenter eggCatcherPresenter;
    private BasketPresenter basketPresenter;
    private ScorePresenter scorePresenter;
    private PointAnimationPresenter pointAnimationPresenter;
    private TimerPresenter timerPreparationPresenter;
    private TimerPresenter timerMainPresenter;

    private GameProgressPresenter gameProgressPresenter;

    private LevelMinigame1_Presenter levelPresenter;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = Instantiate(sceneRootPrefab);
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter(new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS), viewContainer.GetView<SoundView>());
        soundPresenter.Initialize();

        particleEffectPresenter = new ParticleEffectPresenter(new ParticleEffectModel(), viewContainer.GetView<ParticleEffectView>());
        particleEffectPresenter.Initialize();

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        bankPresenter.Initialize();

        eggCatcherPresenter = new EggCatcherPresenter(new EggCatcherModel(2, 0.4f, 0.02f, soundPresenter, particleEffectPresenter), viewContainer.GetView<EggCatcherView>());
        eggCatcherPresenter.Initialize();

        basketPresenter = new BasketPresenter(new BasketModel(5, 2, bankPresenter, soundPresenter), viewContainer.GetView<BasketView_LeftRightControl>());
        basketPresenter.Initialize();

        scorePresenter = new ScorePresenter(new ScoreModel(bankPresenter, soundPresenter), viewContainer.GetView<ScoreView>());
        scorePresenter.Initialize();

        pointAnimationPresenter = new PointAnimationPresenter(new PointAnimationModel(), viewContainer.GetView<PointAnimationView_BabyChicken>());
        pointAnimationPresenter.Initialize();

        timerPreparationPresenter = new TimerPresenter(new TimerModel(), viewContainer.GetView<TimerView>("Preparation"));
        timerPreparationPresenter.Initialize();

        timerMainPresenter = new TimerPresenter(new TimerModel(), viewContainer.GetView<TimerView_MinutesSeconds>("Main"));
        timerMainPresenter.Initialize();

        gameProgressPresenter = new GameProgressPresenter(new GameProgressModel());

        levelPresenter = new LevelMinigame1_Presenter
            (new LevelMinigame1_Model(levelMinigame1_Datas), 
            viewContainer.GetView<LevelMinigame1_View>());
        levelPresenter.Initialize();

        ActivateEvents();

        gameProgressPresenter.Initialize();

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.SetParticleProvider(particleEffectPresenter);
        sceneRoot.Initialize();

        timerPreparationPresenter.ActivateTimer(3);
        basketPresenter.Start();
    }

    private void ActivateEvents()
    {
        sceneRoot.GoToMainMenu += HandleGoToMainMenu;
        sceneRoot.TryAgain += HandleGoToTryAgain;

        gameProgressPresenter.OnGetSelectGame += levelPresenter.ChooseLevel;
        levelPresenter.OnSetSpawnerData += eggCatcherPresenter.SetTimerSpawnerData;

        timerPreparationPresenter.OnStopTimer += eggCatcherPresenter.StartSpawner;
        timerPreparationPresenter.OnStopTimer += ActivateMainTimer;
        eggCatcherPresenter.OnEggDown += scorePresenter.RemoveHealth;
        eggCatcherPresenter.OnEggDown_EggValue += pointAnimationPresenter.PlayAnimation;
        eggCatcherPresenter.OnEggWin_EggValue += scorePresenter.AddScore;

        scorePresenter.OnGameFailed += timerMainPresenter.DeactivateTimer;
        scorePresenter.OnGameFailed += basketPresenter.Stop;
        scorePresenter.OnGameFailed += eggCatcherPresenter.DeactivateSpawner;
        scorePresenter.OnGameFailed += sceneRoot.OpenFailGamePanel;

        timerMainPresenter.OnStopTimer += basketPresenter.Stop;
        timerMainPresenter.OnStopTimer += sceneRoot.OpenWinGamePanel;
        timerMainPresenter.OnStopTimer += gameProgressPresenter.UnlockSecondGame;
        timerMainPresenter.OnStopTimer += eggCatcherPresenter.DeactivateSpawner;

    }

    private void DeactivateEvents()
    {
        sceneRoot.GoToMainMenu -= HandleGoToMainMenu;
        sceneRoot.TryAgain -= HandleGoToTryAgain;

        gameProgressPresenter.OnGetSelectGame -= levelPresenter.ChooseLevel;
        levelPresenter.OnSetSpawnerData -= eggCatcherPresenter.SetTimerSpawnerData;

        timerPreparationPresenter.OnStopTimer -= eggCatcherPresenter.StartSpawner;
        timerPreparationPresenter.OnStopTimer -= ActivateMainTimer;
        eggCatcherPresenter.OnEggDown -= scorePresenter.RemoveHealth;
        eggCatcherPresenter.OnEggDown_EggValue -= pointAnimationPresenter.PlayAnimation;
        eggCatcherPresenter.OnEggWin_EggValue -= scorePresenter.AddScore;

        scorePresenter.OnGameFailed -= timerMainPresenter.DeactivateTimer;
        scorePresenter.OnGameFailed -= basketPresenter.Stop;
        scorePresenter.OnGameFailed -= sceneRoot.OpenFailGamePanel;
        scorePresenter.OnGameFailed -= eggCatcherPresenter.DeactivateSpawner;

        timerMainPresenter.OnStopTimer -= basketPresenter.Stop;
        timerMainPresenter.OnStopTimer -= sceneRoot.OpenWinGamePanel;
        timerMainPresenter.OnStopTimer -= gameProgressPresenter.UnlockSecondGame;
        timerMainPresenter.OnStopTimer -= eggCatcherPresenter.DeactivateSpawner;
    }

    public void Dispose()
    {
        eggCatcherPresenter.DeactivateSpawner();
        DeactivateEvents();

        sceneRoot?.Dispose();
        soundPresenter?.Dispose();
        scorePresenter?.Dispose();
        eggCatcherPresenter?.Dispose();
        basketPresenter?.Dispose();
        bankPresenter?.Dispose();
        pointAnimationPresenter?.Dispose();
        timerPreparationPresenter?.Dispose();
        timerMainPresenter?.Dispose();
        levelPresenter?.Dispose();
        gameProgressPresenter?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    private void ActivateMainTimer()
    {
        timerMainPresenter.ActivateTimer(10);
    }

    #region Input

    public event Action GoToMainMenu;
    public event Action GoToTryAgain;

    private void HandleGoToMainMenu()
    {
        GoToMainMenu?.Invoke();
    }

    private void HandleGoToTryAgain()
    {
        GoToTryAgain?.Invoke();
    }

    #endregion
}
