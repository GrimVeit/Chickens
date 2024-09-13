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
        //firebaseAuthenticationView.OnClickSignInButton += firebaseAuthenticationModel.SignIn;
        firebaseAuthenticationView.OnClickSignUpButton += firebaseAuthenticationModel.SignUp;
        //firebaseAuthenticationView.OnClickLogOutButton += firebaseAuthenticationModel.SignOut;
        //firebaseAuthenticationView.OnClickLogOutButton += firebaseAuthenticationModel.DeleteAccount;
        firebaseAuthenticationView.OnRegisterLoginValueChanged += firebaseAuthenticationModel.ChangeEnterLoginValue;

        firebaseAuthenticationModel.OnEnterRegisterLoginError += firebaseAuthenticationView.OnIncorrectRegisterLogin;
        firebaseAuthenticationModel.OnEnterRegisterLoginSuccess += firebaseAuthenticationView.OnCorrectRegisterLogin;
    }

    private void DeactivateEvents()
    {
        //firebaseAuthenticationView.OnClickSignInButton -= firebaseAuthenticationModel.SignIn;
        firebaseAuthenticationView.OnClickSignUpButton -= firebaseAuthenticationModel.SignUp;
        //firebaseAuthenticationView.OnClickLogOutButton -= firebaseAuthenticationModel.SignOut;
        //firebaseAuthenticationView.OnClickLogOutButton -= firebaseAuthenticationModel.DeleteAccount;
        firebaseAuthenticationView.OnRegisterLoginValueChanged += firebaseAuthenticationModel.ChangeEnterLoginValue;

        firebaseAuthenticationModel.OnEnterRegisterLoginError += firebaseAuthenticationView.OnIncorrectRegisterLogin;
        firebaseAuthenticationModel.OnEnterRegisterLoginSuccess += firebaseAuthenticationView.OnCorrectRegisterLogin;
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
