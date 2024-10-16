using System;

public class EggCatcherPresenter
{
    private EggCatcherModel eggCatcherModel;
    private EggCatcherView eggCatcherView;

    public EggCatcherPresenter(EggCatcherModel eggCatcherModel, EggCatcherView eggCatcherView)
    {
        this.eggCatcherModel = eggCatcherModel;
        this.eggCatcherView = eggCatcherView;
    }

    public void Initialize()
    {
        ActivateActions();

        eggCatcherView.Initialize();
        eggCatcherModel.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        eggCatcherView.Dispose();
        eggCatcherModel.Dispose();
    }

    private void ActivateActions()
    {
        eggCatcherView.OnEggWin += eggCatcherModel.EggWin;
        eggCatcherView.OnEggDown += eggCatcherModel.EggDown;

        eggCatcherModel.OnSpawnEgg += eggCatcherView.Spawn;
    }

    private void DeactivateEvents()
    {
        eggCatcherView.OnEggWin -= eggCatcherModel.EggWin;
        eggCatcherView.OnEggDown -= eggCatcherModel.EggDown;

        eggCatcherModel.OnSpawnEgg -= eggCatcherView.Spawn;
    }

    #region Input

    public event Action OnEggDown
    {
        add { eggCatcherModel.OnEggDown += value; }
        remove { eggCatcherModel.OnEggDown -= value; }
    }

    public event Action<EggValue> OnEggWin
    {
        add { eggCatcherModel.OnEggWin += value; }
        remove { eggCatcherModel.OnEggWin -= value; }
    }

    public void StartSpawner()
    {
        eggCatcherModel.ActivateSpawner();
    }

    public void DeactivateSpawner()
    {
        eggCatcherModel.DeactivateSpawner();
    }

    public void PauseSpawner()
    {
        eggCatcherModel.PauseSpawner();
    }

    public void ResumeSpawner()
    {
        eggCatcherModel.ResumeSpawner();
    }

    #endregion
}
