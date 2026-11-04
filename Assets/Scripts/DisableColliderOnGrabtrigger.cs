using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DisableColliderOnGrabtrigger : MonoBehaviour
{
    private XRGrabInteractable grab;
    private Collider[] colliders;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        colliders = GetComponentsInChildren<Collider>();

        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        foreach (Collider col in colliders)
        {
            if (!col.isTrigger)
                col.enabled = false;
        }
    }

    void OnRelease(SelectExitEventArgs args)
    {
        foreach (Collider col in colliders)
        {
            if (!col.isTrigger)
                col.enabled = true;
        }
    }
}
