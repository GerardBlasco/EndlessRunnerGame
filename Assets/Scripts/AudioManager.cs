using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    private AudioClip previousMusicSource;

    [Header("Audio Clips")]

    [Header("- Player:")]
    public AudioClip pikminRun;

    [Header("- Music:")]
    public AudioClip gameTheme;

    [Header("- Menu:")]
    public AudioClip buttonSelect;

    private static AudioManager audioManager;

    public static AudioManager instance
    {
        get
        {
            return RequestAudioManager();
        }
    }

    private static AudioManager RequestAudioManager()
    {
        if (!audioManager)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
        return audioManager;
    }

    // AudioManager.instance.PlaySFX(AudioManager.instance.chestOpen);

    private void Awake()
    {
        musicSource.loop = true;

        if (audioManager != null && audioManager != this)
        {
            Destroy(gameObject);
            return;
        }

        audioManager = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void ReplaceMusicPlaying(AudioClip clip)
    {
        previousMusicSource = musicSource.clip;
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void ResumePreviousMusic()
    {
        musicSource.clip = previousMusicSource;
        musicSource.Play();
    }
}
