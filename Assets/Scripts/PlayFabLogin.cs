using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayFabLogin : MonoBehaviour
{
    // The PlayFab ID, other scripts can read this
    public string MyPlayFabID { get; private set; } = "";

    // Event that triggers when the ID is ready
    public event Action<string> OnIDReady;

    void Start()
    {
        Login();
    }

    void Login()
    {
        string customID = SystemInfo.deviceUniqueIdentifier;

        var request = new LoginWithCustomIDRequest
        {
            CustomId = customID,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {
        MyPlayFabID = result.PlayFabId;
        Debug.Log("Logged in! PlayFab ID: " + MyPlayFabID);

        // Trigger the event for any listeners
        OnIDReady?.Invoke(MyPlayFabID);
    }

    void OnError(PlayFabError error)
    {
        Debug.LogError("PlayFab Login Failed: " + error.GenerateErrorReport());
    }
}