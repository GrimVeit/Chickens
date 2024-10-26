using System;
using System.Collections;
using UnityEngine;

public class TimerModel
{
    public event Action OnStartTimer;
    public event Action OnStopTimer;
    public event Action<int> OnItterationTimer;

    private IEnumerator timerCoroutine;

    public void StartTimer(int seconds)
    {
        OnStartTimer?.Invoke();
        Coroutines.Start(Timer_Coroutine(seconds));
    }

    public void StopTimer()
    {
        if (timerCoroutine != null)
            Coroutines.Stop(timerCoroutine);

        OnStopTimer?.Invoke();
    }

    private IEnumerator Timer_Coroutine(int seconds)
    {
        int duration = seconds;

        while(duration > 0)
        {
            OnItterationTimer?.Invoke(duration);
            yield return new WaitForSeconds(1);
            duration -= 1;
        }

        StopTimer();
    }
}
