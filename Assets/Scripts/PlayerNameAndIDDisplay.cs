using UnityEngine;
using TMPro;

public class PlayerIDNameAndDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private string playerName = "Player";

    private const string PlayerIDKey = "PLAYER_ID";

    private void Start()
    {
        if (!PlayerPrefs.HasKey(PlayerIDKey))
        {
            displayText.text = "ID not generated";
            return;
        }

        string id = PlayerPrefs.GetString(PlayerIDKey);
        displayText.text = playerName + "\nID: " + id;
    }
}