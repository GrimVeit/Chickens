using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combination")]
public class Combination : ScriptableObject
{
    public List<SlotGrid> SlotGrids = new List<SlotGrid>();
}

[System.Serializable]
public class SlotGrid
{
    public string name;
    public List<SlotPosition> slotPositions = new List<SlotPosition>();
    public float BetMultyply;
    public WinType WinType;
}

[System.Serializable]
public class SlotPosition
{
    [SerializeField] private int col;
    [SerializeField] private int row;

    public int Row => row - 1;
    public int Col => col - 1;
}

public enum WinType
{
    NoWin, Small, Big
}
