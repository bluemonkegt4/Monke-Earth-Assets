using UnityEngine;
using System;

public class BanPlayerManager : MonoBehaviour
{
    [Header("Ban Settings")]
    [Tooltip("The ID of the player to ban")]
    public string playerID;

    [Tooltip("Ban duration in seconds")]
    public float banDurationSeconds = 3600f;

    private const string BAN_PREFIX = "BANNED_";

    // ================= BAN BUTTON =================
    [ContextMenu("Ban Player")]
    public void BanPlayer()
    {
        if (string.IsNullOrEmpty(playerID))
        {
            Debug.LogWarning("Ban failed: Player ID is empty.");
            return;
        }

        long banEndTicks = DateTime.UtcNow
            .AddSeconds(banDurationSeconds)
            .Ticks;

        PlayerPrefs.SetString(BAN_PREFIX + playerID, banEndTicks.ToString());
        PlayerPrefs.Save();

        Debug.Log(
            $"Player '{playerID}' banned for {banDurationSeconds} seconds."
        );
    }

    // ================= BAN CHECK =================
    public static bool IsPlayerBanned(string id)
    {
        string key = BAN_PREFIX + id;

        if (!PlayerPrefs.HasKey(key))
            return false;

        if (!long.TryParse(PlayerPrefs.GetString(key), out long ticks))
        {
            PlayerPrefs.DeleteKey(key);
            return false;
        }

        DateTime banEndTime = new DateTime(ticks, DateTimeKind.Utc);

        if (DateTime.UtcNow >= banEndTime)
        {
            PlayerPrefs.DeleteKey(key); // ban expired
            return false;
        }

        return true;
    }
}