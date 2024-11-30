using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData_3", menuName = "LevelData_3")]
public class LevelMinigame3_Datas : ScriptableObject
{
    public List<LevelMinigame3_Data> Datas = new List<LevelMinigame3_Data>();
}

[System.Serializable]
public class LevelMinigame3_Data
{
    [SerializeField] private int level;

    [Header("Egg spawner data")]
    [SerializeField] private float initialDelay = 2f;
    [SerializeField] private float minDelay = 0.4f;
    [SerializeField] private float decreaseAmount = 0.02f;
    [SerializeField] private float moveTime;

    public int Level => level;
    public float InitialDelay => initialDelay;
    public float MinDelay => minDelay;
    public float DecreaseAmount => decreaseAmount;
    public float MoveTime => moveTime;
}
