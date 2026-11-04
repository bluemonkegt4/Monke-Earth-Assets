using UnityEngine;
using Photon.VR;

public class ColorScript : MonoBehaviour
{
    [Header("Color Values (0–10)")]
    [Range(0f, 10f)] public float Red = 10f;
    [Range(0f, 10f)] public float Green = 10f;
    [Range(0f, 10f)] public float Blue = 10f;

    [Header("Offline Rig")]
    [SerializeField] private SkinnedMeshRenderer offlineRigRenderer;

    private const string RedKey = "PlayerColor_R";
    private const string GreenKey = "PlayerColor_G";
    private const string BlueKey = "PlayerColor_B";

    private void Start()
    {
        LoadColorValues();
        ApplyColor();
    }

    private void Update()
    {
        ApplyColor();
        SaveColorValues();
    }

    // =========================
    // APPLY COLOR
    // =========================
    private void ApplyColor()
    {
        Color color = new Color(
            Red / 10f,
            Green / 10f,
            Blue / 10f
        );

        // Online (Photon)
        PhotonVRManager.SetColour(color);

        // Offline rig
        if (offlineRigRenderer != null)
        {
            offlineRigRenderer.material.color = color;
        }
    }

    // =========================
    // SAVE
    // =========================
    private void SaveColorValues()
    {
        PlayerPrefs.SetFloat(RedKey, Red);
        PlayerPrefs.SetFloat(GreenKey, Green);
        PlayerPrefs.SetFloat(BlueKey, Blue);
        PlayerPrefs.Save();
    }

    // =========================
    // LOAD
    // =========================
    private void LoadColorValues()
    {
        if (PlayerPrefs.HasKey(RedKey))
            Red = PlayerPrefs.GetFloat(RedKey);

        if (PlayerPrefs.HasKey(GreenKey))
            Green = PlayerPrefs.GetFloat(GreenKey);

        if (PlayerPrefs.HasKey(BlueKey))
            Blue = PlayerPrefs.GetFloat(BlueKey);
    }
}