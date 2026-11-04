using UnityEngine;
using Photon.Pun;

public class NetworkedHoldEnable : MonoBehaviourPun, IPunObservable
{
    [Header("Target")]
    public GameObject targetObject;

    [Header("Settings")]
    public string handTag = "HandTag";

    private bool isHeld = false;

    // =========================
    // DETECTION
    // =========================
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(handTag))
        {
            if (!photonView.IsMine)
                photonView.RequestOwnership();

            isHeld = true;
            UpdateState();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(handTag))
        {
            if (!photonView.IsMine)
                photonView.RequestOwnership();

            isHeld = false;
            UpdateState();
        }
    }

    // =========================
    // APPLY STATE
    // =========================
    void UpdateState()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(isHeld);
        }
    }

    // =========================
    // NETWORK SYNC
    // =========================
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isHeld);
        }
        else
        {
            isHeld = (bool)stream.ReceiveNext();
            UpdateState();
        }
    }
}