using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Egg : MonoBehaviour, IEgg
{
    public event Action OnEggSpawned;
    public event Action OnEggDown;
    public event Action OnEggDestroyed;

    public EggValues GetEggValues() => eggValue;
    private EggValues eggValue;

    [SerializeField] private Image image;

    public void Initialize(EggValues eggValue)
    {
        this.eggValue = eggValue;
        this.image.sprite = eggValue.SpriteEgg;

        OnEggSpawned?.Invoke();
    }

    public void Dispose()
    {
        OnEggDestroyed?.Invoke();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Earth"))
        {
            OnEggDown?.Invoke();
        }
    }
}

public interface IEgg
{
    EggValues GetEggValues();
    void Dispose();
}
