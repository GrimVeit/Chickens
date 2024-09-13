using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BranchOutcomeDataContainer", menuName = "Slots/Branch outcome data container")]
public class BranchOutcomeDataContainer : ScriptableObject
{
    public List<BranchOutcomeData> branchOutcomeDatas = new List<BranchOutcomeData>();
}
