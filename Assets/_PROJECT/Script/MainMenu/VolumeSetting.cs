using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Volume Text")]
    [SerializeField] private TextMeshProUGUI masterVolumeText;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private TextMeshProUGUI sfxVolumeText;

    private void Start() {
        // Setup event listeners untuk slider
        masterSlider.onValueChanged.AddListener(OnMasterSliderChanged);
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXSliderChanged);

        if (PlayerPrefs.HasKey("musicVolume")) {
            LoadVolume();
        } else {
            SetMasterVolume();
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    private void OnMasterSliderChanged(float value)
    {
        SetMasterVolume();
        UpdateVolumeText(masterVolumeText, value);
    }

    private void OnMusicSliderChanged(float value)
    {
        SetMusicVolume();
        UpdateVolumeText(musicVolumeText, value);
    }

    private void OnSFXSliderChanged(float value)
    {
        SetSFXVolume();
        UpdateVolumeText(sfxVolumeText, value);
    }

    private void UpdateVolumeText(TextMeshProUGUI text, float value)
    {
        if (text != null)
        {
            // Konversi nilai 0-1 ke 0-10
            int volumeValue = Mathf.RoundToInt(value * 10);
            text.text = volumeValue.ToString();
        }
    }

    public void SetMasterVolume() {
        float volume = masterSlider.value;
        audioMixer.SetFloat("master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterVolume", volume);
        UpdateVolumeText(masterVolumeText, volume);
    }

    public void SetMusicVolume() {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
        UpdateVolumeText(musicVolumeText, volume);
    }

    public void SetSFXVolume() {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
        UpdateVolumeText(sfxVolumeText, volume);
    }

    private void LoadVolume() {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }

    private void OnDestroy()
    {
        // Membersihkan event listeners
        if (masterSlider != null)
        {
            masterSlider.onValueChanged.RemoveListener(OnMasterSliderChanged);
        }
        if (musicSlider != null)
        {
            musicSlider.onValueChanged.RemoveListener(OnMusicSliderChanged);
        }
        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.RemoveListener(OnSFXSliderChanged);
        }
    }
}   
