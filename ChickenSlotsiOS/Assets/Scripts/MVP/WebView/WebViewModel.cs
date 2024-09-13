using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class WebViewModel
{
    //public event Action OnPageStarted_Action;
    //public event Action OnPageClosed_Action;
    public event Action<string> OnGetLink;
    public event Action OnStartPage;
    public event Action OnFinishPage;
    public event Action<string> OnErrorPage;
    public event Action<string> OnLoad;
    //public event Action OnReload;
    public event Action OnShow;
    public event Action OnHide;

    private string URL;

    public WebViewModel(string URL = null)
    {
        this.URL = URL;
    }

    public void GetLinkInTitleFromURL(string URL)
    {
        Coroutines.Start(GetLinkOnTitle(URL));
    }

    private IEnumerator GetLinkOnTitle(string URL)
    {
        using (UnityWebRequest siteRequest = UnityWebRequest.Get(URL))
        {
            yield return siteRequest.SendWebRequest();

            if (siteRequest.result == UnityWebRequest.Result.Success)
            {
                string html = siteRequest.downloadHandler.text;

                string link = GetLinkFromHTML(html);
                OnGetLink?.Invoke(link);

            }
        }
    }

    public string GetLinkFromHTML(string title)
    {
        var match = Regex.Match(title, @"<title>s*(.+?)s*</title>", RegexOptions.IgnoreCase);
        {
            if (match.Success)
            {
                if (match.Groups[1].Value.StartsWith("https://"))
                {
                    return match.Groups[1].Value;
                }
            }

            return null;
        }
    }

    public void SetURL(string URL)
    {
        this.URL = URL;
    }

    public void Load()
    {
        if (URL == null) 
        {
            Debug.Log("Нет ссылки для отображения");
            return;
        } 

        Debug.Log("Загрузка контента - " + URL);
        OnLoad?.Invoke(URL);
    }

    private void Show()
    {
        Debug.Log("Показ контента - " + URL);
        OnShow?.Invoke();
    }

    private void Hide()
    {
        Debug.Log("Скрытие контента - " + URL);
        OnHide?.Invoke();
    }

    public void OnPageStarted(UniWebView webView, string URL)
    {
        OnStartPage?.Invoke();
    }

    public void OnPageFinished(UniWebView webView, string URL)
    {
        OnFinishPage?.Invoke();
        Show();
    }

    public void OnPageClosed()
    {
        Hide();
    }

    public void OnError(UniWebView webView, int code, string message)
    {
        OnErrorPage?.Invoke(message);
    }

    //public string GetLink(string URL)
    //{
    //    using (UnityWebRequest siteRequest = UnityWebRequest.Get(URL))
    //    {
    //        yield return siteRequest.SendWebRequest();

    //        if (siteRequest.result == UnityWebRequest.Result.Success)
    //        {
    //            string html = siteRequest.downloadHandler.text;

    //            //Debug.Log(GetLinkFromTitle(html));
    //            return html;

    //        }
    //    }
    //}
}
