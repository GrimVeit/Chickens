using System;
using UnityEngine;

public class MiniGame1SceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UIMiniGame1SceneRoot sceneRootPrefab;

    private UIMiniGame1SceneRoot sceneRoot;
    private ViewContainer viewContainer;

    private SoundPresenter soundPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private BankPresenter bankPresenter;

    private EggCatcherPresenter eggCatcherPresenter;
    private BasketPresenter basketPresenter;
    private ScorePresenter scorePresenter;
    private PointAnimationPresenter pointAnimationPresenter;
    private TimerPresenter timerPresenter;

    private GameHistoryPresenter gameHistoryPresenter;

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

        timerPresenter = new TimerPresenter(new TimerModel(), viewContainer.GetView<TimerView>());
        timerPresenter.Initialize();

        gameHistoryPresenter = new GameHistoryPresenter(new GameHistoryModel(PlayerPrefsKeys.GAME_HISTORY_TYPE));
        gameHistoryPresenter.Initialize();

        ActivateEvents();

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.SetParticleProvider(particleEffectPresenter);
        sceneRoot.Initialize();

        gameHistoryPresenter.SetCurrentTypeGame(TypeGame.Arcada);
        timerPresenter.ActivateTimer(3);
        basketPresenter.Start();
    }

    private void ActivateEvents()
    {
        sceneRoot.GoToMainMenu += HandleGoToMainMenu;

        timerPresenter.OnStopTimer += eggCatcherPresenter.StartSpawner;
        eggCatcherPresenter.OnEggDown += scorePresenter.RemoveHealth;
        eggCatcherPresenter.OnEggDown_EggValue += pointAnimationPresenter.PlayAnimation;
        eggCatcherPresenter.OnEggWin_EggValue += scorePresenter.AddScore;

        scorePresenter.OnGameFailed += basketPresenter.Stop;
        scorePresenter.OnGameFailed += sceneRoot.OpenFailGamePanel;
        scorePresenter.OnGameFailed += eggCatcherPresenter.DeactivateSpawner;

    }

    private void DeactivateEvents()
    {
        sceneRoot.GoToMainMenu -= HandleGoToMainMenu;

        timerPresenter.OnStopTimer -= eggCatcherPresenter.StartSpawner;
        eggCatcherPresenter.OnEggDown -= scorePresenter.RemoveHealth;
        eggCatcherPresenter.OnEggDown_EggValue -= pointAnimationPresenter.PlayAnimation;
        eggCatcherPresenter.OnEggWin_EggValue -= scorePresenter.AddScore;

        scorePresenter.OnGameFailed -= basketPresenter.Stop;
        scorePresenter.OnGameFailed -= sceneRoot.OpenFailGamePanel;
        scorePresenter.OnGameFailed -= eggCatcherPresenter.DeactivateSpawner;
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
        timerPresenter?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    private void OnApplicationQuit()
    {
        gameHistoryPresenter.DeleteHistory();
    }

    #region Input

    public event Action GoToMainMenu;

    private void HandleGoToMainMenu()
    {
        soundPresenter?.Dispose();
        GoToMainMenu?.Invoke();
    }

    #endregion
}
