using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame2SceneEntryPoint_Compaign : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private LevelMinigame2_Datas levelMinigame2_Datas;
    [SerializeField] private UIMiniGame2SceneRoot_Compaign sceneRootPrefab;

    private UIMiniGame2SceneRoot_Compaign sceneRoot;
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
    private GameHistoryPresenter gameHistoryPresenter;

    private LevelMinigame2_Presenter levelPresenter;

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

        eggCatcherPresenter = new EggCatcherPresenter(new EggCatcherModel(2, 0.6f, 0.02f, soundPresenter, particleEffectPresenter), viewContainer.GetView<EggCatcherView>());
        eggCatcherPresenter.Initialize();

        basketPresenter = new BasketPresenter(new BasketModel(4, 0, bankPresenter, soundPresenter), viewContainer.GetView<BasketView_ChooseButttonView>());
        basketPresenter.Initialize();

        scorePresenter = new ScorePresenter(new ScoreModel(bankPresenter, soundPresenter), viewContainer.GetView<ScoreView>());
        scorePresenter.Initialize();

        pointAnimationPresenter = new PointAnimationPresenter(new PointAnimationModel(), viewContainer.GetView<PointAnimationView_BabyChicken>());
        pointAnimationPresenter.Initialize();

        timerPreparationPresenter = new TimerPresenter(new TimerModel(), viewContainer.GetView<TimerView>("Preparation"));
        timerPreparationPresenter.Initialize();

        timerMainPresenter = new TimerPresenter(new TimerModel(), viewContainer.GetView<TimerView_MinutesSeconds>("Main"));
        timerMainPresenter.Initialize();

        gameProgressPresenter = new GameProgressPresenter(new GameProgressModel(), viewContainer.GetView<GameProgressView>());

        levelPresenter = new LevelMinigame2_Presenter
            (new LevelMinigame2_Model(levelMinigame2_Datas), 
            viewContainer.GetView<LevelMinigame2_View>());
        levelPresenter.Initailize();

        gameHistoryPresenter = new GameHistoryPresenter(new GameHistoryModel(PlayerPrefsKeys.GAME_HISTORY_TYPE));
        gameHistoryPresenter.Initialize();

        ActivateEvents();

        gameProgressPresenter.Initialize();

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.SetParticleProvider(particleEffectPresenter);
        sceneRoot.Initialize();

        gameHistoryPresenter.SetCurrentTypeGame(TypeGame.Campaign);
        timerPreparationPresenter.ActivateTimer(3);
        basketPresenter.Start();
    }

    private void ActivateEvents()
    {
        sceneRoot.GoToMainMenu += HandleGoToMainMenu;
        sceneRoot.TryAgain += HandleGoToTryAgain;
        gameProgressPresenter.OnGoToGame1 += HandleGoToGame1;
        gameProgressPresenter.OnGoToGame2 += HandleGoToGame2;
        gameProgressPresenter.OnGoToGame3 += HandleGoToGame3;

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
        timerMainPresenter.OnStopTimer += gameProgressPresenter.CompleteGame;
        timerMainPresenter.OnStopTimer += gameProgressPresenter.UnlockSecondGame;
        timerMainPresenter.OnStopTimer += eggCatcherPresenter.DeactivateSpawner;

    }

    private void DeactivateEvents()
    {
        sceneRoot.GoToMainMenu -= HandleGoToMainMenu;
        sceneRoot.TryAgain -= HandleGoToTryAgain;
        gameProgressPresenter.OnGoToGame1 -= HandleGoToGame1;
        gameProgressPresenter.OnGoToGame2 -= HandleGoToGame2;
        gameProgressPresenter.OnGoToGame3 -= HandleGoToGame3;

        gameProgressPresenter.OnGetSelectGame -= levelPresenter.ChooseLevel;
        levelPresenter.OnSetSpawnerData -= eggCatcherPresenter.SetTimerSpawnerData;

        timerPreparationPresenter.OnStopTimer -= eggCatcherPresenter.StartSpawner;
        timerPreparationPresenter.OnStopTimer -= ActivateMainTimer;
        eggCatcherPresenter.OnEggDown -= scorePresenter.RemoveHealth;
        eggCatcherPresenter.OnEggDown_EggValue -= pointAnimationPresenter.PlayAnimation;
        eggCatcherPresenter.OnEggWin_EggValue -= scorePresenter.AddScore;

        scorePresenter.OnGameFailed -= timerMainPresenter.DeactivateTimer;
        scorePresenter.OnGameFailed -= basketPresenter.Stop;
        scorePresenter.OnGameFailed -= eggCatcherPresenter.DeactivateSpawner;
        scorePresenter.OnGameFailed -= sceneRoot.OpenFailGamePanel;

        timerMainPresenter.OnStopTimer -= basketPresenter.Stop;
        timerMainPresenter.OnStopTimer -= sceneRoot.OpenWinGamePanel;
        timerMainPresenter.OnStopTimer -= gameProgressPresenter.CompleteGame;
        timerMainPresenter.OnStopTimer -= gameProgressPresenter.UnlockSecondGame;
        timerMainPresenter.OnStopTimer -= eggCatcherPresenter.DeactivateSpawner;
    }

    public void Dispose()
    {
        eggCatcherPresenter.DeactivateSpawner();
        DeactivateEvents();

        sceneRoot?.Dispose();
        scorePresenter?.Dispose();
        eggCatcherPresenter?.Dispose();
        basketPresenter?.Dispose();
        bankPresenter?.Dispose();
        pointAnimationPresenter?.Dispose();
        timerPreparationPresenter?.Dispose();
        timerMainPresenter?.Dispose();
        gameProgressPresenter?.Dispose();
        levelPresenter?.Dispose();
        gameHistoryPresenter?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    private void OnApplicationQuit()
    {
        gameHistoryPresenter.DeleteHistory();
    }

    private void ActivateMainTimer()
    {
        timerMainPresenter.ActivateTimer(90);
    }

    #region Input

    public event Action GoToMainMenu;
    public event Action GoToTryAgain;
    public event Action GoToGame1;
    public event Action GoToGame2;
    public event Action GoToGame3;

    private void HandleGoToMainMenu()
    {
        soundPresenter?.Dispose();
        GoToMainMenu?.Invoke();
    }

    private void HandleGoToTryAgain()
    {
        soundPresenter?.Dispose();
        GoToTryAgain?.Invoke();
    }

    private void HandleGoToGame1()
    {
        soundPresenter?.Dispose();
        GoToGame1?.Invoke();
    }

    private void HandleGoToGame2()
    {
        soundPresenter?.Dispose();
        GoToGame2?.Invoke();
    }

    private void HandleGoToGame3()
    {
        soundPresenter?.Dispose();
        GoToGame3?.Invoke();
    }

    #endregion
}
