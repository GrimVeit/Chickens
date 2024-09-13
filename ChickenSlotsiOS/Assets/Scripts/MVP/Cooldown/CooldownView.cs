using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CooldownView : View, IIdentify
{
    [SerializeField] private string viewID;

    public event Action OnClickCooldownButton;

    [SerializeField] private Button cooldownButton;
    [SerializeField] private Image buttonImage;
    [SerializeField] private Sprite spriteActivatedButton;
    [SerializeField] private Sprite spriteDeactivatedButton;
    [SerializeField] private TextMeshProUGUI textCountdown;

    public string GetID() => viewID;

    public void Initialize()
    {
        cooldownButton.onClick.AddListener(HandlerClickToCooldownButton);
    }

    public void Dispose()
    {
        cooldownButton.onClick.RemoveListener(HandlerClickToCooldownButton);
    }

    public void ChangeTimer(string time)
    {
        textCountdown.text = time;
    }

    public void ActivateButton()
    {
        buttonImage.sprite = spriteActivatedButton;
        textCountdown.gameObject.SetActive(false);
    }

    public void DeactivateButton()
    {
        buttonImage.sprite = spriteDeactivatedButton;
        textCountdown.gameObject.SetActive(true);
    }

    private void HandlerClickToCooldownButton()
    {
        OnClickCooldownButton?.Invoke();
    }
}
