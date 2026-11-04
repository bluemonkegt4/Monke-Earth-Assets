using UnityEngine;
using TMPro;

public class PlayfabIDDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private PlayFabLogin playfabLogin;

    [Header("Custom Format")]
    [Tooltip("Use {id} where the PlayFab ID should appear")]
    [SerializeField] private string format = "PlayFab ID: {id}";

    private void Start()
    {
        if (playfabLogin != null)
        {
            // Subscribe to the event, updates TMP text when ID is ready
            playfabLogin.OnIDReady += UpdateText;
        }
        else
        {
            Debug.LogWarning("PlayFabLogin reference not assigned!");
        }
    }

    private void UpdateText(string id)
    {
        if (displayText != null)
        {
            displayText.text = format.Replace("{id}", id);
        }
    }

    private void OnDestroy()
    {
        if (playfabLogin != null)
            playfabLogin.OnIDReady -= UpdateText;
    }
}