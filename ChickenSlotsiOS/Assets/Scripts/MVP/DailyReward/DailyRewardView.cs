using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardView : View
{
    public event Action OnClickDailyReward;

    [SerializeField] private Button dailyRewardButton;

    public void Initialize()
    {
        dailyRewardButton.onClick.AddListener(HandlerClickToDailyReward);
    }

    public void Dispose()
    {
        dailyRewardButton.onClick.RemoveListener(HandlerClickToDailyReward);
    }

    private void HandlerClickToDailyReward()
    {
        OnClickDailyReward?.Invoke();
    }
}
