using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnCollision : MonoBehaviour
{
    public AudioClip sound;
    public float volume = 1f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (sound == null)
            return;

        audioSource.PlayOneShot(sound, volume);
    }
}
