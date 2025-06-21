using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("---Audio Source---")]
    public AudioSource musicSource;
    public AudioSource ambienceSource;
    public AudioSource SFXSource;

    [Header("---Music---")]
    public AudioClip mainMenu;
    public AudioClip fondnessMainTheme;
    public AudioClip sideScrollAmbience;

    [Header("---SFX---")]
    public AudioClip makWalking;
    public AudioClip buttonClick;
    public AudioClip buttonHover;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            musicSource.loop = true;
            ambienceSource.loop = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            ChangeMusic(mainMenu, 1f, 1f, 0f);
            ambienceSource.Stop();
        }
    }

    public void PlayAmbience()
    {
        ambienceSource.clip = sideScrollAmbience;
        ambienceSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    // Fade In ---------------------------------------------------------------------------
    public void FadeInMusic(AudioClip newClip, float fadeDuration, float startTime = 0f)
    {
        StartCoroutine(FadeInCoroutine(newClip, fadeDuration, startTime));
    }

    private IEnumerator FadeInCoroutine(AudioClip newClip, float fadeDuration, float startTime)
    {
        musicSource.clip = newClip;
        musicSource.time = startTime; // Set waktu mulai
        musicSource.volume = 0f;
        musicSource.Play();

        float targetVolume = 1f;
        while (musicSource.volume < targetVolume)
        {
            musicSource.volume += targetVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        musicSource.volume = targetVolume;
    }

    // Fade Out ---------------------------------------------------------------------------
    public void FadeOutMusic(float fadeDuration)
    {
        StartCoroutine(FadeOutCoroutine(fadeDuration));
    }

    private IEnumerator FadeOutCoroutine(float fadeDuration)
    {
        float startVolume = musicSource.volume;

        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVolume;
    }

    // Ganti Music ---------------------------------------------------------------------------
    public void ChangeMusic(AudioClip newClip, float fadeOutDuration, float fadeInDuration, float startTime = 0f)
    {
        if (musicSource.clip != newClip)
        {
            StartCoroutine(FadeOutAndChangeMusicCoroutine(newClip, fadeOutDuration, fadeInDuration, startTime));
        }
    }

    private IEnumerator FadeOutAndChangeMusicCoroutine(AudioClip newClip, float fadeOutDuration, float fadeInDuration, float startTime)
    {
        float startVolume = musicSource.volume;

        // Fade out
        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / fadeOutDuration;
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = 0f;
        musicSource.clip = newClip;
        musicSource.time = startTime; // Set waktu mulai
        musicSource.Play();

        // Fade in
        while (musicSource.volume < startVolume)
        {
            musicSource.volume += startVolume * Time.deltaTime / fadeInDuration;
            yield return null;
        }

        musicSource.volume = startVolume;
    }

}