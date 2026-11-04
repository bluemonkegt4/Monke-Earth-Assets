using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class TouchSound : MonoBehaviour
{
    public AudioClip sound;
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
        if (!other.CompareTag("PlayerHand"))
            return;

        if (Time.time < nextPlayTime)
            return;

        if (sound == null)
            return;

        audioSource.PlayOneShot(sound, volume);
        nextPlayTime = Time.time + cooldown;
    }
}
