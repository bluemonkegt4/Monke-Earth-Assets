using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class RoomFullText3D : MonoBehaviourPunCallbacks
{
    public TextMeshPro text3D;

    [Header("Message Settings")]
    public string roomFullMessage = "ROOM FULL";
    public float showTime = 2.5f;

    private float timer;

    void Start()
    {
        text3D.gameObject.SetActive(false);
    }

    void Update()
    {
        if (text3D.gameObject.activeSelf)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                text3D.gameObject.SetActive(false);
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        // Photon error code for "Room Full"
        if (returnCode == ErrorCode.GameFull)
        {
            ShowRoomFull();
        }
    }

    void ShowRoomFull()
    {
        text3D.text = roomFullMessage;
        text3D.gameObject.SetActive(true);
        timer = showTime;
    }
}