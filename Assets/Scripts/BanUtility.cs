using UnityEngine;
using System;

public static class BanUtility
{
    private const string BanPrefix = "PLAYER_BAN_";
    private const string BanEndPrefix = "PLAYER_BAN_END_";

    private static string PlayerID
    {
        get
        {
            if (PersistentPlayerID.Instance == null)
                return null;

            return PersistentPlayerID.Instance.PlayerID;
        }
    }

    private static string BanKey => BanPrefix + PlayerID;
    private static string BanEndKey => BanEndPrefix + PlayerID;

    /// <summary>
    /// Bans THIS player (by custom Player ID) for X hours.
    /// </summary>
    public static void BanLocalPlayer(float hours)
    {
        if (string.IsNullOrEmpty(PlayerID))
            return;

        PlayerPrefs.SetInt(BanKey, 1);

        double endTime =
            Time.realtimeSinceStartupAsDouble + (hours * 3600.0);

        PlayerPrefs.SetString(BanEndKey, endTime.ToString());
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Unbans THIS player immediately.
    /// </summary>
    public static void UnbanLocalPlayer()
    {
        if (string.IsNullOrEmpty(PlayerID))
            return;

        PlayerPrefs.DeleteKey(BanKey);
        PlayerPrefs.DeleteKey(BanEndKey);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Checks if THIS player (custom ID) is banned.
    /// </summary>
    public static bool IsLocalPlayerBanned()
    {
        if (string.IsNullOrEmpty(PlayerID))
            return false;

        if (!PlayerPrefs.HasKey(BanKey))
            return false;

        if (!PlayerPrefs.HasKey(BanEndKey))
            return false;

        double endTime =
            double.Parse(PlayerPrefs.GetString(BanEndKey));

        if (Time.realtimeSinceStartupAsDouble >= endTime)
        {
            // Ban expired
            UnbanLocalPlayer();
            return false;
        }

        return true;
    }

    /// <summary>
    /// Remaining ban time in seconds.
    /// </summary>
    public static double GetRemainingBanTime()
    {
        if (!IsLocalPlayerBanned())
            return 0;

        double endTime =
            double.Parse(PlayerPrefs.GetString(BanEndKey));

        return Math.Max(
            0,
            endTime - Time.realtimeSinceStartupAsDouble
        );
    }
}