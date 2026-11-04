using UnityEngine;
using Photon.Pun;

public class BanMultiplayerBlocker : MonoBehaviour
{
    [Header("Optional")]
    public GameObject multiplayerUI; // join buttons, code menu, etc.

    void Start()
    {
        if (BanUtility.IsLocalPlayerBanned())
        {
            DisableMultiplayer();
        }
    }

    void DisableMultiplayer()
    {
        Debug.Log("Player is banned — multiplayer disabled.");

        // Prevent any Photon connection
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }

        PhotonNetwork.OfflineMode = true;

        // Optional: disable multiplayer UI
        if (multiplayerUI != null)
            multiplayerUI.SetActive(false);
    }
}
