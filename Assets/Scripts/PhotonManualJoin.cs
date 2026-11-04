using UnityEngine;
using Photon.Pun;

public class PhotonManualJoin : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = false;

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server (not in a room)");
        PhotonNetwork.JoinLobby(); // optional, safe
    }

    // Call this from a button / trigger / computer
    public void JoinPublicRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void JoinPrivateRoom(string roomCode)
    {
        PhotonNetwork.JoinOrCreateRoom(
            roomCode,
            new Photon.Realtime.RoomOptions { MaxPlayers = 10 },
            Photon.Realtime.TypedLobby.Default
        );
    }
}