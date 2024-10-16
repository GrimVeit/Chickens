using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBounceCatcherPresenter
{
    private EggBounceCatcherModel eggBounceCatcherModel;
    private EggBounceCatcherView eggBounceCatcherView;

    public EggBounceCatcherPresenter(EggBounceCatcherModel eggBounceCatcherModel, EggBounceCatcherView eggBounceCatcherView)
    {
        this.eggBounceCatcherModel = eggBounceCatcherModel;
        this.eggBounceCatcherView = eggBounceCatcherView;
    }

    public void Initialize()
    {
        ActivateEvents();

        eggBounceCatcherModel.Initialize();
        eggBounceCatcherView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        eggBounceCatcherModel.Dispose();
        eggBounceCatcherView.Dispose();
    }

    private void ActivateEvents()
    {
        eggBounceCatcherView.OnEggDown += eggBounceCatcherModel.DeactivateSpawner;

        eggBounceCatcherModel.OnSpawnEgg += eggBounceCatcherView.Spawn;
        eggBounceCatcherModel.OnAddHealth += eggBounceCatcherView.AddHealth;
        eggBounceCatcherModel.OnRemoveHealth += eggBounceCatcherView.RemoveHealth;
    }

    private void DeactivateEvents()
    {
        eggBounceCatcherView.OnEggDown -= eggBounceCatcherModel.DeactivateSpawner;

        eggBounceCatcherModel.OnSpawnEgg -= eggBounceCatcherView.Spawn;
        eggBounceCatcherModel.OnAddHealth -= eggBounceCatcherView.AddHealth;
        eggBounceCatcherModel.OnRemoveHealth -= eggBounceCatcherView.RemoveHealth;
    }

    #region Input

    public event Action OnFailGame
    {
        add { eggBounceCatcherModel.OnGameFailed += value; }
        remove { eggBounceCatcherModel.OnGameFailed -= value; }
    }

    public void StartSpawner()
    {
        eggBounceCatcherModel.ActivateSpawner();
    }

    public void PauseSpawner()
    {
        eggBounceCatcherModel.PauseSpawner();
    }

    public void ResumeSpawner()
    {
        eggBounceCatcherModel.ResumeSpawner();
    }

    #endregion
}
