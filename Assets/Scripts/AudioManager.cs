using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    public AudioSource audioSource;

    public void Play(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }
}