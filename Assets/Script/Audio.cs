using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip winAudio;
    public AudioClip loseAudio;
    public AudioClip starAudio;
    public AudioClip musicAudio;

    public void Init()
    {
        PlayMusicAudio();
    }
    public void StopMusicAudio()
    {
        audioSource.Stop();
    }
    public void PlayMusicAudio()
    {
        audioSource.clip = musicAudio;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void PlayWinAudio()
    {
        audioSource.PlayOneShot(winAudio);
    }
    public void PlayLoseAudio()
    {
        audioSource.PlayOneShot(loseAudio);
    }
    public void PlayStarAudio()
    {
        audioSource.PlayOneShot(starAudio);
    }
}
