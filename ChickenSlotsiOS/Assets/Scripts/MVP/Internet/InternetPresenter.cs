using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetPresenter
{
    private InternetModel internetModel;
    private InternetView internetView;

    public InternetPresenter(InternetModel internetModel, InternetView internetView)
    {
        this.internetModel = internetModel;
        this.internetView = internetView;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    private void ActivateEvents()
    {
        internetModel.OnGetStatusDescription += internetView.OnGetStatusDescription;
    }

    private void DeactivateEvents()
    {
        internetModel.OnGetStatusDescription -= internetView.OnGetStatusDescription;
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    public void StartCkeckInternet()
    {
        internetModel.StartCheckInternet();
    }

    public event Action OnInternetAvailable
    {
        add { internetModel.OnInternetAvailable += value;}
        remove { internetModel.OnInternetAvailable += value; }
    }
}
