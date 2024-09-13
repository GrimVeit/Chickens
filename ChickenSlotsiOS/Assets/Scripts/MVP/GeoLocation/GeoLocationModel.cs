using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class GeoLocationModel
{
    public event Action<string> OnGetCountry;

    private string URL_GET_IP = "https://ipinfo.io/json";

    public void GetUserCountry()
    {
        Coroutines.Start(GetIPInfo_Coroutine());
    }

    private IEnumerator GetIPInfo_Coroutine()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL_GET_IP))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
            }
            else if (request.result == UnityWebRequest.Result.Success)
            {
                var jsonResult = request.downloadHandler.text;
                Debug.Log(jsonResult);
                IPInfo ipInfo = JsonUtility.FromJson<IPInfo>(jsonResult);
                Debug.Log($"IP: {ipInfo.ip}, City: {ipInfo.city}, Region: {ipInfo.region}, Country: {ipInfo.country}");
                OnGetCountry?.Invoke(ipInfo.country);
            }
        }
    }
}

public class IPInfo
{
    public string ip;
    public string city;
    public string region;
    public string country;
    public string loc;
    public string org;
    public string postal;
    public string timezone;
    public string readme;
}
