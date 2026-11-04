using UnityEngine;

public class SavedObjectState : MonoBehaviour
{
    public GameObject targetObject;
    public string saveID = "Object1";

    private string SaveKey => "SavedState_" + saveID;

    private void Start()
    {
        bool state = PlayerPrefs.GetInt(
            SaveKey,
            targetObject.activeSelf ? 1 : 0
        ) == 1;

        targetObject.SetActive(state);
    }

    private void Update()
    {
        if (targetObject == null)
            return;

        PlayerPrefs.SetInt(
            SaveKey,
            targetObject.activeSelf ? 1 : 0
        );
    }
}