using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCatcherModel
{
    public event Action OnSpawnEgg;
    public event Action OnRemoveHealth;
    public event Action<int> OnAddHealth;
    public event Action OnGameFailed;

    private float initialDelay = 2f;
    private float minDelay = 0.01f;
    private float decreaseAmount = 0.02f;
    private float currentDelay;

    private IEnumerator spawnEggs_ienumerator;
    private bool isPaused = true;

    private int health;
    private int currentHealth;

    public void Initialize()
    {
        health = PlayerPrefs.GetInt(PlayerPrefsKeys.HEALTH_COUNT, 1);
    }

    public void Dispose()
    {

    }

    public void ActivateSpawner()
    {
        currentHealth = health;
        OnAddHealth?.Invoke(currentHealth);

        if (spawnEggs_ienumerator != null)
            Coroutines.Stop(spawnEggs_ienumerator);

        spawnEggs_ienumerator = SpawnEggs_Coroutine();
        Coroutines.Start(spawnEggs_ienumerator);
    }

    public void PauseSpawner()
    {
        isPaused = true;
    }

    public void ResumeSpawner()
    {
        if (spawnEggs_ienumerator == null)
            ActivateSpawner();

            isPaused = false;
    }

    public void DeactivateSpawner()
    {
        currentHealth -= 1;

        if (currentHealth > 0)
        {
            OnRemoveHealth?.Invoke();
            return;
        }

        if(currentHealth == 0)
        {
            OnRemoveHealth?.Invoke();

            Debug.Log("�� ���������");

            if (spawnEggs_ienumerator != null)
                Coroutines.Stop(spawnEggs_ienumerator);

            OnGameFailed?.Invoke();
        }
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
}