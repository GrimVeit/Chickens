using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerView_MinutesSeconds : View, ITimerView, IIdentify
{
    public string GetID() => id;

    [SerializeField] private string id;
    [SerializeField] private TextMeshProUGUI textCount;

    public void ChangeTime(int sec)
    {
        int minutes = sec / 60;
        int seconds = sec % 60;
        textCount.text = $"{minutes}:{seconds:D2}";
    }

    public void ActivateTimer()
    {
        textCount.gameObject.SetActive(true);
    }

    public void DeactivateTimer()
    {
        textCount.gameObject.SetActive(false);
    }
}
