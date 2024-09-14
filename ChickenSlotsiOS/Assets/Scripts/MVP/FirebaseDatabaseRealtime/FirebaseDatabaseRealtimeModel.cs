using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseDatabaseRealtimeModel
{
    public event Action<Dictionary<string, int>> OnGetUsersRecords;

    public string Nickname { get; private set; }
    public int Record { get; private set; }

    private Dictionary<string, int> userRecordsDictionary = new Dictionary<string, int>();


    private FirebaseAuth auth;
    private DatabaseReference databaseReference;

    public FirebaseDatabaseRealtimeModel(FirebaseAuth auth, DatabaseReference database)
    {
        this.auth = auth;
        this.databaseReference = database;
    }

    public void Initialize()
    {
        Record = PlayerPrefs.GetInt(PlayerPrefsKeys.GAME_RECORD, 0);

        if (auth.CurrentUser != null)
        {
            SaveLocalDataToServer();
            DisplayUsersRecords();
        }
    }

    public void DisplayUsersRecords()
    {
        Coroutines.Start(GetUsersRecords());
    }

    public void CreateEmptyDataToServer()
    {
        Nickname = auth.CurrentUser.Email.Split('@')[0];
        PlayerPrefs.SetInt(PlayerPrefsKeys.GAME_RECORD, 0);
        UserData user = new(Nickname, 0);
        string json = JsonUtility.ToJson(user);
        databaseReference.Child("Users").Child(auth.CurrentUser.UserId).SetRawJsonValueAsync(json);
    }


    public void SaveLocalDataToServer()
    {
        Nickname = auth.CurrentUser.Email.Split('@')[0];
        UserData user = new(Nickname, Record);
        string json = JsonUtility.ToJson(user);
        databaseReference.Child("Users").Child(auth.CurrentUser.UserId).SetRawJsonValueAsync(json);
    }

    private IEnumerator GetUsersRecords()
    {
        var task = databaseReference.Child("Users").OrderByChild("Coins").LimitToFirst(15).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Debug.Log("Error display record");
            yield break;
        }

        userRecordsDictionary.Clear();

        DataSnapshot data = task.Result;

        Debug.Log("Success " + data.ChildrenCount);

        foreach (var user in data.Children)
        {
            string name = user.Child("Nickname").Value.ToString();
            string coins = user.Child("Record").Value.ToString();
            userRecordsDictionary.Add(name, int.Parse(coins));
        }

        OnGetUsersRecords?.Invoke(userRecordsDictionary);
    }
}

public class UserData
{
    public string Nickname;
    public int Record;

    public UserData(string nickname, int record)
    {
        Nickname = nickname;
        Record = record;
    }
}
