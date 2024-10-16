using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCatcherModel
{
    public event Action OnEggDown;
    public event Action<EggValue> OnEggWin;

    public event Action OnSpawnEgg;

    private float initialDelay = 2f;
    private float minDelay = 0.4f;
    private float decreaseAmount = 0.02f;
    private float currentDelay;

    private IEnumerator spawnEggs_ienumerator;
    private bool isPaused = false;

    private ISoundProvider soundProvider;
    private IParticleEffectProvider particleEffectProvider;

    public EggCatcherModel(ISoundProvider soundProvider, IParticleEffectProvider particleEffectProvider)
    {
        this.soundProvider = soundProvider;
        this.particleEffectProvider = particleEffectProvider;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public void EggWin(EggValues eggValues)
    {
        OnEggWin?.Invoke(eggValues.EggValue);
    }

    public void EggDown()
    {
        OnEggDown?.Invoke();
    }

    #region Spawner

    public void ActivateSpawner()
    {
        if (spawnEggs_ienumerator != null)
            Coroutines.Stop(spawnEggs_ienumerator);

        spawnEggs_ienumerator = SpawnEggs_Coroutine();
        Coroutines.Start(spawnEggs_ienumerator);

        Debug.Log("����� ��������");
    }

    public void DeactivateSpawner()
    {
        if (spawnEggs_ienumerator != null)
            Coroutines.Stop(spawnEggs_ienumerator);

        Debug.Log("����� ��������");
    }

    public void PauseSpawner()
    {
        isPaused = true;

        Debug.Log("����� ��������");
    }

    public void ResumeSpawner()
    {
        if (spawnEggs_ienumerator == null)
            ActivateSpawner();

            isPaused = false;

        Debug.Log("����������� ��������");
    }

    private IEnumerator SpawnEggs_Coroutine()
    {
        currentDelay = initialDelay;

        while (true)
        {
            yield return new WaitUntil(() => !isPaused);

            if(!isPaused)
               OnSpawnEgg?.Invoke();

            currentDelay = Mathf.Max(currentDelay - decreaseAmount, minDelay);
            Debug.Log(currentDelay);

            yield return new WaitForSeconds(currentDelay);

        }
    }

    #endregion
}
