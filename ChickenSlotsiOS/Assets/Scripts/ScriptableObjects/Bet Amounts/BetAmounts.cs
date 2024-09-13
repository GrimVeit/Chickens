using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BetAmounts", menuName = "Slots/Bet amounts")]
public class BetAmounts : ScriptableObject
{
    public List<int> betValues = new List<int>();
}
