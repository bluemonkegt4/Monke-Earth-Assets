using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SendTextToGoogleSheets : MonoBehaviour
{
    [Header("Google Web App")]
    public string webAppURL;

    [Header("Vote Data")]
    [TextArea]
    public string textToSend;

    public string senderEmail;

    private void OnEnable()
    {
        StartCoroutine(SendData());
    }

    IEnumerator SendData()
    {
        string json = JsonUtility.ToJson(new Payload
        {
            text = textToSend,
            email = senderEmail
        });

        byte[] body = System.Text.Encoding.UTF8.GetBytes(json);

        UnityWebRequest request = new UnityWebRequest(webAppURL, "POST");
        request.uploadHandler = new UploadHandlerRaw(body);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
            Debug.LogError("Vote failed: " + request.error);
        else
            Debug.Log("Vote sent!");

        gameObject.SetActive(false); // auto-disable like GT voting
    }

    [System.Serializable]
    class Payload
    {
        public string text;
        public string email;
    }
}
