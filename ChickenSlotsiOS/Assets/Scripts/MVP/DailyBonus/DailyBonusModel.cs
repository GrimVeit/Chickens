using System;
using System.Reflection;
using UnityEngine;

public class DailyBonusModel
{
    public event Action OnAvailableBonusButton;
    public event Action OnUnvailableBonusButton;

    public event Action<int> OnGetBonus;
    public event Action OnActivateSpin;

    private bool isActive = true;

    private ISoundProvider soundProvider;
    private ISound soundSpin;

    public DailyBonusModel(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
        soundSpin = soundProvider.GetSound("Spin");
    }

    public void Spin()
    {
        if (isActive)
        {
            OnActivateSpin?.Invoke();
            soundProvider.PlayOneShot("ClickUnlocked");
            soundSpin.SetVolume(0.4f);
            soundSpin.SetPitch(1);
            soundSpin.Play();
            return;
        }

        soundProvider.PlayOneShot("ClickLocked");
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
        soundProvider.PlayOneShot("Success");

        OnGetBonus?.Invoke(bonus);
    }

    public void OnSpin(float speed)
    {
        float currentSpeed = speed / 500;

        if (currentSpeed > 0.1f)
        {
            return;
        }

        soundSpin.SetVolume(currentSpeed / 2);

        float pitch = Mathf.Lerp(1, 0.88f, 1 - currentSpeed);
        soundSpin.SetPitch(pitch * 1f);
    }

    public void OnEndSpin()
    {
        soundSpin.Stop();
    }
}
