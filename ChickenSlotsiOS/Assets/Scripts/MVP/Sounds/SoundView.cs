using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundView : View
{
    public event Action OnClickSoundButton;

    [SerializeField] private Button soundButton;

    public void Initialize()
    {
        //soundButton.onClick.AddListener(HandlerClickToSoundButton);
    }

    public void Dispose()
    {
        //soundButton.onClick.RemoveListener(HandlerClickToSoundButton);
    }

    public void MuteDisplay()
    {

    }

    public void UnmuteDisplay()
    {

    }

    private void HandlerClickToSoundButton()
    {
        OnClickSoundButton?.Invoke();
    }
}
