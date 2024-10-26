using System;

public class TimerPresenter
{
    private TimerModel timerModel;
    private TimerView timerView;

    public TimerPresenter(TimerModel timerModel, TimerView timerView)
    {
        this.timerModel = timerModel;
        this.timerView = timerView;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        timerModel.OnStartTimer += timerView.ActivateTimer;
        timerModel.OnStopTimer += timerView.DeactivateTimer;

        timerModel.OnItterationTimer += timerView.ChangeTime;
    }

    private void DeactivateEvents()
    {
        timerModel.OnStartTimer -= timerView.ActivateTimer;
        timerModel.OnStopTimer -= timerView.DeactivateTimer;

        timerModel.OnItterationTimer -= timerView.ChangeTime;
    }

    #region Input

    public void StartTimer(int seconds)
    {
        timerModel.StartTimer(seconds);
    }

    public void StopTimer()
    {
        timerModel.StopTimer();
    }

    public event Action OnStopTimer
    {
        add { timerModel.OnStopTimer += value; }
        remove { timerModel.OnStopTimer -= value; }
    }

    #endregion
}
