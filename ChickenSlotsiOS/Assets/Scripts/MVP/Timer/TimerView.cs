using TMPro;
using UnityEngine;

public class TimerView : View, IIdentify
{
    public string GetID() => id;

    [SerializeField] private string id;
    [SerializeField] private TextMeshProUGUI textCount;

    public void ChangeTime(int sec)
    {
        textCount.text = sec.ToString();
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
