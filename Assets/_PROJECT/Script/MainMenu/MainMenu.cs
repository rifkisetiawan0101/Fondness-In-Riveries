using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject popUpPanel;

    [Header("Panel References")]
    [SerializeField] private GameObject ladaHitamLogo;
    [SerializeField] private GameObject previewMainMenu;
    public GameObject mainMenuPanel;
    public GameObject loadGamePanel;
    public GameObject settingsPanel;
    public GameObject controlGuidePanel;
    public GameObject volumeSettingPanel;
    public GameObject creditPanel;
    public GameObject quitPanel;

    [Header("Button References")]
    public Button newGameButton;
    public Button continueButton;
    public Button loadGameButton;
    public Button settingsButton;
    public Button creditButton;
    public Button quitButton;

    [Header("Settings")]
    public float panelCloseDelay = 0.1f; // Delay sebelum menutup panel

    private void Start()
    {
        // Set semua panel nonaktif kecuali main menu
        loadGamePanel.SetActive(false);
        settingsPanel.SetActive(false);
        controlGuidePanel.SetActive(false);
        volumeSettingPanel.SetActive(false);
        creditPanel.SetActive(false);
        quitPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        previewMainMenu.SetActive(false);
        StartCoroutine(FadeLadaHitam(0.7f));

        // Setup button listeners
        SetupButtonListeners();
    }

    private void Update()
    {
        // Handle escape key for all panels ketika key dilepas
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            StartCoroutine(HandleEscapeKeyWithDelay());
        }
    }

    private IEnumerator HandleEscapeKeyWithDelay()
    {
        // Tunggu sebentar agar efek visual button selesai
        yield return new WaitForSeconds(panelCloseDelay);

        if (loadGamePanel.activeSelf)
        {
            ResetAllButtons();
            loadGamePanel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
        else if (settingsPanel.activeSelf)
        {
            ResetAllButtons();
            settingsPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
        else if (controlGuidePanel.activeSelf)
        {
            ResetAllButtons();
            controlGuidePanel.SetActive(false);
            settingsPanel.SetActive(true);
        }
        else if (volumeSettingPanel.activeSelf)
        {
            ResetAllButtons();
            volumeSettingPanel.SetActive(false);
            settingsPanel.SetActive(true);
        }
        else if (creditPanel.activeSelf)
        {
            ResetAllButtons();
            creditPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
        else if (quitPanel.activeSelf)
        {
            ResetAllButtons();
            quitPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
    }

    private void ResetAllButtons()
    {
        // Reset semua button di main menu
        ButtonHoverEffect[] buttonEffects = mainMenuPanel.GetComponentsInChildren<ButtonHoverEffect>();
        foreach (ButtonHoverEffect effect in buttonEffects)
        {
            effect.ResetToDefault();
        }

        // Reset semua button di panel yang sedang aktif
        if (loadGamePanel.activeSelf)
        {
            buttonEffects = loadGamePanel.GetComponentsInChildren<ButtonHoverEffect>();
            foreach (ButtonHoverEffect effect in buttonEffects)
            {
                effect.ResetToDefault();
            }
        }
        else if (settingsPanel.activeSelf)
        {
            buttonEffects = settingsPanel.GetComponentsInChildren<ButtonHoverEffect>();
            foreach (ButtonHoverEffect effect in buttonEffects)
            {
                effect.ResetToDefault();
            }
        }
        else if (controlGuidePanel.activeSelf)
        {
            buttonEffects = controlGuidePanel.GetComponentsInChildren<ButtonHoverEffect>();
            foreach (ButtonHoverEffect effect in buttonEffects)
            {
                effect.ResetToDefault();
            }
        }
        else if (volumeSettingPanel.activeSelf)
        {
            buttonEffects = volumeSettingPanel.GetComponentsInChildren<ButtonHoverEffect>();
            foreach (ButtonHoverEffect effect in buttonEffects)
            {
                effect.ResetToDefault();
            }
        }
        else if (creditPanel.activeSelf)
        {
            buttonEffects = creditPanel.GetComponentsInChildren<ButtonHoverEffect>();
            foreach (ButtonHoverEffect effect in buttonEffects)
            {
                effect.ResetToDefault();
            }
        }
        else if (quitPanel.activeSelf)
        {
            buttonEffects = quitPanel.GetComponentsInChildren<ButtonHoverEffect>();
            foreach (ButtonHoverEffect effect in buttonEffects)
            {
                effect.ResetToDefault();
            }
        }
    }

    private void SetupButtonListeners()
    {
        newGameButton.onClick.AddListener(OnNewGameClicked);
        continueButton.onClick.AddListener(OnContinueClicked);
        loadGameButton.onClick.AddListener(OnLoadGameClicked);
        settingsButton.onClick.AddListener(OnSettingsClicked);
        creditButton.onClick.AddListener(OnCreditClicked);
        quitButton.onClick.AddListener(OnQuitClicked);
    }

    private void OnNewGameClicked()
    {
        SaveSystem.NewGame();
    }

    private void OnContinueClicked()
    {
        SaveSystem.ContinueGame(popUpPanel);
    }

    private void OnLoadGameClicked()
    {
        mainMenuPanel.SetActive(false);
        loadGamePanel.SetActive(true);
    }

    private void OnSettingsClicked()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    private void OnCreditClicked()
    {
        mainMenuPanel.SetActive(false);
        creditPanel.SetActive(true);
    }

    private void OnQuitClicked()
    {
        mainMenuPanel.SetActive(false);
        quitPanel.SetActive(true);
    }

    private IEnumerator FadeLadaHitam(float fade)
    {
        ladaHitamLogo.SetActive(true);
        ladaHitamLogo.GetComponent<FadeImage>().FadeIn(fade);
        yield return new WaitForSeconds(fade);
        previewMainMenu.SetActive(true);
        yield return new WaitForSeconds(2f);
        ladaHitamLogo.GetComponent<FadeImage>().FadeOut(fade);
        previewMainMenu.GetComponent<FadeImage>().FadeIn(fade);
        yield return new WaitForSeconds(fade);
        mainMenuPanel.SetActive(true);
        ladaHitamLogo.SetActive(false);
        previewMainMenu.SetActive(false);
    }
} 