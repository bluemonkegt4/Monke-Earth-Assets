using UnityEngine;
using System.Collections;

public class DisableTeleporterGroupOnStart : MonoBehaviour
{
    [Header("Teleporter Group")]
    public GameObject teleporterGroup;   // Parent object holding all teleporters

    [Header("Timing")]
    public float reEnableDelay = 2f;

    private void Start()
    {
        StartCoroutine(ControlTeleporters());
    }

    IEnumerator ControlTeleporters()
    {
        if (teleporterGroup == null)
        {
            Debug.LogWarning("DisableTeleporterGroupOnStart: Teleporter Group not assigned");
            yield break;
        }

        // Disable all teleporters at once
        teleporterGroup.SetActive(false);

        // Wait for player to stabilize
        yield return new WaitForSeconds(reEnableDelay);

        // Re-enable teleporters
        teleporterGroup.SetActive(true);
    }
}
