using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseDatabaseRealtimePresenter
{
    private FirebaseDatabaseRealtimeModel firebaseDatabaseRealtimeModel;
    private FirebaseDatabaseRealtimeView firebaseDatabaseRealtimeView;

    public FirebaseDatabaseRealtimePresenter(FirebaseDatabaseRealtimeModel firebaseDatabaseRealtimeModel, FirebaseDatabaseRealtimeView firebaseDatabaseRealtimeView)
    {
        this.firebaseDatabaseRealtimeModel = firebaseDatabaseRealtimeModel;
        this.firebaseDatabaseRealtimeView = firebaseDatabaseRealtimeView;
    }

    public void Initialize()
    {
        ActivateEvents();

        firebaseDatabaseRealtimeModel.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        firebaseDatabaseRealtimeModel.OnGetUsersRecords += firebaseDatabaseRealtimeView.DisplayUsersRecords;
    }

    private void DeactivateEvents()
    {
        firebaseDatabaseRealtimeModel.OnGetUsersRecords -= firebaseDatabaseRealtimeView.DisplayUsersRecords;
    }

    public void CreateEmptyDataToServer()
    {
        firebaseDatabaseRealtimeModel.CreateEmptyDataToServer();
    }

    public void DisplayUsersRecords()
    {
        firebaseDatabaseRealtimeModel.DisplayUsersRecords();
    }
}
