using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerCounterAutoSwitch3D : MonoBehaviourPunCallbacks
{
    public TextMeshPro text3D;

    void Start()
    {
        UpdateText();
    }

    void UpdateText()
    {
        if (!PhotonNetwork.IsConnected)
        {
            text3D.text = "Offline";
            return;
        }

        // NOT in a room → show global players
        if (!PhotonNetwork.InRoom)
        {
            text3D.text = "Online: " + PhotonNetwork.CountOfPlayers;
            return;
        }

        // IN a room → show room players (includes yourself)
        int count = PhotonNetwork.CurrentRoom.PlayerCount;
        int max = PhotonNetwork.CurrentRoom.MaxPlayers;

        text3D.text = $"Room: {count}/{max}";
    }

    public override void OnConnectedToMaster()
    {
        UpdateText();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdateText();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateText();
    }

    public override void OnJoinedRoom()
    {
        UpdateText();
    }

    public override void OnLeftRoom()
    {
        UpdateText();
    }
}