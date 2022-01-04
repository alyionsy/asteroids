using System.Collections;
using System.Collections.Generic;
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
