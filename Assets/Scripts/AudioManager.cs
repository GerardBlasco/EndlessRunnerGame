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
    public AudioClip pikminSwipe;
    public AudioClip pikminJump;
    public AudioClip pikminRoll;
    public AudioClip pikminDeath;
    public AudioClip pikminCrash;

    [Header("- Music:")]
    public AudioClip gameTheme;
    public AudioClip menuTheme;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

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

    private void Start()
    {
        musicSource.clip = menuTheme;
        musicSource.Play();
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "MainMenu":
                PlayMusic(menuTheme);
                break;

            case "GameScene":
                PlayMusic(gameTheme);
                break;
        }
    }
}
