using UnityEngine;
using Photon.Pun;
using Photon.VR.Player;

public class PersistentPlayerColor : MonoBehaviour
{
    [Header("Default Color")]
    [SerializeField] private Color defaultColor = Color.white;

    private const string ColorKey = "PLAYER_COLOR";

    private Renderer playerRenderer;

    private void Start()
    {
        FindLocalPlayerRenderer();
        LoadColor();
    }

    // =========================
    // FIND LOCAL PLAYER
    // =========================
    private void FindLocalPlayerRenderer()
    {
        foreach (PhotonVRPlayer vrPlayer in FindObjectsOfType<PhotonVRPlayer>())
        {
            PhotonView pv = vrPlayer.GetComponent<PhotonView>();
            if (pv != null && pv.IsMine)
            {
                playerRenderer = vrPlayer.GetComponentInChildren<Renderer>();
                break;
            }
        }

        if (playerRenderer == null)
            Debug.LogError("[PersistentPlayerColor] Local player renderer not found");
    }

    // =========================
    // PUBLIC SET COLOR
    // =========================
    public void SetColor(Color newColor)
    {
        SaveColor(newColor);
        ApplyColor(newColor);
    }

    // =========================
    // SAVE COLOR
    // =========================
    private void SaveColor(Color color)
    {
        PlayerPrefs.SetFloat(ColorKey + "_R", color.r);
        PlayerPrefs.SetFloat(ColorKey + "_G", color.g);
        PlayerPrefs.SetFloat(ColorKey + "_B", color.b);
        PlayerPrefs.SetFloat(ColorKey + "_A", color.a);
        PlayerPrefs.Save();
    }

    // =========================
    // LOAD COLOR
    // =========================
    private void LoadColor()
    {
        if (!PlayerPrefs.HasKey(ColorKey + "_R"))
        {
            ApplyColor(defaultColor);
            return;
        }

        Color savedColor = new Color(
            PlayerPrefs.GetFloat(ColorKey + "_R"),
            PlayerPrefs.GetFloat(ColorKey + "_G"),
            PlayerPrefs.GetFloat(ColorKey + "_B"),
            PlayerPrefs.GetFloat(ColorKey + "_A")
        );

        ApplyColor(savedColor);
    }

    // =========================
    // APPLY COLOR
    // =========================
    private void ApplyColor(Color color)
    {
        if (playerRenderer != null)
            playerRenderer.material.color = color;
    }
}