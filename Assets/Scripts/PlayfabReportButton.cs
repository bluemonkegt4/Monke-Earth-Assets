using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class PlayfabReportButton : MonoBehaviour
{
    [Header("Trigger")]
    [SerializeField] private string handTag = "HandTag";

    [Header("Input (TMP 3D)")]
    [SerializeField] private TMP_Text playerNameInput; // name to search
    [SerializeField] private TMP_Text reasonText;

    [Header("Leaderboard Data")]
    [SerializeField] private TMP_Text[] leaderboardNames;
    [SerializeField] private TMP_Text[] leaderboardIDs;

    [Header("Google Sheets")]
    [SerializeField] private string webAppURL;

    [Header("Visuals")]
    [SerializeField] private Renderer rend;
    [SerializeField] private Material pressedMaterial;
    private Material defaultMaterial;

    private bool pressed;

    private void Start()
    {
        if (rend != null)
            defaultMaterial = rend.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(handTag) || pressed)
            return;

        HandleReport();

        if (rend != null && pressedMaterial != null)
            rend.material = pressedMaterial;

        pressed = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(handTag))
            return;

        if (rend != null && defaultMaterial != null)
            rend.material = defaultMaterial;

        pressed = false;
    }

    // =========================
    // FIND PLAYER + SEND
    // =========================
    private void HandleReport()
    {
        string searchName = playerNameInput.text;
        string reason = reasonText != null ? reasonText.text : "No Reason";

        for (int i = 0; i < leaderboardNames.Length; i++)
        {
            if (leaderboardNames[i] == null || leaderboardIDs[i] == null)
                continue;

            if (leaderboardNames[i].text == searchName)
            {
                string foundID = leaderboardIDs[i].text;

                StartCoroutine(SendToGoogleSheets(
                    leaderboardNames[i].text,
                    foundID,
                    reason
                ));

                Debug.Log("[Report] Sent for: " + searchName);
                return;
            }
        }

        Debug.LogWarning("[Report] Player not found: " + searchName);
    }

    // =========================
    // SEND TO GOOGLE SHEETS
    // =========================
    IEnumerator SendToGoogleSheets(string playerName, string playerID, string reason)
    {
        ReportData data = new ReportData
        {
            reportedName = playerName,
            reportedID = playerID,
            reason = reason
        };

        string json = JsonUtility.ToJson(data);
        byte[] body = Encoding.UTF8.GetBytes(json);

        UnityWebRequest request = new UnityWebRequest(webAppURL, "POST");
        request.uploadHandler = new UploadHandlerRaw(body);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("[Report] Failed: " + request.error);
        }
        else
        {
            Debug.Log("[Report] Successfully sent to Google Sheets");
        }
    }

    [System.Serializable]
    public class ReportData
    {
        public string reportedName;
        public string reportedID;
        public string reason;
    }
}