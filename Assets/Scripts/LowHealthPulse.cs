using UnityEngine;

public class LowHealthPulse : MonoBehaviour
{
    [Header("References")]
    public PlayerHealth playerHealth;
    public Renderer effectRenderer;

    [Header("Health Trigger")]
    [Range(1, 100)]
    public int showAtHealthPercent = 30;

    [Header("Fade Settings")]
    public float fadeSpeed = 3f;
    public float maxAlpha = 0.6f;

    [Header("Pulse Settings")]
    public float pulseSpeed = 2f;
    public float pulseStrength = 0.15f;

    private Material mat;
    private float currentAlpha;

    private void Start()
    {
        if (effectRenderer != null)
        {
            mat = effectRenderer.material;
            SetAlpha(0f);
        }
    }

    private void Update()
    {
        if (playerHealth == null || mat == null)
            return;

        int healthPercent =
            Mathf.RoundToInt(playerHealth.HealthPercent() * 100f);

        bool shouldShow =
            !playerHealth.IsDead &&
            healthPercent <= showAtHealthPercent;

        float targetAlpha = shouldShow ? maxAlpha : 0f;

        // Smooth fade
        currentAlpha = Mathf.MoveTowards(
            currentAlpha,
            targetAlpha,
            fadeSpeed * Time.deltaTime
        );

        // Pulse effect
        if (shouldShow)
        {
            float pulse =
                Mathf.Sin(Time.time * pulseSpeed) * pulseStrength;
            SetAlpha(Mathf.Clamp01(currentAlpha + pulse));
        }
        else
        {
            SetAlpha(currentAlpha);
        }
    }

    private void SetAlpha(float a)
    {
        Color c = mat.color;
        c.a = a;
        mat.color = c;
    }
}
