using UnityEngine;

public class PlayerIDRegenerator : MonoBehaviour
{
    private const string PlayerIDKey = "PLAYER_ID";

    [Header("Player ID")]
    [SerializeField] private string currentPlayerID;

    private void Awake()
    {
        LoadOrCreateID();
    }

    private void LoadOrCreateID()
    {
        if (PlayerPrefs.HasKey(PlayerIDKey))
        {
            currentPlayerID = PlayerPrefs.GetString(PlayerIDKey);
        }
        else
        {
            GenerateNewID();
        }
    }

    [ContextMenu("Regenerate Player ID")]
    public void GenerateNewID()
    {
        currentPlayerID = System.Guid.NewGuid().ToString();
        PlayerPrefs.SetString(PlayerIDKey, currentPlayerID);
        PlayerPrefs.Save();

        Debug.Log("New Player ID generated: " + currentPlayerID);
    }

    public string GetPlayerID()
    {
        return currentPlayerID;
    }
}