using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WinCombinations", menuName = "Slots/Win combinations")]
public class WinCombinations : ScriptableObject
{
    public List<SlotCombination> combinations;
}

public class SlotCombination
{
    [SerializeField] private int[] id;
    [SerializeField] private float multiply;

    public int[] ID => id;

    public float Multiply => multiply;
}
