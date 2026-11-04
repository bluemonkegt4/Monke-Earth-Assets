using UnityEngine;
using GorillaLocomotion;

public class SpeedBoost : MonoBehaviour
{
    [Header("Speed Boost")]
    public float speedBoost = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("HandTag"))
            return;

        if (Player.Instance == null)
            return;

        Rigidbody rb = Player.Instance.GetComponent<Rigidbody>();

        if (rb == null)
            return;

        Vector3 velocity = rb.velocity;

        if (velocity.magnitude > 0.01f)
        {
            rb.velocity = velocity.normalized * (velocity.magnitude + speedBoost);
        }
    }
}