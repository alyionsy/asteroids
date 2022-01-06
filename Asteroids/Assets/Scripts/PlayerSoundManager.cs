using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip fireSound;

    public void PlayFireSound()
    {
        audioSource.PlayOneShot(fireSound);
    }
}
