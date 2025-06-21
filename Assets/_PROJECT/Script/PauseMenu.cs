using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool isMenuActive;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private Button diaryButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button audioButton;
    [SerializeField] private Button controlButton;
    [SerializeField] private Button backMainMenuButton;

    [SerializeField] private GameObject panelDiary;
    [SerializeField] private GameObject panelSetting;
    [SerializeField] private GameObject panelAudio;
    [SerializeField] private GameObject panelControl;
    [SerializeField] private GameObject panelBackMainMenu;

    [SerializeField] private Button yesBackButton;
    [SerializeField] private Button noBackButton;

    [SerializeField] private bool isPanelDiaryActive;
    [SerializeField] private bool isPanelSettingActive;
    [SerializeField] private bool isPanelAudioActive;
    [SerializeField] private bool isPanelControlActive;
    [SerializeField] private bool isPanelBackMainMenuActive;

    private void Start()
    {
        menuUI.SetActive(false);
        panelDiary.SetActive(false);
        panelSetting.SetActive(false);
        panelAudio.SetActive(false);
        panelBackMainMenu.SetActive(false);

        diaryButton.onClick.AddListener(() => { panelDiary.SetActive(true); isPanelDiaryActive = true;});
        settingButton.onClick.AddListener(() => { panelSetting.SetActive(true); isPanelSettingActive = true;});
        audioButton.onClick.AddListener(() => { 
            panelAudio.SetActive(true); 
            isPanelAudioActive = true;
            panelControl.SetActive(false);
            isPanelControlActive = false;
        });
        controlButton.onClick.AddListener(() => { 
            panelControl.SetActive(true);
            isPanelControlActive = true;
            panelAudio.SetActive(false);
            isPanelAudioActive = false;
        });

        backMainMenuButton.onClick.AddListener(() => { panelBackMainMenu.SetActive(true); });
        noBackButton.onClick.AddListener(() => { panelBackMainMenu.SetActive(false); });
        yesBackButton.onClick.AddListener(BackToMainMenu);
    }

    private void Update()
    {
        if (!isPanelDiaryActive && !isPanelSettingActive && !isPanelAudioActive && !isPanelControlActive && MechanicsManager.Instance.isGameStart &&  Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuActive = !isMenuActive;
            menuUI.SetActive(isMenuActive);
            if (isMenuActive) { Time.timeScale = 0; } 
            else { Time.timeScale = 1; }
            
            Debug.Log("Game Paused. Time.timeScale = " + Time.timeScale);
        }

        if (isPanelDiaryActive && Input.GetKeyDown(KeyCode.Escape)) { panelDiary.SetActive(false); isPanelDiaryActive = false; }
        if (isPanelSettingActive && Input.GetKeyDown(KeyCode.Escape)) 
        { 
            panelSetting.SetActive(false);
            isPanelSettingActive = false;
            panelAudio.SetActive(false);
            isPanelAudioActive = false;
            panelControl.SetActive(false);
            isPanelControlActive = false;
        }

        if (isPanelBackMainMenuActive && Input.GetKeyDown(KeyCode.Escape)) { panelBackMainMenu.SetActive(false); isPanelBackMainMenuActive = false; }
        
        // if (isPanelAudioActive && Input.GetKeyDown(KeyCode.Escape)) { panelAudio.SetActive(false); isPanelAudioActive = false; }
        // if (isPanelControlActive && Input.GetKeyDown(KeyCode.Escape)) { panelControl.SetActive(false); isPanelControlActive = false; }
    }

    private void BackToMainMenu()
    {
        panelBackMainMenu.SetActive(false);
        isMenuActive = false;
        menuUI.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("Game Paused. Time.timeScale = " + Time.timeScale);
        SceneManager.LoadScene("Main Menu");
    }

    [Header("Button States")]
    [SerializeField] private Sprite normalDiary;
    [SerializeField] private Sprite enterDiary;
    [SerializeField] private Sprite pressedDiary;
    [SerializeField] private Sprite normalSetting;
    [SerializeField] private Sprite enterSetting;
    [SerializeField] private Sprite pressedSetting;
    [SerializeField] private Sprite normalControl;
    [SerializeField] private Sprite enterControl;
    [SerializeField] private Sprite pressedControl;
    [SerializeField] private Sprite normalBackMainMenu;
    [SerializeField] private Sprite enterBackMainMenu;
    [SerializeField] private Sprite pressedBackMainMenu;
    [SerializeField] private Sprite normalYes;
    [SerializeField] private Sprite enterYes;
    [SerializeField] private Sprite pressedYes;
    [SerializeField] private Sprite normalNo;
    [SerializeField] private Sprite enterNo;
    [SerializeField] private Sprite pressedNo;

    public void OnEnterDiary()
    {
        diaryButton.image.sprite = enterDiary;
        //AudioManager
    }

    public void OnPressedDiary()
    {
        diaryButton.image.sprite = pressedDiary;
        //AudioManager
    }

    public void OnExitDiary()
    {
        diaryButton.image.sprite = normalDiary;
    }

    public void OnEnterSetting()
    {
        settingButton.image.sprite = enterSetting;
        //AudioManager
    }

    public void OnPressedSetting()
    {
        settingButton.image.sprite = pressedSetting;
        //AudioManager
    }

    public void OnExitSetting()
    {
        settingButton.image.sprite = normalSetting;
    }

    public void OnEnterControl()
    {
        controlButton.image.sprite = enterControl;
        //AudioManager
    }

    public void OnPressedControl()
    {
        controlButton.image.sprite = pressedControl;
        //AudioManager
    }

    public void OnExitControl()
    {
        controlButton.image.sprite = normalControl;
    }

    public void OnEnterBackMainMenu()
    {
        backMainMenuButton.image.sprite = enterBackMainMenu;
        //AudioManager
    }

    public void OnPressedBackMainMenu()
    {
        backMainMenuButton.image.sprite = pressedBackMainMenu;
        //AudioManager
    }

    public void OnExitBackMainMenu()
    {
        backMainMenuButton.image.sprite = normalBackMainMenu;
    }

    public void OnEnterYes()
    {
        yesBackButton.image.sprite = enterYes;
        //AudioManager
    }

    public void OnPressedYes()
    {
        yesBackButton.image.sprite = pressedYes;
        //AudioManager
    }

    public void OnExitYes()
    {
        yesBackButton.image.sprite = normalYes;
    }

    public void OnEnterNo()
    {
        noBackButton.image.sprite = enterNo;
        //AudioManager
    }

    public void OnPressedrNo()
    {
        noBackButton.image.sprite = pressedNo;
        //AudioManager
    }

    public void OnExitNo()
    {
        noBackButton.image.sprite = normalNo;
    }
}
