using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        eggCatcherView.OnEggDown += eggCatcherModel.DeactivateSpawner;

        eggCatcherModel.OnSpawnEgg += eggCatcherView.Spawn;
        eggCatcherModel.OnAddHealth += eggCatcherView.AddHealth;
        eggCatcherModel.OnRemoveHealth += eggCatcherView.RemoveHealth;
    }

    private void DeactivateEvents()
    {
        eggCatcherView.OnEggDown -= eggCatcherModel.DeactivateSpawner;

        eggCatcherModel.OnSpawnEgg -= eggCatcherView.Spawn;
        eggCatcherModel.OnAddHealth -= eggCatcherView.AddHealth;
        eggCatcherModel.OnRemoveHealth -= eggCatcherView.RemoveHealth;
    }

    #region Input

    public event Action OnFailGame
    {
        add { eggCatcherModel.OnGameFailed += value; }
        remove { eggCatcherModel.OnGameFailed -= value; }
    }

    public void StartSpawner()
    {
        eggCatcherModel.ActivateSpawner();
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
