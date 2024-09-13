using System;
using System.Collections;
using UnityEngine;

public class CooldownModel
{
    public event Action OnSetAvailableButton;
    public event Action OnSetUnvailableButton;

    public event Action OnClickToActivatedButton;
    public event Action OnClickToDeactivatedButton;
    public event Action<string> OnCountdownTimer;

    private string ID;
    private readonly string KEY;

    private TimeSpan timeToReload;
    private DateTime nextRewardTime;
    private bool isRewardAvailable => DateTime.Now >= nextRewardTime;

    //private ISoundProvider soundProvider;
    //private IParticleEffectProvider particleEffectProvider;
    //private IParticleEffect effectReload;

    private IEnumerator countdownButton_coroutine;

    public CooldownModel(string key, TimeSpan timeToReload)
    {
        KEY = key;
        this.timeToReload = timeToReload;
        //this.soundProvider = soundProvider;
        //this.particleEffectProvider = particleEffectProvider;
    }

    public void Initialize()
    {
        //effectReload = particleEffectProvider.GetParticleEffect(ID);

        nextRewardTime = DateTime.Parse(PlayerPrefs.GetString(KEY, DateTime.Now.ToString()));
    }

    public void Activate()
    {
        ActivateCountdown();
    }

    public void Deactivate()
    {
        DeactivateCountdown();
        //effectReload.Stop();
    }

    public void SetID(string ID)
    {
        this.ID = ID;
    }

    public void Dispose()
    {

    }

    public void ClickButton()
    {
        if (isRewardAvailable)
        {
            OnClickToActivatedButton?.Invoke();
            return;
        }

        OnClickToDeactivatedButton?.Invoke();
        //soundProvider.PlayOneShot("Error");
    }

    public void ActivateCooldown()
    {
        nextRewardTime = DateTime.Now + timeToReload;
        PlayerPrefs.SetString(KEY, nextRewardTime.ToString());
        ActivateCountdown();

        Debug.Log("Запуск нового таймера");
        //soundProvider.PlayOneShot("Error");
    }

    private void ActivateCountdown()
    {
        DeactivateCountdown();

        countdownButton_coroutine = Countdown_Coroutine();

        Coroutines.Start(countdownButton_coroutine);
    }

    private void DeactivateCountdown()
    {
        if (countdownButton_coroutine != null)
            Coroutines.Stop(countdownButton_coroutine);
    }

    private IEnumerator Countdown_Coroutine()
    {
        //effectReload.Stop();
        OnSetUnvailableButton?.Invoke();

        while (!isRewardAvailable)
        {
            TimeSpan timeRemaining = nextRewardTime - DateTime.Now;

            if (timeRemaining.TotalSeconds == 0)
            {
                OnSetAvailableButton?.Invoke();
                break;
            }

            OnCountdownTimer?.Invoke(string.Format("{0:D2}:{1:D2}:{2:D2}", timeRemaining.Hours, timeRemaining.Minutes, timeRemaining.Seconds));

            yield return new WaitForSeconds(1);
        }

        //soundProvider.PlayOneShot(ID);
        //effectReload.Play();
        OnSetAvailableButton?.Invoke();
    }
}
