using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
    [Tooltip("How many seconds before this object disables")]
    public float disableAfterSeconds = 5f;

    private void OnEnable()
    {
        Invoke(nameof(DisableObject), disableAfterSeconds);
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }
}