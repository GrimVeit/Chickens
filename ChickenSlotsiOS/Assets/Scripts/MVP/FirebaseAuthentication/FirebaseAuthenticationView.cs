using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseAuthenticationView : View
{
    //public event Action<string, string> OnClickSignInButton;
    public event Action<string> OnClickSignUpButton;
    public event Action OnClickRandomNicknameButton;
    //public event Action OnClickLogOutButton;
    //public event Action OnClickDeleteAccButton;

    public event Action<string> OnRegisterLoginValueChanged;

    [SerializeField] private TextMeshProUGUI textDescription;
    [SerializeField] private TMP_InputField fieldLoginRegistration;
    [SerializeField] private GameObject registerNicknameButtonObject;
    [SerializeField] private Button registrationNicknameButton;
    [SerializeField] private Button randomNicknameButton;

    [SerializeField] private TextMeshProUGUI textDescriptionRegisterLogin;

    public void Initialize()
    {
        registrationNicknameButton.onClick.AddListener(HandlerClickToRegistrationButton);
        randomNicknameButton.onClick.AddListener(HandlerClickToRandomNicknameButton);
        fieldLoginRegistration.onValueChanged.AddListener(HandlerOnRegisterLoginValueChanged);
    }

    public void Dispose()
    {
        registrationNicknameButton.onClick.RemoveListener(HandlerClickToRegistrationButton);
        randomNicknameButton.onClick.RemoveListener(HandlerClickToRandomNicknameButton);
        fieldLoginRegistration.onValueChanged.RemoveListener(HandlerOnRegisterLoginValueChanged);
    }

    public void OnCorrectRegisterLogin()
    {
        registerNicknameButtonObject.SetActive(true);

        textDescriptionRegisterLogin.text = "";
    }

    public void OnIncorrectRegisterLogin(string textError)
    {
        registerNicknameButtonObject.SetActive(false);

        textDescriptionRegisterLogin.text = textError;
    }

    public void DisplayRandomNickname(string text)
    {
        fieldLoginRegistration.text = text;
    }

    public void GetMessage(string message)
    {
        textDescription.text = message;
    }

    #region Input

    private void HandlerClickToRandomNicknameButton()
    {
        OnClickRandomNicknameButton?.Invoke();
    }

    private void HandlerClickToRegistrationButton()
    {
        string login = fieldLoginRegistration.text;

        OnClickSignUpButton?.Invoke(login);
    }

    private void HandlerOnRegisterLoginValueChanged(string value)
    {
        OnRegisterLoginValueChanged?.Invoke(value);
    }

    #endregion
}
