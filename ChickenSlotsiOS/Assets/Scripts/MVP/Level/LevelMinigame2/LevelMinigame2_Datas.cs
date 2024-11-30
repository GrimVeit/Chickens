using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData_2", menuName = "LevelData_2")]
public class LevelMinigame2_Datas : ScriptableObject
{
    public List<LevelMinigame2_Data> Datas = new List<LevelMinigame2_Data>();
}

[System.Serializable]
public class LevelMinigame2_Data
{
    [SerializeField] private int level;

    [Header("Egg spawner data")]
    [SerializeField] private float initialDelay = 2f;
    [SerializeField] private float minDelay = 0.4f;
    [SerializeField] private float decreaseAmount = 0.02f;
    [SerializeField] private float moveTime = 1;

    public int Level => level;
    public float InitialDelay => initialDelay;
    public float MinDelay => minDelay;
    public float DecreaseAmount => decreaseAmount;
    public float MoveTime => moveTime;

}
