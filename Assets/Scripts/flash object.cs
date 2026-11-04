using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    public GameObject objectToToggle;
    public float seconds = 1f;

    private void Start()
    {
        InvokeRepeating(nameof(Toggle), seconds, seconds);
    }

    private void Toggle()
    {
        if (objectToToggle != null)
            objectToToggle.SetActive(!objectToToggle.activeSelf);
    }
}