using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DeathSound : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public AudioClip deathLoopSound;

    private AudioSource audioSource;
    private bool wasDead;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        if (playerHealth == null)
            return;

        if (playerHealth.IsDead && !wasDead)
        {
            audioSource.clip = deathLoopSound;
            audioSource.Play();
            wasDead = true;
        }

        if (!playerHealth.IsDead && wasDead)
        {
            audioSource.Stop();
            wasDead = false;
        }
    }
}
