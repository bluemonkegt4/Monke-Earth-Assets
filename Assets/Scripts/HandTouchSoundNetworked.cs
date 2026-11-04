using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class HandTouchSoundNetworked : MonoBehaviour
{
    [System.Serializable]
    public class TagSound
    {
        public string tag;
        public AudioClip sound;
        [Range(0f, 1f)] public float volume = 1f;
    }

    [Header("Sound Settings")]
    public List<TagSound> tagSounds = new List<TagSound>();

    [Header("Audio Source")]
    public AudioSource audioSource;

    [Header("Network")]
    private PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        TryPlaySound(collision.gameObject.tag);
    }

    private void OnTriggerEnter(Collider other)
    {
        TryPlaySound(other.gameObject.tag);
    }

    private void TryPlaySound(string tag)
    {
        foreach (var ts in tagSounds)
        {
            if (ts.tag == tag)
            {
                photonView.RPC(nameof(PlaySoundRPC), RpcTarget.All, tag);
                return;
            }
        }
    }

    [PunRPC]
    private void PlaySoundRPC(string tag)
    {
        foreach (var ts in tagSounds)
        {
            if (ts.tag == tag && ts.sound != null)
            {
                audioSource.PlayOneShot(ts.sound, ts.volume);
                return;
            }
        }
    }
}