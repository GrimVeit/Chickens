using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData")]
public class LevelMinigame1_Datas : ScriptableObject
{
    public List<LevelMinigame1_Data> Datas = new List<LevelMinigame1_Data>();
}

[System.Serializable]
public class LevelMinigame1_Data
{
    [SerializeField] private int level;

    [Header("Egg spawner data")]
    [SerializeField] private float initialDelay = 2f;
    [SerializeField] private float minDelay = 0.4f;
    [SerializeField] private float decreaseAmount = 0.02f;

    public int Level => level;
    public float InitialDelay => initialDelay;
    public float MinDelay => minDelay;
    public float DecreaseAmount => decreaseAmount;

}
