using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseDatabaseRealtimeView : View
{
    [SerializeField] private Transform contentUsers;
    [SerializeField] private UserGrid userGridPrefab;

    public void DisplayUsersRecords(Dictionary<string, int> users)
    {
        for (int i = 0; i < contentUsers.childCount; i++)
        {
            Destroy(contentUsers.GetChild(i).gameObject);
        }

        users = users.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

        foreach (var item in users)
        {
            UserGrid grid = Instantiate(userGridPrefab, contentUsers);
            grid.SetData(item.Key, item.Value.ToString());
        }
    }
}
