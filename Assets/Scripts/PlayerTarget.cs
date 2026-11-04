using UnityEngine;

public class PlayerTarget : MonoBehaviour
{
    public string playerID;

    public void OnAdminSelected()
    {
        Debug.Log("Admin selected player: " + playerID);
        // Hook this to UI buttons (Kick / Give Mods)
    }
}
