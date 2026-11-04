using UnityEngine;
using System.Collections.Generic;

public class AdminPermissions : MonoBehaviour
{
    public static AdminPermissions Instance;

    [Header("Owner ID")]
    public string ownerPlayerID;

    [Header("Temporary Mod Access")]
    private Dictionary<string, float> tempMods = new Dictionary<string, float>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        List<string> expired = new List<string>();

        foreach (var pair in tempMods)
        {
            if (Time.time > pair.Value)
                expired.Add(pair.Key);
        }

        foreach (var id in expired)
            tempMods.Remove(id);
    }

    public bool HasAdmin(string playerID)
    {
        if (playerID == ownerPlayerID)
            return true;

        return tempMods.ContainsKey(playerID);
    }

    public void GiveTempAdmin(string playerID, float seconds)
    {
        tempMods[playerID] = Time.time + seconds;
    }

    public void RemoveAdmin(string playerID)
    {
        if (tempMods.ContainsKey(playerID))
            tempMods.Remove(playerID);
    }
}
