using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [Header("Settings Buttons")]
    public Button controlGuideButton;
    public Button volumeSettingButton;

    [Header("Panels")]
    public GameObject controlGuidePanel;
    public GameObject volumeSettingPanel;

    private void Start()
    {
        // Setup button listeners
        controlGuideButton.onClick.AddListener(OnControlGuideClicked);
        volumeSettingButton.onClick.AddListener(OnVolumeSettingClicked);
    }

    private void OnControlGuideClicked()
    {
        gameObject.SetActive(false);
        controlGuidePanel.SetActive(true);
    }

    private void OnVolumeSettingClicked()
    {
        gameObject.SetActive(false);
        volumeSettingPanel.SetActive(true);
    }
} 