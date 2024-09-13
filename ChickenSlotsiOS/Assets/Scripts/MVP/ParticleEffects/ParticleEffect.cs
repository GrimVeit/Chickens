using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ParticleEffect : IParticleEffect
{
    public string ID;

    //public event Action OnStartParticleEffect;
    //public event Action OnEndParticleEffect;

    private bool isPlaying;

    public List<Particle> particles = new List<Particle>();

    private IEnumerator coroutineParticles;

    public void Initialize()
    {
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Initialize();
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Dispose();
        }
    }

    public void Play()
    {
        if (coroutineParticles != null)
            Coroutines.Stop(coroutineParticles);

        coroutineParticles = PlayParticles_Coroutine();
        Coroutines.Start(coroutineParticles);
    }

    public void Stop()
    {
        if (coroutineParticles != null)
            Coroutines.Stop(coroutineParticles);

        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }
    }

    private IEnumerator PlayParticles_Coroutine()
    {
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Play();
            yield return new WaitForSeconds(particles[i].TimeToInterval);
        }
    }
}

public interface IParticleEffect 
{ 
    //event Action OnStartParticleEffect;
    //event Action OnEndParticleEffect;
    void Play();
    void Stop();
}


[Serializable]
public class Particle
{
    public float TimeToInterval => timeToInterval;

    [SerializeField] private float timeToInterval;
    [SerializeField] private ParticleSystem particleSystemPrefab;
    [SerializeField] private Transform particleTransform;

    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;
    [SerializeField] private bool isPlayAwake;

    private ParticleSystem particleSystem;

    public void Initialize()
    {
        particleSystem = UnityEngine.Object.Instantiate(particleSystemPrefab, particleTransform);
        particleSystem.transform.SetLocalPositionAndRotation(Vector3.zero, particleSystemPrefab.transform.rotation);

        float randomSize = UnityEngine.Random.Range(minSize, maxSize);

        particleSystem.transform.localScale = new Vector3(randomSize, randomSize, randomSize);

        if (isPlayAwake)
            Play();
    }

    public void Dispose()
    {
        //Destroy();
        Stop();
    }

    public void Play()
    {
        if(particleSystem != null)
           particleSystem.Play();
    }

    public void Stop()
    {
        if(particleSystem != null)
           particleSystem.Stop();
    }

    private void Destroy()
    {
        if (particleSystem != null)
            Coroutines.Destroy(particleSystem.gameObject);
    }
}
