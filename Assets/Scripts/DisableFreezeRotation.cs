using UnityEngine;

public class DisableFreezeRotation : MonoBehaviour
{
    public Rigidbody targetRigidbody;

    public void DisableRotationConstraints()
    {
        if (targetRigidbody == null)
        {
            Debug.LogWarning("No Rigidbody assigned!");
            return;
        }

        targetRigidbody.constraints &= ~RigidbodyConstraints.FreezeRotation;
    }
}