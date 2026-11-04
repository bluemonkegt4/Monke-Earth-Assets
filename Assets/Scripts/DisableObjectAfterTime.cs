using UnityEngine;

public class DisableObjectAfterTime : MonoBehaviour
{
    [Header("Target")]
    public GameObject objectToDisable;

    [Header("Timing")]
    public float disableAfterSeconds = 5f;

    private void Start()
    {
        if (objectToDisable != null)
        {
            Invoke(nameof(DisableTarget), disableAfterSeconds);
        }
        else
        {
            Debug.LogWarning("No object assigned to disable.", this);
        }
    }

    private void DisableTarget()
    {
        objectToDisable.SetActive(false);
    }
}
