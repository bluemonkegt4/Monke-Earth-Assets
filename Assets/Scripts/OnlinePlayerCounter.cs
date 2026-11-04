using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class RoomPlayerCounter3D : MonoBehaviourPunCallbacks
{
    public TextMeshPro text3D;

    void Start()
    {
        UpdateText();
    }

    void UpdateText()
    {
        if (!PhotonNetwork.InRoom)
        {
            text3D.text = "Players: 0";
            return;
        }

        int count = PhotonNetwork.CurrentRoom.PlayerCount;
        int max = PhotonNetwork.CurrentRoom.MaxPlayers;

        text3D.text = $"Players: {count}/{max}";
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdateText();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateText();
    }
}