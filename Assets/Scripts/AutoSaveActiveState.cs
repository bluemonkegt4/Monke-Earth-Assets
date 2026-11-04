using UnityEngine;

public class AutoSaveActiveState : MonoBehaviour
{
    public string saveID = "Object1";

    private bool lastState;

    private string SaveKey => "ActiveState_" + saveID;

    private void Awake()
    {
        bool savedState = PlayerPrefs.GetInt(SaveKey, gameObject.activeSelf ? 1 : 0) == 1;
        gameObject.SetActive(savedState);

        lastState = gameObject.activeSelf;
    }

    private void Update()
    {
        if (gameObject.activeSelf != lastState)
        {
            lastState = gameObject.activeSelf;

            PlayerPrefs.SetInt(SaveKey, lastState ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}