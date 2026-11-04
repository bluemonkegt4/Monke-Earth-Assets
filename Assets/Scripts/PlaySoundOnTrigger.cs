using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnTrigger : MonoBehaviour
{
    public AudioClip sound;
    public float volume = 1f;
    public string requiredTag = "Player";

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(requiredTag))
            return;

        if (sound == null)
            return;

        audioSource.PlayOneShot(sound, volume);
    }
}
