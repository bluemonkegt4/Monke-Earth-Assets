using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Drinking : MonoBehaviour
{
    [Header("Made by Nico, Don't Skid")]
    public string webhook = "";
    public string AppName = "com.YOURCOMPANY.YOURGAME";

    void Start()
    {
        string CurrentPackageName = Application.identifier;

        if (CurrentPackageName != AppName)
        {
            Debug.Log("Package Name has Changed! Quitting Game.");
            StartCoroutine(SendWebhook(webhook, "Monke Earth detected someone was modding! Their account has been blacklisted untill the mods are removed."));
            Application.Quit();
        }

        CheckIfScriptDeleted();
    }

    void CheckIfScriptDeleted()
    {
        var script = FindObjectOfType<Drinking>();

        if (script == null)
        {
            Debug.Log("Script 'Drinking' got removed, closing game!");
            StartCoroutine(SendWebhook(webhook, "Someone tried deleting the drinking script."));
            Application.Quit();
        }
    }

    IEnumerator SendWebhook(string link, string message)
    {
        WWWForm form = new WWWForm();
        form.AddField("content", message);
        using (UnityWebRequest www = UnityWebRequest.Post(link, form))
        {
            yield return www.SendWebRequest();
        }
    }
}
