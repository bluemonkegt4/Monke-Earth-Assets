using UnityEngine;
using System.Collections;

public class SpawnAndFreezeOnStart : MonoBehaviour
{
    [Header("Spawn Settings")]
    public Transform spawnPoint;     // Where player spawns
    public GameObject playerRoot;    // XR Origin / Gorilla Player

    [Header("Freeze Settings")]
    public float freezeDuration = 1f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        // Wait one frame so XR initializes
        yield return null;

        if (spawnPoint == null || playerRoot == null)
        {
            Debug.LogWarning("SpawnAndFreezeOnStart missing references");
            yield break;
        }

        Rigidbody rb = playerRoot.GetComponent<Rigidbody>();

        RigidbodyConstraints originalConstraints = RigidbodyConstraints.None;

        if (rb != null)
        {
            // Save original constraints
            originalConstraints = rb.constraints;

            // Stop motion
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Freeze everything
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        // Teleport player
        playerRoot.transform.position = spawnPoint.position;
        playerRoot.transform.rotation = spawnPoint.rotation;

        // Wait while frozen
        yield return new WaitForSeconds(freezeDuration);

        // Restore movement
        if (rb != null)
        {
            rb.constraints = originalConstraints;
        }
    }
}
