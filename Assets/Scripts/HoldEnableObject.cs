using UnityEngine;

public class HoldEnableObject : MonoBehaviour
{
    [Header("Target")]
    public GameObject targetObject;

    [Header("Settings")]
    public string handTag = "HandTag";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(handTag))
        {
            SetState(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(handTag))
        {
            SetState(false);
        }
    }

    void SetState(bool state)
    {
        if (targetObject != null)
        {
            targetObject.SetActive(state);
        }
    }
}