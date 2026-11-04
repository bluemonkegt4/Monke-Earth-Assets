using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class LockPositionGrab : MonoBehaviour
{
    private XRGrabInteractable grab;
    private Vector3 startPos;
    private Quaternion startRot;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        startPos = transform.position;
        startRot = transform.rotation;
    }

    void OnEnable()
    {
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        grab.selectEntered.RemoveListener(OnGrab);
        grab.selectExited.RemoveListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        // Freeze movement
        transform.position = startPos;
        transform.rotation = startRot;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        // Snap back just in case
        transform.position = startPos;
        transform.rotation = startRot;
    }

    void LateUpdate()
    {
        // Enforce lock every frame while grabbed
        if (grab.isSelected)
        {
            transform.position = startPos;
            transform.rotation = startRot;
        }
    }
}
