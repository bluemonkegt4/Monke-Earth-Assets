using UnityEngine;
using System.Collections.Generic;

public class PlayerZoneDetector : MonoBehaviour
{
    public static PlayerZoneDetector Instance;

    private HashSet<GameObject> playersInZone = new HashSet<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playersInZone.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playersInZone.Remove(other.gameObject);
        }
    }

    public bool IsPlayerInZone(GameObject player)
    {
        return playersInZone.Contains(player);
    }
}