using UnityEngine;
using GorillaLocomotion;

public class GorillaSwimming : MonoBehaviour
{
    [Header("Swimming")]
    public bool inWater;

    public float buoyancy = 8f;
    public float swimStrength = 2f;
    public float waterDrag = 0.96f;
    public float sinkSpeed = 2f;

    private Rigidbody rb;

    private Vector3 lastLeftPos;
    private Vector3 lastRightPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        lastLeftPos = Player.Instance.leftHandTransform.position;
        lastRightPos = Player.Instance.rightHandTransform.position;
    }

    private void FixedUpdate()
    {
        if (!inWater)
            return;

        Vector3 leftVel =
            (Player.Instance.leftHandTransform.position - lastLeftPos)
            / Time.fixedDeltaTime;

        Vector3 rightVel =
            (Player.Instance.rightHandTransform.position - lastRightPos)
            / Time.fixedDeltaTime;

        lastLeftPos = Player.Instance.leftHandTransform.position;
        lastRightPos = Player.Instance.rightHandTransform.position;

        Vector3 swimForce =
            (-leftVel - rightVel) * swimStrength;

        rb.AddForce(swimForce, ForceMode.Acceleration);

        rb.AddForce(Vector3.up * buoyancy, ForceMode.Acceleration);

        rb.velocity *= waterDrag;

        if (swimForce.magnitude < 0.5f)
        {
            rb.AddForce(Vector3.down * sinkSpeed,
                ForceMode.Acceleration);
        }
    }
}