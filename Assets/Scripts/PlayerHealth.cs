using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHits = 5;   // how many hits until death
    public int currentHits;

    public bool IsDead { get; private set; }

    private void Start()
    {
        currentHits = maxHits;
        IsDead = false;
    }

    public void TakeHit(int damage = 1)
    {
        if (IsDead)
            return;

        currentHits -= damage;

        if (currentHits <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        IsDead = true;
        currentHits = 0;

        // Notify other scripts
        SendMessage("OnPlayerDeath", SendMessageOptions.DontRequireReceiver);
    }

    public float HealthPercent()
    {
        return (float)currentHits / maxHits;
    }

    public void ResetHealth()
    {
        currentHits = maxHits;
        IsDead = false;
    }
}
