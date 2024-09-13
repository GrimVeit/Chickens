using System;
using System.Collections.Generic;
using UnityEngine;

public class Slots2SceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private Combination combinations;
    [SerializeField] private BetAmounts betAmounts;
    [SerializeField] private UISlots2SceneRoot sceneRootPrefab;

    private UISlots2SceneRoot sceneRoot;
    private ViewContainer viewContainer;

    private WebViewPresenter privacyPolicyWebViewPresenter;
    private WebViewPresenter aboutWebViewPresenter;

    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;
    private BankPresenter bankPresenter;
    private SlotMachinePresenter slotMachinePresenter;

    private ISoundProvider soundProvider;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = Instantiate(sceneRootPrefab);
        sceneRoot.Initialize();
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        privacyPolicyWebViewPresenter = new WebViewPresenter
            (new WebViewModel("https://fgo.today/policy"),
            viewContainer.GetView<WebViewView>("PrivacyPolicy"));
        privacyPolicyWebViewPresenter.Initialize();

        aboutWebViewPresenter = new WebViewPresenter
            (new WebViewModel("https://fgo.today/about"),
            viewContainer.GetView<WebViewView>("About"));
        aboutWebViewPresenter.Initialize();

        particleEffectPresenter = new ParticleEffectPresenter
            (new ParticleEffectModel(),
            viewContainer.GetView<ParticleEffectView>());
        particleEffectPresenter.Initialize();

        soundPresenter = new SoundPresenter(new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS), viewContainer.GetView<SoundView>());
        soundPresenter.Initialize();

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        bankPresenter.Initialize();

        slotMachinePresenter = new SlotMachinePresenter
            (new SlotMachineModel(3, 1, combinations, betAmounts, bankPresenter, soundPresenter, particleEffectPresenter),
            viewContainer.GetView<SlotMachineView>());
        slotMachinePresenter.Initialize();

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Activate();

        //soundProvider = soundPresenter;
        //soundProvider.Play("Background");

        ActivateActions();
    }

    private void ActivateActions()
    {
        sceneRoot.GoToMainMenu += HandleGoToMainMenu;

        sceneRoot.OnActivatePrivacyPolicyPanel += privacyPolicyWebViewPresenter.Load;
        sceneRoot.OnActivateAboutPanel += aboutWebViewPresenter.Load;

        privacyPolicyWebViewPresenter.OnHidePage += sceneRoot.ClosePrivacyPolicyPanel;
        aboutWebViewPresenter.OnHidePage += sceneRoot.CloseAboutPanel;
    }

    private void DeactivateActions()
    {
        sceneRoot.GoToMainMenu -= HandleGoToMainMenu;

        sceneRoot.OnActivatePrivacyPolicyPanel -= privacyPolicyWebViewPresenter.Load;
        sceneRoot.OnActivateAboutPanel -= aboutWebViewPresenter.Load;

        privacyPolicyWebViewPresenter.OnHidePage -= sceneRoot.ClosePrivacyPolicyPanel;
        aboutWebViewPresenter.OnHidePage -= sceneRoot.CloseAboutPanel;
    }

    private void Dispose()
    {
        DeactivateActions();
        sceneRoot?.Deactivate();

        sceneRoot?.Dispose();
        privacyPolicyWebViewPresenter?.Dispose();
        aboutWebViewPresenter?.Dispose();
        particleEffectPresenter?.Dispose();
        soundPresenter?.Dispose();
        slotMachinePresenter?.Dispose();
        bankPresenter?.Dispose();
    }

    #region Input Actions

    public event Action GoToMainMenu;

    private void HandleGoToMainMenu()
    {
        sceneRoot.GoToMainMenu -= HandleGoToMainMenu;
        Dispose();
        GoToMainMenu?.Invoke();
    }

    #endregion
}
