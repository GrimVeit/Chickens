using System;
using System.Collections;
using UnityEngine;

public class TimerModel
{
    public event Action OnActivateTimer;
    public event Action OnDeactivateTimer;
    public event Action OnStartTimer;
    public event Action OnStopTimer;
    public event Action<int> OnItterationTimer;

    private bool isActive;

    private IEnumerator timerCoroutine;

    public void ActivateTimer(int seconds)
    {
        isActive = true;

        if (timerCoroutine != null)
            Coroutines.Stop(timerCoroutine);

        timerCoroutine = Timer_Coroutine(seconds);
        Coroutines.Start(timerCoroutine);

        OnActivateTimer?.Invoke();
    }

    public void DeactivateTimer()
    {
        isActive = false;

        if (timerCoroutine != null)
            Coroutines.Stop(timerCoroutine);

        OnDeactivateTimer?.Invoke();
    }

    private IEnumerator Timer_Coroutine(int seconds)
    {
        OnStartTimer?.Invoke();

        int duration = seconds;

        while(duration > 0)
        {
            OnItterationTimer?.Invoke(duration);
            yield return new WaitForSeconds(1);
            duration -= 1;
        }

        if(isActive)
           OnStopTimer?.Invoke();
    }
}
