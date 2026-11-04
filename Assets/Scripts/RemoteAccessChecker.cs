using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class RemoteAccessChecker : MonoBehaviour
{
    [Header("Access")]
    [SerializeField] private GameObject objectToEnable;

    // Google Sheet Web App URL
    [SerializeField] private string accessCheckURL;

    private void Start()
    {
        StartCoroutine(CheckAccess());
    }

    private IEnumerator CheckAccess()
    {
        if (PersistentPlayerID.Instance == null)
            yield break;

        string playerID = PersistentPlayerID.Instance.PlayerID;
        string url = accessCheckURL + "?playerID=" + playerID;

        using (UnityWebRequest req = UnityWebRequest.Get(url))
        {
            yield return req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.Success)
                yield break;

            if (req.downloadHandler.text == "ALLOW")
            {
                objectToEnable.SetActive(true);
            }
            else
            {
                objectToEnable.SetActive(false);
            }
        }
    }
}