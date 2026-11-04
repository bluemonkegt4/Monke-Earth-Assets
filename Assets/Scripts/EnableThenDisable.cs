using UnityEngine;
using System.Collections;

public class EnableThenDisable : MonoBehaviour
{
    [Header("Target")]
    public GameObject targetObject;

    [Header("Timer (seconds)")]
    public float disableAfterSeconds = 5f;

    public void EnableObject()
    {
        if (targetObject == null)
            return;

        StopAllCoroutines();
        targetObject.SetActive(true);
        StartCoroutine(DisableTimer());
    }

    private IEnumerator DisableTimer()
    {
        yield return new WaitForSeconds(disableAfterSeconds);
        targetObject.SetActive(false);
    }
}
