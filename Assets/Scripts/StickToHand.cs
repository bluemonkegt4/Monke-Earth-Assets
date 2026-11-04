using UnityEngine;

public class StickToHand : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string handTag = "HandTag";
    [SerializeField] private bool stickOnTouch = true;
    [SerializeField] private bool allowDetach = false;

    [Header("Offset")]
    public Vector3 positionOffset;
    public Vector3 rotationOffset;

    private Transform currentHand;
    private bool isStuck = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!stickOnTouch || isStuck)
            return;

        if (other.CompareTag(handTag))
        {
            AttachToHand(other.transform);
        }
    }

    private void Update()
    {
        if (isStuck && currentHand != null)
        {
            transform.position = currentHand.position + currentHand.TransformDirection(positionOffset);
            transform.rotation = currentHand.rotation * Quaternion.Euler(rotationOffset);

            if (allowDetach && Input.GetKeyDown(KeyCode.G)) // optional detach key (PC testing)
            {
                Detach();
            }
        }
    }

    // =========================
    // ATTACH
    // =========================
    private void AttachToHand(Transform hand)
    {
        currentHand = hand;
        isStuck = true;

        // Optional: disable physics so it doesn't fight the hand
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
        }
    }

    // =========================
    // DETACH
    // =========================
    public void Detach()
    {
        isStuck = false;
        currentHand = null;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
}