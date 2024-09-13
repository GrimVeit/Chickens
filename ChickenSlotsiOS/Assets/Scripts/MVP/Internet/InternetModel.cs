using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetModel
{
    public event Action<string> OnGetStatusDescription;
    public event Action OnInternetAvailable;
    public event Action OnInternetUnvailable;

    public void StartCheckInternet()
    {
        Coroutines.Start(CheckInternet_Coroutine());
    }

    private IEnumerator CheckInternet_Coroutine()
    {
        while (Application.internetReachability == NetworkReachability.NotReachable)
        {
            OnInternetUnvailable?.Invoke();
            Debug.Log("Подключения к интернету нет");
            OnGetStatusDescription?.Invoke("Unable to connect. Please check your internet connection");
            yield return new WaitForSeconds(1);
        }

        Debug.Log("Подключение к интернету есть");
        OnGetStatusDescription?.Invoke("Connected successfully. You are now online");
        OnInternetAvailable?.Invoke();
    }
}
