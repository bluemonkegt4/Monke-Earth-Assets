using UnityEngine;
using TMPro;

public class BanMessageText : MonoBehaviour
{
    [Header("Text Display")]
    [SerializeField] private TMP_Text banText;

    [Header("Ban Message")]
    [TextArea]
    [SerializeField]
    private string banMessage =
        "You are banned.\nPlease wait until your ban expires.";

    private const string BanKey = "PLAYER_BANNED";
    private const string BanEndTimeKey = "BAN_END_TIME";

    private void Start()
    {
        if (banText == null)
            return;

        if (!PlayerPrefs.HasKey(BanKey))
        {
            banText.gameObject.SetActive(false);
            return;
        }

        if (PlayerPrefs.GetInt(BanKey) == 1)
        {
            double banEndTime = double.Parse(PlayerPrefs.GetString(BanEndTimeKey));
            double timeLeft = banEndTime - Time.realtimeSinceStartupAsDouble;

            if (timeLeft <= 0)
            {
                // Ban expired
                PlayerPrefs.DeleteKey(BanKey);
                PlayerPrefs.DeleteKey(BanEndTimeKey);
                banText.gameObject.SetActive(false);
            }
            else
            {
                banText.gameObject.SetActive(true);
                banText.text =
                    banMessage +
                    "\n\nTime left: " +
                    Mathf.CeilToInt((float)(timeLeft / 60f)) +
                    " minutes";
            }
        }
        else
        {
            banText.gameObject.SetActive(false);
        }
    }
}