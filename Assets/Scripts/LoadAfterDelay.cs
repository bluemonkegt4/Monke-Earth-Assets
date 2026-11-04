using UnityEngine;
using System.Collections;

public class LoadAfterDelay : MonoBehaviour
{
    [Header("Target Object")]
    [SerializeField] private GameObject targetObject;

    [Header("Delay Settings (seconds)")]
    [Tooltip("Time to wait before showing the object")]
    [SerializeField] private float delayTime = 3f;

    private void Start()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false); // Make sure object starts hidden
            StartCoroutine(ShowAfterDelay());
        }
    }

    private IEnumerator ShowAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        targetObject.SetActive(true);
    }
}
