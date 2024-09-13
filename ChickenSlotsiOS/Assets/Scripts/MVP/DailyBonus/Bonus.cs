using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private Transform transformBonus;
    [SerializeField] private int coins;

    public Transform TransformBonus => transformBonus;
    public int Coins => coins;
}
