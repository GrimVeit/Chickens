using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EggValues
{
    [SerializeField] private Sprite spriteEgg;
    [SerializeField] [Range(0, 100)] float dropChance;
    [SerializeField] private EggValue eggValue;

    public Sprite SpriteEgg => spriteEgg;
    public float DropChance => dropChance;
    public EggValue EggValue => eggValue;
}

public enum EggValue
{
    Ten, Hundred, Thousand
}
