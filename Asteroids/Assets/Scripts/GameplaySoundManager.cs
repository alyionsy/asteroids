using UnityEngine;

public class GameplaySoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hurtSound;
    public AudioClip deathSound;
    public AudioClip asteroidDestroySound;

    public void PlayHurtSound()
    {
        audioSource.PlayOneShot(hurtSound);
    }

    public void PlayDeathSound()
    {
        audioSource.PlayOneShot(deathSound);
    }

    public void PlayAsteroidDestroySound()
    {
        audioSource.PlayOneShot(asteroidDestroySound);
    }

}
