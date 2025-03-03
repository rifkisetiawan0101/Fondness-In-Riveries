using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool isMenuActive;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button creditButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button backMainMenuButton;

    [SerializeField] private GameObject panelCredit;
    [SerializeField] private GameObject panelSetting;
    [SerializeField] private GameObject panelBackMainMenu;

    [SerializeField] private Button closeCreditButton;
    [SerializeField] private Button closeSettingButton;
    [SerializeField] private Button yesBackButton;
    [SerializeField] private Button noBackButton;

    private void Start()
    {
        menuUI.SetActive(false);
        panelCredit.SetActive(false);
        panelSetting.SetActive(false);
        panelBackMainMenu.SetActive(false);

        resumeButton.onClick.AddListener(() => {
            isMenuActive = false;
            menuUI.SetActive(false);
            Time.timeScale = 1;
            Debug.Log("Game UnPaused. Time.timeScale = " + Time.timeScale);
        });

        creditButton.onClick.AddListener(() => { panelCredit.SetActive(true); });

        settingButton.onClick.AddListener(() => { panelSetting.SetActive(true); });

        closeCreditButton.onClick.AddListener(() => { panelCredit.SetActive(false); });

        closeSettingButton.onClick.AddListener(() => { panelSetting.SetActive(false); });

        backMainMenuButton.onClick.AddListener(() => { panelBackMainMenu.SetActive(true); });

        noBackButton.onClick.AddListener(() => { panelBackMainMenu.SetActive(false); });

        yesBackButton.onClick.AddListener(BackToMainMenu);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuActive = !isMenuActive;
            menuUI.SetActive(isMenuActive);
            if (isMenuActive) { Time.timeScale = 0; } 
            else { Time.timeScale = 1; }
            
            Debug.Log("Game Paused. Time.timeScale = " + Time.timeScale);
        }
    }

    private void BackToMainMenu()
    {
        panelBackMainMenu.SetActive(false);
        isMenuActive = false;
        menuUI.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("Game Paused. Time.timeScale = " + Time.timeScale);
        SceneManager.LoadScene("MainMenu");
    }

    [Header("Button States")]
    [SerializeField] private Sprite normalResume;
    [SerializeField] private Sprite enterResume;
    [SerializeField] private Sprite normalCredit;
    [SerializeField] private Sprite enterCredit;
    [SerializeField] private Sprite normalSetting;
    [SerializeField] private Sprite enterSetting;
    [SerializeField] private Sprite normalBackMainMenu;
    [SerializeField] private Sprite enterBackMainMenu;
    [SerializeField] private Sprite normalClose;
    [SerializeField] private Sprite enterClose;
    [SerializeField] private Sprite normalYes;
    [SerializeField] private Sprite enterYes;
    [SerializeField] private Sprite normalNo;
    [SerializeField] private Sprite enterNo;

    private void OnEnterResume()
    {
        resumeButton.image.sprite = enterResume;
        //AudioManager
    }

    private void OnExitResume()
    {
        resumeButton.image.sprite = normalResume;
    }

    private void OnEnterCredit()
    {
        creditButton.image.sprite = enterCredit;
        //AudioManager
    }

    private void OnExitCredit()
    {
        creditButton.image.sprite = normalCredit;
    }

    private void OnEnterSetting()
    {
        settingButton.image.sprite = enterSetting;
        //AudioManager
    }

    private void OnExitSetting()
    {
        settingButton.image.sprite = normalSetting;
    }

    private void OnEnterBackMainMenu()
    {
        backMainMenuButton.image.sprite = enterBackMainMenu;
        //AudioManager
    }

    private void OnExitBackMainMenu()
    {
        backMainMenuButton.image.sprite = normalBackMainMenu;
    }

    private void OnEnterCloseCredit()
    {
        closeCreditButton.image.sprite = enterClose;
        //AudioManager
    }

    private void OnExitCloseCredit()
    {
        closeCreditButton.image.sprite = normalClose;
    }

    private void OnEnterCloseSetting()
    {
        closeSettingButton.image.sprite = enterClose;
        //AudioManager
    }

    private void OnExitCloseSetting()
    {
        closeSettingButton.image.sprite = normalClose;
    }

    private void OnEnterYes()
    {
        yesBackButton.image.sprite = enterYes;
        //AudioManager
    }

    private void OnExitYes()
    {
        yesBackButton.image.sprite = normalYes;
    }

    private void OnEnterNo()
    {
        noBackButton.image.sprite = enterNo;
        //AudioManager
    }

    private void OnExitNo()
    {
        noBackButton.image.sprite = normalNo;
    }
}
