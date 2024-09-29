using System;
using UnityEngine;

public class MiniGameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UIMiniGameSceneRoot sceneRootPrefab;

    private UIMiniGameSceneRoot sceneRoot;
    private ViewContainer viewContainer;

    private SoundPresenter soundPresenter;
    private ParticleEffectPresenter particleEffectPresenter;

    private BankPresenter bankPresenter;
    private EggCatcherPresenter eggCatcherPresenter;
    private BasketPresenter basketPresenter;

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

        basketPresenter = new BasketPresenter(new BasketModel(bankPresenter, soundPresenter), viewContainer.GetView<BasketView>());
        basketPresenter.Initialize();

        ActivateEvents();


        soundProvider = soundPresenter;
        soundProvider.Play("Background");

        sceneRoot.SetSoundProvider(soundProvider);
        sceneRoot.Initialize();

        basketPresenter.Start();
    }

    private void ActivateEvents()
    {
        sceneRoot.GoToMainMenu += HandleGoToMainMenu;

        eggCatcherPresenter.OnFailGame += basketPresenter.Stop;
        eggCatcherPresenter.OnFailGame += sceneRoot.OpenFailGamePanel;


        basketPresenter.OnStartMove += eggCatcherPresenter.ResumeSpawner;
        basketPresenter.OnStopMove += eggCatcherPresenter.PauseSpawner;

    }

    private void DeactivateEvents()
    {
        sceneRoot.GoToMainMenu -= HandleGoToMainMenu;

        eggCatcherPresenter.OnFailGame -= basketPresenter.Stop;
        eggCatcherPresenter.OnFailGame -= sceneRoot.OpenFailGamePanel;

        basketPresenter.OnStartMove -= eggCatcherPresenter.ResumeSpawner;
        basketPresenter.OnStopMove -= eggCatcherPresenter.PauseSpawner;
    }

    public void Dispose()
    {
        DeactivateEvents();

        sceneRoot?.Dispose();
        soundPresenter?.Dispose();
        eggCatcherPresenter?.Dispose();
        basketPresenter?.Dispose();
        bankPresenter?.Dispose();
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
