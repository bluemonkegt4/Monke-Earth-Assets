using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;

public class BanCheck : MonoBehaviour
{
    public GameObject bannedScreen;

    public void OnBanConfirmed()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.Disconnect();
        }

        if (bannedScreen != null)
        {
            bannedScreen.SetActive(true);
        }
    }
    public GameObject objectEnable;


    public void OnTriggerEnter()
    {
        objectEnable.SetActive(true);
    }
}