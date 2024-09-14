using System;
using UnityEngine;

public class MiniGameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UIMiniGameSceneRoot sceneRootPrefab;

    private UIMiniGameSceneRoot sceneRoot;
    private ViewContainer viewContainer;

    private BankPresenter bankPresenter;
    private EggCatcherPresenter eggCatcherPresenter;
    private BasketPresenter basketPresenter;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = Instantiate(sceneRootPrefab);
        sceneRoot.Initialize();
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        bankPresenter.Initialize();

        eggCatcherPresenter = new EggCatcherPresenter(new EggCatcherModel(), viewContainer.GetView<EggCatcherView>());
        eggCatcherPresenter.Initialize();

        basketPresenter = new BasketPresenter(new BasketModel(bankPresenter), viewContainer.GetView<BasketView>());
        basketPresenter.Initialize();

        ActivateEvents();

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
