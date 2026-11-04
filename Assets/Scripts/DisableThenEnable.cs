using UnityEngine;
using System.Collections;

public class DisableThenEnableTarget : MonoBehaviour
{
    [Header("Target")]
    public GameObject targetObject;

    [Header("Timing")]
    public float disableTime = 2f;

    // Call this from a button / trigger / script
    public void Trigger()
    {
        if (targetObject != null)
            StartCoroutine(DisableRoutine());
    }

    private IEnumerator DisableRoutine()
    {
        targetObject.SetActive(false);
        yield return new WaitForSeconds(disableTime);
        targetObject.SetActive(true);
    }
}