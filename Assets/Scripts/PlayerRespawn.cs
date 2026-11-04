using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Teleport Points")]
    public Transform deathLocation;
    public Transform respawnLocation;

    [Header("Respawn Settings")]
    public float respawnDelay = 5f;

    private PlayerHealth health;

    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
    }

    private void OnPlayerDeath()
    {
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        if (deathLocation != null)
            transform.position = deathLocation.position;

        yield return new WaitForSeconds(respawnDelay);

        if (respawnLocation != null)
            transform.position = respawnLocation.position;

        health.ResetHealth();
    }
}
