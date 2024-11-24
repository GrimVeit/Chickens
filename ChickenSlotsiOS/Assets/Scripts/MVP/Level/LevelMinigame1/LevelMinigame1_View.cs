using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LevelMinigame1_View : View
{
    [SerializeField] private TextMeshProUGUI textLevel;

    public void DisplayLevel(int level)
    {
        textLevel.text = "Level " + level;
    }
}
