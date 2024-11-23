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

    private IEnumerator timerCoroutine;

    public void ActivateTimer(int seconds)
    {
        OnActivateTimer?.Invoke();
        Coroutines.Start(Timer_Coroutine(seconds));
    }

    public void DeactivateTimer()
    {
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

        OnStopTimer?.Invoke();
    }
}
