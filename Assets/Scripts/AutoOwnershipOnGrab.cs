using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class AutoOwnershipOnGrab : MonoBehaviour
{
    private PhotonView pv;
    private XRGrabInteractable grab;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        grab = GetComponent<XRGrabInteractable>();

        grab.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (!pv.IsMine)
        {
            pv.RequestOwnership();
        }
    }
}