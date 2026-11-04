using UnityEngine;

public class BanTeleportEnforcer : MonoBehaviour
{
    [Header("Ban Teleport")]
    [SerializeField] private Transform banTeleportPoint;
    [SerializeField] private float positionLockInterval = 0.1f;

    private float timer;

    private void Update()
    {
        if (!BanUtility.IsLocalPlayerBanned())
            return;

        if (banTeleportPoint == null)
            return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            ForceTeleport();
            timer = positionLockInterval;
        }
    }

    private void ForceTeleport()
    {
        transform.position = banTeleportPoint.position;
        transform.rotation = banTeleportPoint.rotation;
    }
}