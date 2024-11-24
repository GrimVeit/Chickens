using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelMinigame2_View : View
{
    [SerializeField] private TextMeshProUGUI textLevel;

    public void DisplayLevel(int level)
    {
        textLevel.text = "Level " + level;
    }
}
