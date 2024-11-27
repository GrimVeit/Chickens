using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame3SceneEntryPoint_Compaign : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private LevelMinigame3_Datas levelMinigame3_Datas;
    [SerializeField] private UIMiniGame3SceneRoot_Compaign sceneRootPrefab;

    private UIMiniGame3SceneRoot_Compaign sceneRoot;
    private ViewContainer viewContainer;

    private SoundPresenter soundPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private BankPresenter bankPresenter;

    private BasketPresenter basketPresenter;
    private EggCatcherPresenter eggCatcherPresenter;
    private ScorePresenter scorePresenter;
    private PointAnimationPresenter pointAnimationPresenter;

    private TimerPresenter timerPreparationPresenter;
    private TimerPresenter timerMainPresenter;

    private GameProgressPresenter gameProgressPresenter;

    private LevelMinigame3_Presenter levelPresenter;

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

        basketPresenter = new BasketPresenter(new BasketModel(5, 0, bankPresenter, soundPresenter), viewContainer.GetView<BasketView_LeftRightControl>());
        basketPresenter.Initialize();

        eggCatcherPresenter = new EggCatcherPresenter(new EggCatcherModel(7, 2f, 0.04f, soundPresenter, particleEffectPresenter), viewContainer.GetView<EggCatcherView>());
        eggCatcherPresenter.Initialize();

        scorePresenter = new ScorePresenter(new ScoreModel(bankPresenter, soundPresenter), viewContainer.GetView<ScoreView>());
        scorePresenter.Initialize();

        pointAnimationPresenter = new PointAnimationPresenter(new PointAnimationModel(), viewContainer.GetView<PointAnimationView_Frog>());
        pointAnimationPresenter.Initialize();

        timerPreparationPresenter = new TimerPresenter(new TimerModel(), viewContainer.GetView<TimerView>("Preparation"));
        timerPreparationPresenter.Initialize();

        timerMainPresenter = new TimerPresenter(new TimerModel(), viewContainer.GetView<TimerView_MinutesSeconds>("Main"));
        timerMainPresenter.Initialize();

        gameProgressPresenter = new GameProgressPresenter(new GameProgressModel(), viewContainer.GetView<GameProgressView>());

        levelPresenter = new LevelMinigame3_Presenter
            (new LevelMinigame3_Model(levelMinigame3_Datas), 
            viewContainer.GetView<LevelMinigame3_View>());
        levelPresenter.Initailize();

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
        sceneRoot.GoToTryAgain += HandleGoToTryAgain;

        gameProgressPresenter.OnGetSelectGame += levelPresenter.ChooseLevel;
        levelPresenter.OnSetSpawnerData += eggCatcherPresenter.SetTimerSpawnerData;

        timerPreparationPresenter.OnStopTimer += eggCatcherPresenter.StartSpawner;
        timerPreparationPresenter.OnStopTimer += ActivateMainTimer;
        eggCatcherPresenter.OnEggDown += scorePresenter.RemoveHealth;
        eggCatcherPresenter.OnEggDown_Position += pointAnimationPresenter.PlayAnimation;
        eggCatcherPresenter.OnEggWin += pointAnimationPresenter.PlayAnimation;
        eggCatcherPresenter.OnEggWin_EggValue += scorePresenter.AddScore;

        scorePresenter.OnGameFailed += timerMainPresenter.DeactivateTimer;
        scorePresenter.OnGameFailed += eggCatcherPresenter.DeactivateSpawner;
        scorePresenter.OnGameFailed += basketPresenter.Stop;
        scorePresenter.OnGameFailed += sceneRoot.OpenFailGamePanel;

        timerMainPresenter.OnStopTimer += basketPresenter.Stop;
        timerMainPresenter.OnStopTimer += sceneRoot.OpenWinGamePanel;
        timerMainPresenter.OnStopTimer += gameProgressPresenter.UnlockSecondGame;
        timerMainPresenter.OnStopTimer += eggCatcherPresenter.DeactivateSpawner;

    }

    private void DeactivateEvents()
    {
        sceneRoot.GoToMainMenu -= HandleGoToMainMenu;
        sceneRoot.GoToTryAgain -= HandleGoToTryAgain;

        gameProgressPresenter.OnGetSelectGame -= levelPresenter.ChooseLevel;
        levelPresenter.OnSetSpawnerData -= eggCatcherPresenter.SetTimerSpawnerData;

        timerPreparationPresenter.OnStopTimer -= eggCatcherPresenter.StartSpawner;
        timerPreparationPresenter.OnStopTimer -= ActivateMainTimer;
        eggCatcherPresenter.OnEggDown -= scorePresenter.RemoveHealth;
        eggCatcherPresenter.OnEggDown_Position -= pointAnimationPresenter.PlayAnimation;
        eggCatcherPresenter.OnEggWin -= pointAnimationPresenter.PlayAnimation;
        eggCatcherPresenter.OnEggWin_EggValue -= scorePresenter.AddScore;

        scorePresenter.OnGameFailed -= timerMainPresenter.DeactivateTimer;
        scorePresenter.OnGameFailed -= eggCatcherPresenter.DeactivateSpawner;
        scorePresenter.OnGameFailed -= basketPresenter.Stop;
        scorePresenter.OnGameFailed -= sceneRoot.OpenFailGamePanel;

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
        bankPresenter?.Dispose();
        basketPresenter?.Dispose();
        eggCatcherPresenter?.Dispose();
        pointAnimationPresenter?.Dispose();
        timerPreparationPresenter?.Dispose();
        timerMainPresenter?.Dispose();
        gameProgressPresenter?.Dispose();
        levelPresenter?.Dispose();
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
        Dispose();
        GoToMainMenu?.Invoke();
    }

    private void HandleGoToTryAgain()
    {
        Dispose();
        GoToTryAgain?.Invoke();
    }

    #endregion
}
