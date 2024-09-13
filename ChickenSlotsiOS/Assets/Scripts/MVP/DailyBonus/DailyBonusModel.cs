using System;

public class DailyBonusModel
{
    public event Action OnAvailableBonusButton;
    public event Action OnUnvailableBonusButton;

    public event Action<int> OnGetBonus;
    public event Action OnActivateSpin;

    private bool isActive = true;

    public void Spin()
    {
        if(isActive)
          OnActivateSpin?.Invoke();
    }

    public void SetUnvailable()
    {
        isActive = false;
        OnUnvailableBonusButton?.Invoke();
    }

    public void SetAvailable()
    {
        isActive = true;
        OnAvailableBonusButton?.Invoke();
    }

    public void GetBonus(int bonus)
    {
        OnGetBonus?.Invoke(bonus);
    }
}
