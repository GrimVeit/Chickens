using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound : ISound
{
    public string ID => id;

    public float Volume => audioSource.volume;

    [SerializeField] private string id;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float volume;
    [SerializeField] private float pitch;
    [SerializeField] private bool isLoop;
    [SerializeField] private bool isPlayAwake;

    private float normalVolume;
    private float durationChangeVolume = 0.2f;

    private bool isMainControl;

    private IEnumerator setVolume_Coroutine;

    public void Initialize()
    {
        normalVolume = volume;

        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.loop = isLoop;

        SetVolume(0, normalVolume);

        if (isPlayAwake)
            audioSource.Play();
    }

    public void MainMute()
    {
        isMainControl = true;

        SetVolume(normalVolume, 0, () => audioSource.mute = true);
    }

    public void MainUnmute()
    {
        audioSource.mute = false;
        isMainControl = false;

        SetVolume(0, normalVolume);
    }

    public void Mute()
    {
        if (isMainControl) return;

        audioSource.mute = true;
    }

    public void Unmute()
    {
        if (isMainControl) return;

        audioSource.mute = false;
    }

    public void SetPitch(float pitch)
    {
        audioSource.pitch = pitch;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void Play()
    {
        audioSource.Play();
    }

    public void PlayOneShot()
    {
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(audioClip);
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void Dispose()
    {
        SetVolume(normalVolume, 0);
    }

    public void SetVolume(float startVolume, float endVolume, Action action = null)
    {
        Coroutines.Start(ChangeVolume_Coroutine(startVolume, endVolume, action));
    }

    private IEnumerator ChangeVolume_Coroutine(float startVolume, float endVolume, Action actionOnend)
    {
        if (audioSource == null) yield break;

        if(audioSource.volume == endVolume) yield break;

        audioSource.volume = startVolume;
        float elapsedTime = 0f;

        while (elapsedTime < durationChangeVolume)
        {
            elapsedTime += Time.deltaTime;
            if (audioSource == null) yield break;
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, elapsedTime / durationChangeVolume);
            yield return null;
        }

        actionOnend?.Invoke();
    }
}

public interface ISound
{
    public float Volume { get;  }
    public void Play();
    public void PlayOneShot();
    public void Stop();
    public void SetVolume(float vol);
    public void SetVolume(float startVolume, float endVolume, Action action = null); 
    public void SetPitch(float pitch);
}
