using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class RemoteAccessAdminEditor : EditorWindow
{
    private string playerID = "";
    private string adminURL = "";

    [MenuItem("Admin/Remote Access Control")]
    public static void Open()
    {
        GetWindow<RemoteAccessAdminEditor>("Access Control");
    }

    private void OnGUI()
    {
        GUILayout.Label("Player ID", EditorStyles.boldLabel);
        playerID = EditorGUILayout.TextField(playerID);

        GUILayout.Space(10);
        adminURL = EditorGUILayout.TextField("Admin URL", adminURL);

        GUILayout.Space(10);

        if (GUILayout.Button("Give Access"))
        {
            Send("give");
        }

        if (GUILayout.Button("Take Away"))
        {
            Send("remove");
        }
    }

    private void Send(string action)
    {
        string url = adminURL + "?action=" + action + "&playerID=" + playerID;
        EditorCoroutineUtility.StartCoroutineOwnerless(SendRequest(url));
    }

    private IEnumerator SendRequest(string url)
    {
        using (UnityWebRequest req = UnityWebRequest.Get(url))
        {
            yield return req.SendWebRequest();
        }
    }
}