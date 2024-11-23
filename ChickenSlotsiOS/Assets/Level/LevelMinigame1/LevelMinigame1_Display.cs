using UnityEngine;

public class LevelMinigame1_Display : MonoBehaviour
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
