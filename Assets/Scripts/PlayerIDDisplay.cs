using UnityEngine;
using TMPro;

public class PlayerIDDisplay : MonoBehaviour
{
    [SerializeField] private PersistentPlayerID player;
    [SerializeField] private TMP_Text text;

    private void Start()
    {
        if (player != null && text != null)
        {
            text.text = "Player ID: " + player.PlayerID;
        }
    }
}