using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private UIOtherSceneRoot sceneRootPrefab;

    private UIOtherSceneRoot sceneRoot;

    private ViewContainer viewContainer;
    private WebViewPresenter otherWebViewPresenter;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = Instantiate(sceneRootPrefab);
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        otherWebViewPresenter = new WebViewPresenter (new WebViewModel(), viewContainer.GetView<WebViewView>());
        otherWebViewPresenter.Initialize();

        ActivateActions();
        otherWebViewPresenter.GetLinkInTitleFromURL("https://fgo.today/shop");
    }

    private void ActivateActions()
    {
        otherWebViewPresenter.OnGetLinkFromTitle += GetUrl;
    }

    private void DeactivateActions()
    {
        otherWebViewPresenter.OnGetLinkFromTitle -= GetUrl;
    }

    private void GetUrl(string URL)
    {
        if(URL == null)
        {
            GoToMainMenu?.Invoke();
            return;
        }

        otherWebViewPresenter.SetURL(URL);
        otherWebViewPresenter.Load();
    }

    private void OnDestroy()
    {
        DeactivateActions();

        otherWebViewPresenter.Dispose();
    }

    #region Input

    public event Action GoToMainMenu;

    #endregion
}
