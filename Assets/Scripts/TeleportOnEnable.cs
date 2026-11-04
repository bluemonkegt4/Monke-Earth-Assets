using UnityEngine;
using System.Collections;

public class TeleportOnEnable : MonoBehaviour
{
    [Header("Teleport Settings")]
    public Transform teleportTarget;   // Where the player will be teleported
    public GameObject playerRoot;       // Player rig / gorilla player root

    [Header("Auto Disable")]
    public float disableAfterSeconds = 1f;

    private void OnEnable()
    {
        if (teleportTarget == null || playerRoot == null)
        {
            Debug.LogWarning("TeleportOnEnable: Missing references");
            return;
        }

        Teleport();
        StartCoroutine(DisableAfterDelay());
    }

    void Teleport()
    {
        // Move player instantly
        playerRoot.transform.position = teleportTarget.position;
        playerRoot.transform.rotation = teleportTarget.rotation;
    }

    IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(disableAfterSeconds);
        gameObject.SetActive(false);
    }
}
