using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    public int damage = 1;
    public string targetTag = "Player";

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(targetTag))
            return;

        PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeHit(damage);
        }
    }
}
