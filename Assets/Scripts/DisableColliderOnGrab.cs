using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DisableColliderOnGrab : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Collider objectCollider;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        objectCollider = GetComponent<Collider>();

        if (grabInteractable == null)
            Debug.LogError("DisableColliderOnGrab: XRGrabInteractable missing!");

        if (objectCollider == null)
            Debug.LogError("DisableColliderOnGrab: Collider missing!");
    }

    void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        objectCollider.enabled = false;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        objectCollider.enabled = true;
    }
}
