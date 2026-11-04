using UnityEngine;
using System.Collections;

public class FreezeRigidbody : MonoBehaviour
{
    [Header("Freeze Settings")]
    public Rigidbody targetRigidbody;
    public float freezeDuration = 1f;

    private RigidbodyConstraints originalConstraints;
    private bool isFreezing;

    private void OnEnable()
    {
        if (targetRigidbody == null)
        {
            Debug.LogWarning("FreezeRigidbody: No Rigidbody assigned");
            return;
        }

        if (!isFreezing)
            StartCoroutine(FreezeRoutine());
    }

    IEnumerator FreezeRoutine()
    {
        isFreezing = true;

        // Save constraints
        originalConstraints = targetRigidbody.constraints;

        // Stop motion
        targetRigidbody.velocity = Vector3.zero;
        targetRigidbody.angularVelocity = Vector3.zero;

        // Freeze everything
        targetRigidbody.constraints = RigidbodyConstraints.FreezeAll;

        yield return new WaitForSeconds(freezeDuration);

        // Restore constraints
        targetRigidbody.constraints = originalConstraints;

        isFreezing = false;

        // Optional: disable after use
        gameObject.SetActive(false);
    }
}
