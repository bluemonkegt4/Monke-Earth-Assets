using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AdminPermissions))]
public class AdminPermissionEditor : Editor
{
    string targetID;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AdminPermissions perms = (AdminPermissions)target;

        GUILayout.Space(10);
        targetID = EditorGUILayout.TextField("Target Player ID", targetID);

        if (GUILayout.Button("Give Mods (10 Minutes)"))
        {
            perms.GiveTempAdmin(targetID, 600f);
        }

        if (GUILayout.Button("Remove Mods"))
        {
            perms.RemoveAdmin(targetID);
        }
    }
}
