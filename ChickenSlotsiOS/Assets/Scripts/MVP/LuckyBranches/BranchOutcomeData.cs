using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BranchOutcomeData
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private BranchOutcome branchOutcome;

    public Sprite Sprite => sprite;
    public BranchOutcome Outcome => branchOutcome;
}

public enum BranchOutcome
{
    None, Coin, Leaf, Parrot
}
