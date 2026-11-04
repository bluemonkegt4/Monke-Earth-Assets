using UnityEngine;

public class BanStateObjectSwitcher : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject banCameraObject;

    private bool lastBanState;

    private void Start()
    {
        ApplyState(BanUtility.IsLocalPlayerBanned());
    }

    private void Update()
    {
        bool isBanned = BanUtility.IsLocalPlayerBanned();

        if (isBanned != lastBanState)
        {
            ApplyState(isBanned);
        }
    }

    private void ApplyState(bool banned)
    {
        if (playerObject != null)
            playerObject.SetActive(!banned);

        if (banCameraObject != null)
            banCameraObject.SetActive(banned);

        lastBanState = banned;
    }
}