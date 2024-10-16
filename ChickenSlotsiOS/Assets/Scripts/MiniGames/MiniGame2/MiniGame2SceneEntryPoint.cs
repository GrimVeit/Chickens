using System;
using UnityEngine;

public class MiniGame2SceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UIMiniGame2SceneRoot sceneRootPrefab;

    private UIMiniGame2SceneRoot sceneRoot;
    private ViewContainer viewContainer;

    private SoundPresenter soundPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private BankPresenter bankPresenter;

    private EggCatcherPresenter eggCatcherPresenter;
    private BasketPresenter basketPresenter;
    private ScorePresenter scorePresenter;

    private ISoundProvider soundProvider;

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

        eggCatcherPresenter = new EggCatcherPresenter(new EggCatcherModel(soundPresenter, particleEffectPresenter), viewContainer.GetView<EggCatcherView>());
        eggCatcherPresenter.Initialize();

        basketPresenter = new BasketPresenter(new BasketModel(bankPresenter, soundPresenter), viewContainer.GetView<BasketView_ChooseButttonView>());
        basketPresenter.Initialize();

        scorePresenter = new ScorePresenter(new ScoreModel(bankPresenter, soundPresenter), viewContainer.GetView<ScoreView>());
        scorePresenter.Initialize();

        ActivateEvents();


        soundProvider = soundPresenter;
        soundProvider.Play("Background");

        sceneRoot.SetSoundProvider(soundProvider);
        sceneRoot.Initialize();

        basketPresenter.Start();
        eggCatcherPresenter.StartSpawner();
    }

    private void ActivateEvents()
    {
        sceneRoot.GoToMainMenu += HandleGoToMainMenu;

        eggCatcherPresenter.OnEggDown += scorePresenter.RemoveHealth;
        eggCatcherPresenter.OnEggWin += scorePresenter.AddScore;

        scorePresenter.OnGameFailed += basketPresenter.Stop;
        scorePresenter.OnGameFailed += eggCatcherPresenter.DeactivateSpawner;
        scorePresenter.OnGameFailed += sceneRoot.OpenFailGamePanel;

    }

    private void DeactivateEvents()
    {
        sceneRoot.GoToMainMenu -= HandleGoToMainMenu;

        eggCatcherPresenter.OnEggDown -= scorePresenter.RemoveHealth;
        eggCatcherPresenter.OnEggWin -= scorePresenter.AddScore;

        scorePresenter.OnGameFailed -= basketPresenter.Stop;
        scorePresenter.OnGameFailed -= eggCatcherPresenter.DeactivateSpawner;
        scorePresenter.OnGameFailed -= sceneRoot.OpenFailGamePanel;
    }

    public void Dispose()
    {
        DeactivateEvents();

        sceneRoot?.Dispose();
        soundPresenter?.Dispose();
        eggCatcherPresenter?.Dispose();
        basketPresenter?.Dispose();
        bankPresenter?.Dispose();
        scorePresenter?.Dispose();
    }

    #region Input

    public event Action GoToMainMenu;

    private void HandleGoToMainMenu()
    {
        Dispose();
        GoToMainMenu?.Invoke();
    }

    #endregion
}
