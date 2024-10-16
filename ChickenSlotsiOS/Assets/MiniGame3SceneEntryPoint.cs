using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame3SceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UIMiniGame3SceneRoot sceneRootPrefab;

    private UIMiniGame3SceneRoot sceneRoot;
    private ViewContainer viewContainer;

    private SoundPresenter soundPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private BankPresenter bankPresenter;

    private BasketPlatformPresenter basketPlatformPresenter;
    private EggBounceCatcherPresenter eggBounceCatcherPresenter; 

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

        basketPlatformPresenter = new BasketPlatformPresenter(new BasketPlatformModel(5, soundPresenter), viewContainer.GetView<BasketPlatformView>());
        basketPlatformPresenter.Initialize();

        eggBounceCatcherPresenter = new EggBounceCatcherPresenter(new EggBounceCatcherModel(soundPresenter, particleEffectPresenter), viewContainer.GetView<EggBounceCatcherView>());
        eggBounceCatcherPresenter.Initialize();
        ActivateEvents();


        soundProvider = soundPresenter;
        soundProvider.Play("Background");

        sceneRoot.SetSoundProvider(soundProvider);
        sceneRoot.Initialize();

        eggBounceCatcherPresenter.StartSpawner();
    }

    private void ActivateEvents()
    {
        sceneRoot.GoToMainMenu += HandleGoToMainMenu;

    }

    private void DeactivateEvents()
    {
        sceneRoot.GoToMainMenu -= HandleGoToMainMenu;
    }

    public void Dispose()
    {
        DeactivateEvents();

        sceneRoot?.Dispose();
        soundPresenter?.Dispose();
        bankPresenter?.Dispose();
        basketPlatformPresenter?.Dispose();
        eggBounceCatcherPresenter.Dispose();
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
