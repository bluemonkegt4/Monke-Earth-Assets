using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class HandTouchSound : MonoBehaviour
{
    [System.Serializable]
    public class TagSound
    {
        public string tag;
        public AudioClip sound;
    }

    [Header("Tag → Sound Mapping")]
    public List<TagSound> tagSounds = new List<TagSound>();

    [Header("Settings")]
    public float volume = 1f;
    public float cooldown = 0.15f;

    private AudioSource audioSource;
    private float nextPlayTime;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time < nextPlayTime)
            return;

        foreach (TagSound ts in tagSounds)
        {
            if (other.CompareTag(ts.tag) && ts.sound != null)
            {
                audioSource.PlayOneShot(ts.sound, volume);
                nextPlayTime = Time.time + cooldown;
                break;
            }
        }
    }
}
