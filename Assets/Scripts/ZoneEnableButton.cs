using UnityEngine;
using Photon.Pun;

public class ZoneEnableButton : MonoBehaviourPun
{
    [Header("Target")]
    public GameObject targetObject;

    [Header("Zone (assign your BoxCollider)")]
    public BoxCollider zoneCollider;

    [Header("Settings")]
    public string handTag = "HandTag";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(handTag))
        {
            photonView.RPC("ActivateZone", RpcTarget.All);
        }
    }

    [PunRPC]
    void ActivateZone()
    {
        GameObject localPlayer = PhotonNetwork.LocalPlayer.TagObject as GameObject;

        if (localPlayer == null || zoneCollider == null)
        {
            Debug.LogWarning("Missing player or zone!");
            return;
        }

        // 🔥 Check if player is inside THIS specific box collider
        if (IsInsideBox(zoneCollider, localPlayer.transform.position))
        {
            if (targetObject != null)
            {
                targetObject.SetActive(true);
            }
        }
    }

    // =========================
    // BOX CHECK
    // =========================
    bool IsInsideBox(BoxCollider box, Vector3 point)
    {
        Vector3 localPoint = box.transform.InverseTransformPoint(point) - box.center;

        Vector3 halfSize = box.size * 0.5f;

        return Mathf.Abs(localPoint.x) <= halfSize.x &&
               Mathf.Abs(localPoint.y) <= halfSize.y &&
               Mathf.Abs(localPoint.z) <= halfSize.z;
    }
}