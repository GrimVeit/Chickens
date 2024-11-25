using System;

public class TimerPresenter
{
    private TimerModel timerModel;
    private ITimerView timerView;

    public TimerPresenter(TimerModel timerModel, ITimerView timerView)
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
        timerModel.OnActivateTimer += timerView.ActivateTimer;
        timerModel.OnStopTimer += timerView.DeactivateTimer;

        timerModel.OnItterationTimer += timerView.ChangeTime;
    }

    private void DeactivateEvents()
    {
        timerModel.OnActivateTimer -= timerView.ActivateTimer;
        timerModel.OnStopTimer -= timerView.DeactivateTimer;

        timerModel.OnItterationTimer -= timerView.ChangeTime;
    }

    #region Input

    public void ActivateTimer(int seconds)
    {
        timerModel.ActivateTimer(seconds);
    }

    public void DeactivateTimer()
    {
        timerModel.DeactivateTimer();
    }

    public event Action OnStopTimer
    {
        add { timerModel.OnStopTimer += value; }
        remove { timerModel.OnStopTimer -= value; }
    }

    #endregion
}

public interface ITimerView
{
    public void ChangeTime(int sec);

    public void ActivateTimer();

    public void DeactivateTimer();
}
