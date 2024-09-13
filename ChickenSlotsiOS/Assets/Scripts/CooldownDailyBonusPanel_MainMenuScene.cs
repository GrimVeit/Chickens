using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownDailyBonusPanel_MainMenuScene : MovePanel
{
    [SerializeField] private Button backButton;

    public event Action OnClickBackButton;

    public override void Initialize()
    {
        base.Initialize();

        backButton.onClick.AddListener(() => OnClickBackButton?.Invoke());
    }
}
