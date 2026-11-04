using UnityEngine;
using UnityEngine.UI;

public class LowHealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image warningImage;

    [Header("Settings")]
    [Range(0f, 1f)]
    public float showAtHealthPercent = 0.3f;

    private void Update()
    {
        if (playerHealth == null || warningImage == null)
            return;

        warningImage.enabled =
            !playerHealth.IsDead &&
            playerHealth.HealthPercent() <= showAtHealthPercent;
    }
}
