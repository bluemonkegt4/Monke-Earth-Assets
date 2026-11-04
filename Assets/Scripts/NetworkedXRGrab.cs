using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(Rigidbody))]
public class NetworkedXRGrabInteractable : XRGrabInteractable
{
    private PhotonView pv;
    private Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();

        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();

        pv.OwnershipTransfer = OwnershipOption.Takeover;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        // 🔥 TAKE OWNERSHIP BEFORE XR MOVES IT
        if (!pv.IsMine)
        {
            pv.RequestOwnership();
        }

        // 🔥 STOP PHYSICS CONFLICT
        rb.isKinematic = true;
        rb.useGravity = false;

        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // Only owner restores physics
        if (!pv.IsMine) return;

        rb.isKinematic = false;
        rb.useGravity = true;

        // OPTIONAL: throw velocity (prevents drop dead feel)
        if (args.interactorObject is XRBaseInteractor interactor)
        {
            if (interactor.TryGetComponent(out Rigidbody handRB))
            {
                rb.velocity = handRB.velocity;
                rb.angularVelocity = handRB.angularVelocity;
            }
        }
    }
}