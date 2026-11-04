using UnityEngine;
using TMPro;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;

public class GlobalClicker : MonoBehaviourPun
{
    [Header("UI")]
    public TextMeshPro textMesh;

    [Header("Effects")]
    public ParticleSystem[] particleEffects;
    public AudioSource audioSource;

    private int touchCount = 0;
    private bool loaded = false;
    private bool cooldown = false;

    private void Start()
    {
        GetGlobalCount();
        StartCoroutine(AutoRefresh());
    }

    // =========================
    // GET GLOBAL COUNT
    // =========================
    void GetGlobalCount()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest
        {
            FunctionName = "GetGlobalClick"
        },
        result =>
        {
            var resultDict = result.FunctionResult as Dictionary<string, object>;

            if (resultDict != null && resultDict.ContainsKey("count"))
            {
                touchCount = int.Parse(resultDict["count"].ToString());
                UpdateCounterText();
                loaded = true;

                Debug.Log("Loaded Global Count: " + touchCount);
            }
            else
            {
                Debug.LogError("Failed to read global count!");
            }
        },
        error =>
        {
            Debug.LogError("Get Error: " + error.GenerateErrorReport());
        });
    }

    // =========================
    // CLICK DETECTION
    // =========================
    private void OnTriggerEnter(Collider other)
    {
        if (!loaded || cooldown) return;

        if (other.CompareTag("HandTag"))
        {
            Debug.Log("Click detected");

            photonView.RPC("ClickRPC", RpcTarget.All);
        }
    }

    // =========================
    // NETWORK SYNC
    // =========================
    [PunRPC]
    void ClickRPC()
    {
        touchCount++;
        UpdateCounterText();

        if (touchCount % 100 == 0)
        {
            PlayEffects();
        }

        // Only master updates server
        if (PhotonNetwork.IsMasterClient)
        {
            SendClickToServer();
        }
    }

    // =========================
    // SEND TO PLAYFAB (GLOBAL)
    // =========================
    void SendClickToServer()
    {
        cooldown = true;

        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest
        {
            FunctionName = "AddGlobalClick"
        },
        result =>
        {
            var resultDict = result.FunctionResult as Dictionary<string, object>;

            if (resultDict != null && resultDict.ContainsKey("count"))
            {
                touchCount = int.Parse(resultDict["count"].ToString());
                UpdateCounterText();

                Debug.Log("Server Updated Count: " + touchCount);
            }
            else
            {
                Debug.LogError("Failed to update count!");
            }

            cooldown = false;
        },
        error =>
        {
            Debug.LogError("Save Error: " + error.GenerateErrorReport());
            cooldown = false;
        });
    }

    // =========================
    // AUTO REFRESH (SYNC ALL PLAYERS)
    // =========================
    IEnumerator AutoRefresh()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            GetGlobalCount();
        }
    }

    // =========================
    // UI UPDATE
    // =========================
    void UpdateCounterText()
    {
        if (textMesh != null)
        {
            textMesh.text = touchCount.ToString();
        }
    }

    // =========================
    // EFFECTS
    // =========================
    void PlayEffects()
    {
        foreach (var effect in particleEffects)
        {
            if (effect != null)
                effect.Play();
        }

        if (audioSource != null)
            audioSource.Play();
    }
}