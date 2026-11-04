using UnityEngine;
using System;

public class PersistentPlayerID : MonoBehaviour
{
    public static PersistentPlayerID Instance { get; private set; }

    private const string PlayerIDKey = "CUSTOM_PLAYER_ID";

    public string PlayerID { get; private set; }

    private void Awake()
    {
        // Singleton (safe)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadOrCreateID();
    }

    private void LoadOrCreateID()
    {
        if (PlayerPrefs.HasKey(PlayerIDKey))
        {
            PlayerID = PlayerPrefs.GetString(PlayerIDKey);
        }
        else
        {
            PlayerID = GenerateID();
            PlayerPrefs.SetString(PlayerIDKey, PlayerID);
            PlayerPrefs.Save();
        }
    }

    private string GenerateID()
    {
        // Short, unique, readable
        return Guid.NewGuid()
            .ToString("N")   // no dashes
            .Substring(0, 10)
            .ToUpper();
    }

    // OPTIONAL: only call this from an admin tool
    public void RegenerateID()
    {
        PlayerID = GenerateID();
        PlayerPrefs.SetString(PlayerIDKey, PlayerID);
        PlayerPrefs.Save();
    }
}