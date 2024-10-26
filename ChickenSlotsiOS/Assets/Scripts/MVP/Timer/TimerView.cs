using TMPro;
using UnityEngine;

public class TimerView : View
{
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
