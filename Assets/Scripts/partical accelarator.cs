using UnityEngine;

public class ParticleAcceleratorBall : MonoBehaviour
{
    [Header("Speed Settings")]
    [Tooltip("How much speed the ball multiplies by on each bounce (e.g., 1.1 = 10% faster)")]
    public float speedMultiplier = 1.15f;
    
    [Tooltip("The maximum speed the ball can reach so it doesn't break the game")]
    public float maxSpeed = 100f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Give it an initial push if it's not moving
        rb.velocity = Random.insideUnitSphere.normalized * 5f; 
    }

    void OnCollisionEnter(Collision collision)
    {
        // 1. Get the current speed after the initial physics bounce calculation
        float currentSpeed = rb.velocity.magnitude;

        // 2. Multiply the speed, while capping it at your maximum threshold
        float newSpeed = Mathf.Min(currentSpeed * speedMultiplier, maxSpeed);

        // 3. Re-apply the new speed in the exact direction the ball is already bouncing
        rb.velocity = rb.velocity.normalized * newSpeed;
    }
}
