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
    //public event Action OnClickLogOutButton;
    //public event Action OnClickDeleteAccButton;

    public event Action<string> OnRegisterLoginValueChanged;

    [SerializeField] private TMP_InputField fieldLoginRegistration;
    [SerializeField] private GameObject buttonObject;
    [SerializeField] private Button registrationButton;

    [SerializeField] private TextMeshProUGUI textDescriptionRegisterLogin;

    public void Initialize()
    {
        registrationButton.onClick.AddListener(HandlerClickToRegistrationButton);
        fieldLoginRegistration.onValueChanged.AddListener(HandlerOnRegisterLoginValueChanged);
    }

    public void Dispose()
    {
        registrationButton.onClick.RemoveListener(HandlerClickToRegistrationButton);
        fieldLoginRegistration.onValueChanged.RemoveListener(HandlerOnRegisterLoginValueChanged);
    }

    public void OnCorrectRegisterLogin()
    {
        buttonObject.SetActive(true);

        textDescriptionRegisterLogin.text = "";
    }

    public void OnIncorrectRegisterLogin(string textError)
    {
        buttonObject.SetActive(false);

        textDescriptionRegisterLogin.text = textError;
    }

    #region Input

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
