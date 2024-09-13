using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoinNominals", menuName = "Mini game/Coin nominals")]
public class MiniGameCoinNominals : ScriptableObject
{
    public List<int> Nominals = new List<int>();
}
