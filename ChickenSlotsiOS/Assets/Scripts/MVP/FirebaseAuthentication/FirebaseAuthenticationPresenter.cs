//using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseAuthenticationPresenter
{
    private FirebaseAuthenticationModel firebaseAuthenticationModel;
    private FirebaseAuthenticationView firebaseAuthenticationView;

    public FirebaseAuthenticationPresenter(FirebaseAuthenticationModel firebaseAuthenticationModel, FirebaseAuthenticationView firebaseAuthenticationView)
    {
        this.firebaseAuthenticationModel = firebaseAuthenticationModel;
        this.firebaseAuthenticationView = firebaseAuthenticationView;
    }

    public void Initialize()
    {
        ActivateEvents();

        firebaseAuthenticationModel.Initialize();
        firebaseAuthenticationView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        //firebaseAuthenticationModel.D
        firebaseAuthenticationView.Dispose();
    }

    private void ActivateEvents()
    {
        firebaseAuthenticationView.OnClickSignUpButton += firebaseAuthenticationModel.SignUp;
        firebaseAuthenticationView.OnClickRandomNicknameButton += firebaseAuthenticationModel.RandomNickname;
        firebaseAuthenticationView.OnRegisterLoginValueChanged += firebaseAuthenticationModel.ChangeEnterLoginValue;

        firebaseAuthenticationModel.OnEnterRegisterLoginError += firebaseAuthenticationView.OnIncorrectRegisterLogin;
        firebaseAuthenticationModel.OnEnterRegisterLoginSuccess += firebaseAuthenticationView.OnCorrectRegisterLogin;
        firebaseAuthenticationModel.OnGetRandomNickname += firebaseAuthenticationView.DisplayRandomNickname;
        firebaseAuthenticationModel.OnSignUpMessage_Action += firebaseAuthenticationView.GetMessage;
    }

    private void DeactivateEvents()
    {
        firebaseAuthenticationView.OnClickSignUpButton -= firebaseAuthenticationModel.SignUp;
        firebaseAuthenticationView.OnClickRandomNicknameButton -= firebaseAuthenticationModel.RandomNickname;
        firebaseAuthenticationView.OnRegisterLoginValueChanged -= firebaseAuthenticationModel.ChangeEnterLoginValue;

        firebaseAuthenticationModel.OnEnterRegisterLoginError -= firebaseAuthenticationView.OnIncorrectRegisterLogin;
        firebaseAuthenticationModel.OnEnterRegisterLoginSuccess -= firebaseAuthenticationView.OnCorrectRegisterLogin;
        firebaseAuthenticationModel.OnGetRandomNickname -= firebaseAuthenticationView.DisplayRandomNickname;
    }

    #region Input

    public bool CheckAuthenticated()
    {
        return firebaseAuthenticationModel.CheckUserAuthentication();
    }

    public void DeleteAccount()
    {
        firebaseAuthenticationModel.DeleteAccount();
    }

    public void SignOut()
    {
        firebaseAuthenticationModel.SignOut();
    }

    public event Action<string> OnChangeCurrentUser
    {
        add { firebaseAuthenticationModel.OnChangeUser += value; }
        remove { firebaseAuthenticationModel.OnChangeUser -= value; }
    }

    public event Action OnSignIn
    {
        add { firebaseAuthenticationModel.OnSignIn_Action += value; }
        remove { firebaseAuthenticationModel.OnSignIn_Action -= value; }
    }

    public event Action OnSignUp
    {
        add { firebaseAuthenticationModel.OnSignUp_Action += value; }
        remove { firebaseAuthenticationModel.OnSignUp_Action -= value; }
    }

    public event Action OnSignOut
    {
        add { firebaseAuthenticationModel.OnSignOut_Action += value; }
        remove { firebaseAuthenticationModel.OnSignOut_Action -= value; }
    }

    public event Action OnDeleteAccoun
    {
        add { firebaseAuthenticationModel.OnDeleteAccount_Action += value; }
        remove { firebaseAuthenticationModel.OnDeleteAccount_Action -= value; }
    }

    #endregion
}
