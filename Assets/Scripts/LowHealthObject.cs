using UnityEngine;

public class LowHealthObject : MonoBehaviour
{
    [Header("References")]
    public PlayerHealth playerHealth;
    public GameObject lowHealthObject;

    [Header("Settings")]
    [Range(0f, 1f)]
    public float showAtHealthPercent = 0.3f;

    private void Start()
    {
        if (lowHealthObject != null)
            lowHealthObject.SetActive(false);
    }

    private void Update()
    {
        if (playerHealth == null || lowHealthObject == null)
            return;

        bool shouldShow =
            !playerHealth.IsDead &&
            playerHealth.HealthPercent() <= showAtHealthPercent;

        if (lowHealthObject.activeSelf != shouldShow)
            lowHealthObject.SetActive(shouldShow);
    }
}
