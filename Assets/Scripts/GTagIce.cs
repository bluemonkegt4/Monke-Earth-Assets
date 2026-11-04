using UnityEngine;

public class GTagIce : MonoBehaviour
{
    [Header("Ice Settings")]
    public float slipMultiplier = 1.2f;   // how fast you keep momentum
    public float controlReduction = 0.5f; // how much control you lose
    public float drag = 0.98f;            // how slowly you stop

    private Vector3 velocity;
    private bool onIce = false;

    private Rigidbody playerRb;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (onIce)
        {
            // keep momentum
            velocity *= drag;

            // apply movement
            playerRb.velocity = new Vector3(
                velocity.x,
                playerRb.velocity.y,
                velocity.z
            );
        }
    }

    // called by your movement system
    public void AddForce(Vector3 inputForce)
    {
        if (onIce)
        {
            velocity += inputForce * slipMultiplier;
        }
        else
        {
            velocity = inputForce;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ice"))
        {
            onIce = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ice"))
        {
            onIce = false;
            velocity = Vector3.zero;
        }
    }
}