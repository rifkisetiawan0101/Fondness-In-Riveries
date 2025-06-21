using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIALOGUE;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    public static DialogueTrigger Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // Menghindari duplikasi Instance
            return;
        }
        else Instance = this;
        DontDestroyOnLoad(gameObject);
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    [SerializeField] private CutscenePlayer cutscenePlayer;
    [Header("-- Dialogue --")]
    public TextAsset currentDialogue;
    [SerializeField] private TextAsset cutcenePrologue_0;
    [SerializeField] private TextAsset startHumming_1;
    [SerializeField] private TextAsset moveSideScroll_2;
    [SerializeField] private TextAsset swingingBaby_3;
    [SerializeField] private TextAsset putSleepBaby_4;
    [SerializeField] private TextAsset putSleepBaby_5;
    [SerializeField] private TextAsset putSleepBaby_6;
    [SerializeField] private TextAsset closeCurtain_7;
    [SerializeField] private TextAsset afterCurtain_8;
    [SerializeField] private TextAsset turnOffLamp_9;
    [SerializeField] private TextAsset interactPhoto1_10;
    [SerializeField] private TextAsset interactPhoto2_11;
    [SerializeField] private TextAsset interactPhoto3_12;
    [SerializeField] private TextAsset interactPhoto4_13;
    [SerializeField] private TextAsset searchCamera_14;
    [SerializeField] private TextAsset collectCamera_15;
    [SerializeField] private TextAsset grandmaDoor_16;
    [SerializeField] private TextAsset collectTDL_17;
    [SerializeField] private TextAsset makingMilk_18;
    [SerializeField] private TextAsset afterMakingMilk_19;
    [SerializeField] private TextAsset givingMilk_20;
    [SerializeField] private TextAsset afterGivingMilk_21;
    [SerializeField] private TextAsset getWater_22;
    [SerializeField] private TextAsset pourWater_23;
    [SerializeField] private TextAsset bathingBaby_24;
    [SerializeField] private TextAsset repairSwing_25;
    [SerializeField] private TextAsset doneSwing_26;
    [SerializeField] private TextAsset openCamera_27;
    [SerializeField] private TextAsset photoTaken_28;
    [SerializeField] private TextAsset openDiary_29;
    [SerializeField] private TextAsset dragPhoto_30;
    [SerializeField] private TextAsset showDiary_31;
    [SerializeField] private TextAsset searchHotWater;
    [SerializeField] private TextAsset collectHotWater;
    [SerializeField] private TextAsset finalPhoto;

    [Header("-- All Dialogue Flags --")]
    public bool[] allDialogueFlags;

    public void UpdateDialogueFlags()
    {
        allDialogueFlags = new bool[]
        {
            isCutcenePrologue_0Played,
            isStartHumming_1Played,
            isMoveSideScroll_2Played,
            isSwingingBaby_3Played,
            isPutBabySleep_4Played,
            isCloseCurtain_7Played,
            isAfterCurtain_8Played,
            isTurnOffLamp_9Played,
            isPlayInteractPhoto1_10Played,
            isPlayInteractPhoto2_11Played,
            isPlayInteractPhoto3_12Played,
            isPlayInteractPhoto4_13Played,
            isSearchCamera_14Played,
            isCollectCamera_15Played,
            isGrandmaDoor_16Played,
            isCollectTDL_17Played,
            isMakingMilk_18Played,
            isAfterMakingMilk_19Played,
            isGivingMilk_20Played,
            isAfterGivingMilk_21Played,
            isGetWater_22Played,
            isPourWater_23Played,
            isBathingBaby_24Played,
            isRepairSwing_25Played,
            isDoneSwing_26Played,
            isOpenCamera_27Played,
            isPhotoTaken_28Played,
            isOpenDiary_29Played,
            isDragPhoto_30Played,
            isShowDiary_31Played
        };
    }

    public void ApplyDialogueFlags(bool[] flags)
    {
        // if (flags == null || flags.Length != allDialogueFlags.Length) return;
        allDialogueFlags = (bool[])flags.Clone();

        // Sinkronkan ke masing-masing boolean
        isCutcenePrologue_0Played = flags[0];
        isStartHumming_1Played = flags[1];
        isMoveSideScroll_2Played = flags[2];
        isSwingingBaby_3Played = flags[3];
        isPutBabySleep_4Played = flags[4];
        isPutBabySleep_5Played = flags[5];
        isPutBabySleep_6Played = flags[6];
        isCloseCurtain_7Played = flags[7];
        isAfterCurtain_8Played = flags[8];
        isTurnOffLamp_9Played = flags[9];
        isPlayInteractPhoto1_10Played = flags[10];
        isPlayInteractPhoto2_11Played = flags[11];
        isPlayInteractPhoto3_12Played = flags[12];
        isPlayInteractPhoto4_13Played = flags[13];
        isSearchCamera_14Played = flags[14];
        isCollectCamera_15Played = flags[15];
        isGrandmaDoor_16Played = flags[16];
        isCollectTDL_17Played = flags[17];
        isMakingMilk_18Played = flags[18];
        isAfterMakingMilk_19Played = flags[19];
        isGivingMilk_20Played = flags[20];
        isAfterGivingMilk_21Played = flags[21];
        isSearchHotWater = flags[22];
        isCollectHotWater = flags[23];
        isGetWater_22Played = flags[24];
        isPourWater_23Played = flags[25];
        isBathingBaby_24Played = flags[26];
        isRepairSwing_25Played = flags[27];
        isDoneSwing_26Played = flags[28];
        isOpenCamera_27Played = flags[29];
        isPhotoTaken_28Played = flags[30];
        isOpenDiary_29Played = flags[31];
        isDragPhoto_30Played = flags[32];
        isShowDiary_31Played = flags[33];
    }

    private async void OnEnable()
    {
        await Task.Delay(1000);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    bool activeScene2 = false;
    bool activeScene3 = false;
    private async void OpenScene2()
    {
        activeScene2 = true;
        bool[] flagsDialogue = new bool[34] {
            true, true, true, true, true, true, true, true,
            true, true, false, false, false, false, false, false,
            false, false, false, false, false, false, false, false,
            false, false, false, false, false, false, false, false,
            false, false
        };
        ApplyDialogueFlags(flagsDialogue);

        // Tunggu hingga MechanicsManager tersedia
        int attempts = 0;
        while (MechanicsManager.Instance == null && attempts < 10)
        {
            Debug.Log("Waiting for MechanicsManager to initialize...");
            await Task.Delay(100);
            attempts++;
        }

        // Periksa apakah MechanicsManager sudah tersedia
        if (MechanicsManager.Instance != null)
        {
            bool[] flagsMechanic = new bool[43] {
                true, true, true, true, true, true, true, true, true, true,
                // false untuk sisanya
                false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
                false, false, false
            };
            MechanicsManager.Instance.ApplyMechanicsFlags(flagsMechanic);
            MechanicsManager.Instance.isGameStart = true;

        }
        else
        {
            Debug.LogError("MechanicsManager.Instance is null after waiting. Cannot apply mechanics flags.");
        }

        await Task.Delay(2000);
        if (isTurnOffLamp_9Played && !isSearchCamera_14Played)
        {
            Debug.Log("Starting Search Camera dialogue sequence");
            StartCoroutine(PlaySearchCamera());
        }
    }

    private async void OpenScene3()
    {
        activeScene3 = true;
        // Flags untuk Dialogue: 0-22 true, sisanya false
        bool[] flagsDialogue = new bool[34] {
            true, true, true, true, true, true, true, true, true, true, true,
            true, true, true, true, true, true, true, true, true, true, true,
            true, true, false, false, false, false, false, false, false, false, false, false
        };
        ApplyDialogueFlags(flagsDialogue);

        // Tunggu MechanicsManager
        int attempts = 0;
        while (MechanicsManager.Instance == null && attempts < 10)
        {
            Debug.Log("Waiting for MechanicsManager to initialize...");
            await Task.Delay(100);
            attempts++;
        }

        if (MechanicsManager.Instance != null)
        {
            // Flags untuk Mechanic: 0-21 true, 22-23 false, 24 true, sisanya false
            bool[] flagsMechanic = new bool[43] {
                true, true, true, true, true, true, true, true, true, true,  // 0-9
                true, true, true, true, true, true, true, true, true, true,  // 10-19
                true, true,              // 20-21
                false, false,            // 22-23
                true,                    // 24
                false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false
            };
            MechanicsManager.Instance.ApplyMechanicsFlags(flagsMechanic);
            MechanicsManager.Instance.isGameStart = true;
        }
        else
        {
            Debug.LogError("MechanicsManager.Instance is null after waiting. Cannot apply mechanics flags.");
        }

        await Task.Delay(2000);
        if (isCollectHotWater && !isPourWater_23Played)
        {
            Debug.Log("Starting bath baby");
            StartCoroutine(PlayPourWater());
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (!isCutcenePrologue_0Played && scene.name == "Act-1_Scene1_KamarIbu")
        {
            cutscenePlayer = GameObject.Find("[7] - Cutscene").GetComponent<CutscenePlayer>();
            // Tambahkan debug logs
            Debug.Log($"Scene Loaded: {scene.name}");
            Debug.Log($"isTurnOffLamp_9Played: {isTurnOffLamp_9Played}");
            Debug.Log($"isSearchCamera_12Played: {isSearchCamera_14Played}");

            var data = SaveSystem.LoadGameData();
            // if (data != null) return;
            StartCoroutine(PlayCutcenePrologue());
        }

        if (scene.name == "Act-1_Scene2_RuangTamu" && !activeScene2)
        {
            OpenScene2();
        }

        if (scene.name == "Act-1_Scene3_KamarMandi" && !activeScene3)
        {
            OpenScene3();
        }
    }

    private IEnumerator StartAutoDialogue(float delay, bool isInMechanic)
    {
        StartCoroutine(DialogueManager.instance.StartAutoConversationInMechanics(isInMechanic));
        Debug.Log("isOpenMechanic = " + MechanicsManager.Instance.isOpenMechanic);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => !DialogueManager.instance.architect.isBuilding);
        yield return new WaitForSeconds(delay);
    }

    private IEnumerator NextAutoDialogue(float delay)
    {
        StartCoroutine(DialogueManager.instance.OpenDialogue(0.4f));
        DialogueManager.instance.OnDialogue_Next();
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => !DialogueManager.instance.architect.isBuilding);
        yield return new WaitForSeconds(delay);
    }

    [SerializeField] private bool isCutcenePrologue_0Played;
    private IEnumerator PlayCutcenePrologue()
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Act-1_Scene1_KamarIbu");
        cutscenePlayer.cutscene1_Prologue.SetActive(true);
        // StartCoroutine(cutscenePlayer.PlayCutcenePrologue());
        // frame 1 tanpa dialog
        cutscenePlayer.ChangeCutsceneFrame(0);

        currentDialogue = cutcenePrologue_0;
        yield return new WaitForSeconds(5f);

        // frame 2 dialog line 1
        cutscenePlayer.ChangeCutsceneFrame(1);
        yield return StartCoroutine(StartAutoDialogue(2f, false));
        yield return StartCoroutine(DialogueManager.instance.CloseDialogue(0.4f));

        // frame 2 tanpa dialog
        cutscenePlayer.ChangeCutsceneFrame(3);
        yield return new WaitForSeconds(2f);

        // frame 3 dialog line 2
        cutscenePlayer.ChangeCutsceneFrame(0);
        yield return StartCoroutine(NextAutoDialogue(2f));
        yield return StartCoroutine(DialogueManager.instance.CloseDialogue(0.4f));

        cutscenePlayer.cutsceneImage.GetComponent<FadeImage>().FadeOut(0.7f);
        yield return new WaitForSeconds(0.7f);
        cutscenePlayer.cutsceneImage.gameObject.SetActive(false);

        currentDialogue = null;
        isCutcenePrologue_0Played = true;
        StopCoroutine(PlayCutcenePrologue());

        if (isCutcenePrologue_0Played && !isStartHumming_1Played)
        {
            StartCoroutine(PlayStartHumming());
        }
        else
        {
            StopCoroutine(PlayStartHumming());
        }
    }

    [SerializeField] private bool isStartHumming_1Played;
    private IEnumerator PlayStartHumming()
    {
        // yield return new WaitUntil(() => );
        AudioManager.Instance.FadeInMusic(AudioManager.Instance.fondnessMainTheme, 1f, 0f);
        AudioManager.Instance.PlayAmbience();
        currentDialogue = startHumming_1;
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(StartAutoDialogue(2f, false));
        yield return StartCoroutine(NextAutoDialogue(2f));
        yield return StartCoroutine(NextAutoDialogue(0.1f));
        DialogueManager.instance.isAutoConversation = false;
        StartCoroutine(DialogueManager.instance.OpenDialogue(0.4f)); // menyalakan space next done

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isStartHumming_1Played = true;

        if (isStartHumming_1Played && !isMoveSideScroll_2Played)
        {
            StartCoroutine(PlayMoveSideScroll());
        }
        else
        {
            StopCoroutine(PlayMoveSideScroll());
        }
    }

    private GameObject triggerMoveSide;
    private IEnumerator CheckTriggerMoveSide()
    {
        triggerMoveSide = GameObject.Find("TriggerMoveSide");
        float triggerPosX = triggerMoveSide.transform.position.x;
        float triggerWidth = 250f;

        yield return new WaitUntil(() =>
            Mathf.Abs(PlayerMovement.Instance.transform.position.x - triggerPosX) < triggerWidth);

        Debug.Log("Player berada di area trigger MoveSide!");
        triggerMoveSide.SetActive(false);
        StopCoroutine(CheckTriggerMoveSide());
    }

    [SerializeField] private bool isMoveSideScroll_2Played;
    private IEnumerator PlayMoveSideScroll()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(NotificationManager.Instance.NotifMoveSide());
        MechanicsManager.Instance.isGameStart = true;

        yield return StartCoroutine(CheckTriggerMoveSide());

        currentDialogue = moveSideScroll_2;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        MechanicsManager.Instance.isOpenMechanic = false;
        currentDialogue = null;
        isMoveSideScroll_2Played = true;

        if (isMoveSideScroll_2Played && !isSwingingBaby_3Played)
        {
            StartCoroutine(PlaySwingingBaby());
        }
        else
        {
            StopCoroutine(PlaySwingingBaby());
        }
    }

    [SerializeField] public bool isSwingingBaby_3Played;
    public IEnumerator PlaySwingingBaby()
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isSwingingBabyToSleepOpened);
        currentDialogue = swingingBaby_3;
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(StartAutoDialogue(2f, true));
        yield return StartCoroutine(NextAutoDialogue(2f));
        yield return StartCoroutine(NextAutoDialogue(2f));
        yield return StartCoroutine(NextAutoDialogue(2f));
        yield return StartCoroutine(DialogueManager.instance.CloseDialogue(0.4f));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isSwingingBaby_3Played = true;

        if (isSwingingBaby_3Played && !isPutBabySleep_4Played)
        {
            StartCoroutine(PlayBabySleep());
        }
        else
        {
            StopCoroutine(PlayBabySleep());
        }
    }


    [SerializeField] public bool isPutBabySleep_4Played = false;
    private IEnumerator PlayBabySleep()
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isSwingComplete);
        currentDialogue = putSleepBaby_4;
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isPutBabySleep_4Played = true;

        if (isPutBabySleep_4Played && !isPutBabySleep_5Played)
        {
            StartCoroutine(PlayBabySleep_4_1());
        }
        else
        {
            StopCoroutine(PlayBabySleep_4_1());
        }
    }

    [SerializeField] public bool isPutBabySleep_5Played = false;
    private IEnumerator PlayBabySleep_4_1()
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isBabyPanelOpened1);
        currentDialogue = putSleepBaby_5;
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isPutBabySleep_5Played = true;

        if (isPutBabySleep_5Played && !isPutBabySleep_6Played)
        {
            StartCoroutine(PlayBabySleep_4_2());
        }
        else
        {
            StopCoroutine(PlayBabySleep_4_2());
        }
    }

    [SerializeField] public bool isPutBabySleep_6Played = false;
    private IEnumerator PlayBabySleep_4_2()
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isBabyPanelOpened2);
        currentDialogue = putSleepBaby_6;
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isPutBabySleep_6Played = true;

        if (isPutBabySleep_6Played && !isCloseCurtain_7Played)
        {
            StartCoroutine(PlayCloseCurtain());
        }
        else
        {
            StopCoroutine(PlayCloseCurtain());
        }
    }

    private GameObject triggerCurtain;
    private IEnumerator CheckTriggerCurtain()
    {
        triggerCurtain = GameObject.Find("ObjCloseCurtain");
        float triggerPosX = triggerCurtain.transform.position.x;
        float triggerWidth = 500f;

        yield return new WaitUntil(() =>
            Mathf.Abs(PlayerMovement.Instance.transform.position.x - triggerPosX) < triggerWidth);

        Debug.Log("Player berada di area trigger MoveSide!");
        StopCoroutine(CheckTriggerCurtain());
    }

    private CloseCurtain closeCurtain;
    [SerializeField] private bool isCloseCurtain_7Played = false;
    private IEnumerator PlayCloseCurtain()
    {
        closeCurtain = GameObject.Find("CloseCurtain").GetComponent<CloseCurtain>();
        yield return new WaitUntil(() => !MechanicsManager.Instance.isOpenMechanic); // keluar dulu dari swing
        currentDialogue = closeCurtain_7;
        yield return new WaitForSeconds(0.7f);
        yield return StartCoroutine(StartAutoDialogue(1.5f, false)); // line 1
        yield return StartCoroutine(DialogueManager.instance.CloseDialogue(0.4f));

        yield return StartCoroutine(CheckTriggerCurtain());
        MechanicsManager.Instance.isOpenMechanic = true;
        yield return StartCoroutine(NextAutoDialogue(2f)); // masih di luar hordeng, line 2
        yield return StartCoroutine(DialogueManager.instance.CloseDialogue(0.4f));
        MechanicsManager.Instance.isOpenMechanic = false;

        yield return new WaitUntil(() => MechanicsManager.Instance.isCloseCurtainOpened);
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(NextAutoDialogue(2f)); // line 3
        yield return new WaitForSeconds(2f);
        closeCurtain.isCurtainReady = true;
        yield return StartCoroutine(DialogueManager.instance.CloseDialogue(0.4f));

        yield return new WaitUntil(() => closeCurtain.isLeftSwiped);
        yield return StartCoroutine(NextAutoDialogue(2f)); // line 4
        yield return StartCoroutine(DialogueManager.instance.CloseDialogue(0.4f));

        yield return new WaitUntil(() => closeCurtain.isRightSwiped);
        yield return StartCoroutine(NextAutoDialogue(0.1f));
        DialogueManager.instance.isAutoConversation = false;
        StartCoroutine(DialogueManager.instance.OpenDialogue(0.4f)); // menyalakan space next done

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isCloseCurtain_7Played = true;

        if (isCloseCurtain_7Played && !isAfterCurtain_8Played)
        {
            StartCoroutine(PlayAfterCurtain());
        }
        else
        {
            StopCoroutine(PlayAfterCurtain());
        }
    }

    [SerializeField] private bool isAfterCurtain_8Played = false;
    private IEnumerator PlayAfterCurtain()
    {
        yield return new WaitUntil(() => !MechanicsManager.Instance.isOpenMechanic);
        currentDialogue = afterCurtain_8;
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(StartAutoDialogue(2f, false)); // line 1
        yield return StartCoroutine(DialogueManager.instance.CloseDialogue(0.4f));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isAfterCurtain_8Played = true;

        if (isAfterCurtain_8Played && !isTurnOffLamp_9Played)
        {
            StartCoroutine(PlayTurnOffLamp());
        }
        else
        {
            StopCoroutine(PlayTurnOffLamp());
        }
    }

    public bool isTurnOffLamp_9Played = false;
    private IEnumerator PlayTurnOffLamp()
    {
        currentDialogue = turnOffLamp_9;
        yield return new WaitUntil(() => MechanicsManager.Instance.isTurnOffLampPlayed);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isTurnOffLamp_9Played = true;

        if (isTurnOffLamp_9Played)
        {
            // StartCoroutine(PlayInteractPhoto1());
            // StartCoroutine(PlayInteractPhoto2());
            // StartCoroutine(PlayInteractPhoto3());
            // StartCoroutine(PlayInteractPhoto4());
        }
    }

    [SerializeField] private bool isPlayInteractPhoto1_10Played = false;
    private IEnumerator PlayInteractPhoto1()
    {
        if (!isPlayInteractPhoto1_10Played)
        {
            yield return new WaitUntil(() => MechanicsManager.Instance.isInteractPhoto_1Opened);
            currentDialogue = interactPhoto1_10;
            yield return new WaitForSeconds(2f);
            StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));

            yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
            currentDialogue = null;
            isPlayInteractPhoto1_10Played = true;
            StopCoroutine(PlayInteractPhoto1());
        }
    }

    [SerializeField] private bool isPlayInteractPhoto2_11Played = false;
    private IEnumerator PlayInteractPhoto2()
    {
        if (!isPlayInteractPhoto2_11Played)
        {
            yield return new WaitUntil(() => MechanicsManager.Instance.isInteractPhoto_2Opened);
            currentDialogue = interactPhoto2_11;
            yield return new WaitForSeconds(2f);
            StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));

            yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
            currentDialogue = null;
            isPlayInteractPhoto2_11Played = true;
            StopCoroutine(PlayInteractPhoto2());
        }
    }

    [SerializeField] private bool isPlayInteractPhoto3_12Played = false;
    private IEnumerator PlayInteractPhoto3()
    {
        if (!isPlayInteractPhoto3_12Played)
        {
            yield return new WaitUntil(() => MechanicsManager.Instance.isInteractPhoto_3Opened);
            currentDialogue = interactPhoto3_12;
            yield return new WaitForSeconds(2f);
            StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));

            yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
            currentDialogue = null;
            isPlayInteractPhoto3_12Played = true;
            StopCoroutine(PlayInteractPhoto3());
        }
    }

    [SerializeField] private bool isPlayInteractPhoto4_13Played = false;
    private IEnumerator PlayInteractPhoto4()
    {
        if (!isPlayInteractPhoto4_13Played)
        {
            yield return new WaitUntil(() => MechanicsManager.Instance.isInteractPhoto_4Opened);
            currentDialogue = interactPhoto4_13;
            yield return new WaitForSeconds(2f);
            StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));

            yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
            currentDialogue = null;
            isPlayInteractPhoto4_13Played = true;
            StopCoroutine(PlayInteractPhoto4());
        }
    }

    private GameObject triggerSearchCamera;
    private IEnumerator CheckTriggerSearchCamera()
    {
        triggerSearchCamera = GameObject.Find("TriggerSearchCamera");
        float triggerPosX = triggerSearchCamera.transform.position.x;
        float triggerWidth = 100f;

        yield return new WaitUntil(() =>
            Mathf.Abs(PlayerMovement.Instance.transform.position.x - triggerPosX) < triggerWidth);

        Debug.Log("Player berada di area trigger SearchCamera!");
        triggerSearchCamera.SetActive(false);
        StopCoroutine(CheckTriggerSearchCamera());
    }

    [SerializeField] private bool isSearchCamera_14Played;
    private IEnumerator PlaySearchCamera()
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Act-1_Scene2_RuangTamu");
        Debug.Log("PlaySearchCamera started");

        currentDialogue = searchCamera_14;
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(StartAutoDialogue(3f, false));
        yield return StartCoroutine(NextAutoDialogue(3f));
        yield return StartCoroutine(NextAutoDialogue(3f));
        yield return StartCoroutine(DialogueManager.instance.CloseDialogue(0.4f));

        yield return StartCoroutine(CheckTriggerSearchCamera());
        DialogueManager.instance.OnDialogue_Next();
        MechanicsManager.Instance.isOpenMechanic = true;
        DialogueManager.instance.isAutoConversation = false;
        StartCoroutine(DialogueManager.instance.OpenDialogue(0.4f));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        MechanicsManager.Instance.isOpenMechanic = false;
        currentDialogue = null;
        isSearchCamera_14Played = true;

        if (isSearchCamera_14Played && !isCollectCamera_15Played)
        {
            StartCoroutine(PlayCollectCamera());
        }
        else
        {
            StopCoroutine(PlayCollectCamera());
        }
    }

    public bool isCollectCamera_15Played;
    private IEnumerator PlayCollectCamera()
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isCameraCollected);
        StartCoroutine(NotificationManager.Instance.NotifCameraCollected());
        currentDialogue = collectCamera_15;
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(DialogueManager.instance.StartConversationInMechanics(false));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isCollectCamera_15Played = true;

        if (isCollectCamera_15Played && !isCollectTDL_17Played)
        {
            StartCoroutine(PlayCollectTDL());
        }
        else
        {
            StopCoroutine(PlayCollectTDL());
        }
    }

    [SerializeField] private bool isGrandmaDoor_16Played;
    public IEnumerator PlayGrandmaDoor()
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isGrandmaDoorOpened);
        currentDialogue = grandmaDoor_16;
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));
        MechanicsManager.Instance.isOpenMechanic = false;

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isGrandmaDoor_16Played = true;
        MechanicsManager.Instance.isGrandmaDoorOpened = false;
        StopCoroutine(PlayGrandmaDoor());
    }

    public bool isCollectTDL_17Played;
    private IEnumerator PlayCollectTDL() //harusnya open
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isTDLCollected);
        StartCoroutine(NotificationManager.Instance.NotifTDLCollected());
        // StartCoroutine(NotificationManager.Instance.NotifFOpenTDL());
        currentDialogue = collectTDL_17;

        yield return new WaitUntil(() => MechanicsManager.Instance.isTDLOpen);
        yield return new WaitForSeconds(1f);
        StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isCollectTDL_17Played = true;

        if (isCollectTDL_17Played && !isMakingMilk_18Played)
        {
            StartCoroutine(PlayMakingMilk());
        }
        else
        {
            StopCoroutine(PlayMakingMilk());
        }
    }

    public bool isMakingMilk_18Played;
    private IEnumerator PlayMakingMilk()
    {
        currentDialogue = makingMilk_18;
        yield return new WaitUntil(() => MechanicsManager.Instance.isMakingMilkOpened);
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(StartAutoDialogue(2f, true));
        yield return StartCoroutine(DialogueManager.instance.CloseDialogue(0.4f));
        DialogueManager.instance.OnDialogue_Next();

        yield return new WaitUntil(() => MechanicsManager.Instance.isMakingMilkPlayed);
        DialogueManager.instance.isAutoConversation = false;
        yield return StartCoroutine(DialogueManager.instance.OpenDialogue(0.4f));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isMakingMilk_18Played = true;

        if (isMakingMilk_18Played && !isAfterMakingMilk_19Played)
        {
            StartCoroutine(PlayAfterMakingMilk());
        }
        else
        {
            StopCoroutine(PlayAfterMakingMilk());
        }
    }

    [SerializeField] private bool isAfterMakingMilk_19Played;
    private IEnumerator PlayAfterMakingMilk()
    {
        yield return new WaitUntil(() => !MechanicsManager.Instance.isOpenMechanic);
        currentDialogue = afterMakingMilk_19;
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(StartAutoDialogue(2.5f, false));
        yield return StartCoroutine(NextAutoDialogue(2.5f));
        yield return StartCoroutine(NextAutoDialogue(2.5f));
        yield return StartCoroutine(NextAutoDialogue(2.5f));
        yield return StartCoroutine(NextAutoDialogue(2.5f));
        yield return StartCoroutine(DialogueManager.instance.CloseDialogue(0.4f));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isAfterMakingMilk_19Played = true;

        if (isAfterMakingMilk_19Played && !isGivingMilk_20Played)
        {
            StartCoroutine(PlayGivingMilk());
        }
        else
        {
            StopCoroutine(PlayGivingMilk());
        }
    }

    public bool isGivingMilk_20Played;
    private IEnumerator PlayGivingMilk()
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isGivingMilkOpened);
        currentDialogue = givingMilk_20;
        yield return new WaitForSeconds(2f);
        StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isGivingMilk_20Played = true;

        if (isGivingMilk_20Played && !isAfterGivingMilk_21Played)
        {
            StartCoroutine(PlayAfterGivingMilk());
        }
        else
        {
            StopCoroutine(PlayAfterGivingMilk());
        }
    }

    public bool isAfterGivingMilk_21Played;
    private IEnumerator PlayAfterGivingMilk()
    {
        yield return new WaitForSeconds(2f);
        currentDialogue = afterGivingMilk_21;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isAfterGivingMilk_21Played = true;
        MechanicsManager.Instance.isGivingMilkPlayed = true;

        if (isAfterGivingMilk_21Played && !isGetWater_22Played)
        {
            // StartCoroutine(PlayGetWater());
            StartCoroutine(PlaySearchHotWater());
        }
        else
        {
            // StopCoroutine(PlayGetWater());
        }
    }

    // --- Crashing
    [SerializeField] private bool isSearchHotWater;
    private IEnumerator PlaySearchHotWater()
    {
        yield return new WaitUntil(() => !MechanicsManager.Instance.isOpenMechanic);
        currentDialogue = searchHotWater;
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(StartAutoDialogue(2.5f, false));
        yield return StartCoroutine(NextAutoDialogue(2.5f));
        yield return StartCoroutine(DialogueManager.instance.CloseDialogue(0.4f));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isSearchHotWater = true;
        StopCoroutine(PlaySearchHotWater());
        StartCoroutine(PlayCollectHotWater());
    }

    [SerializeField] private bool isCollectHotWater;
    private IEnumerator PlayCollectHotWater()
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isCollectHotWater);
        currentDialogue = collectHotWater;
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(DialogueManager.instance.StartConversationInMechanics(false));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        MechanicsManager.Instance.isOpenMechanic = false;
        currentDialogue = null;
        isCollectHotWater = true;
        StopCoroutine(PlayCollectHotWater());
    }

    [SerializeField] private bool isGetWater_22Played;
    private IEnumerator PlayGetWater()
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isGoingToGetWater);
        currentDialogue = getWater_22;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isGetWater_22Played = true;

        if (isGetWater_22Played && !isPourWater_23Played)
        {
            StartCoroutine(PlayPourWater());
        }
        else
        {
            StopCoroutine(PlayPourWater());
        }
    }

    public bool isPourWater_23Played;
    private IEnumerator PlayPourWater()
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isPourWaterOpened);
        currentDialogue = pourWater_23;
        yield return new WaitForSeconds(2f);
        DialogueManager.instance.isRunningConversation = true;
        StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isPourWater_23Played = true;

        if (isPourWater_23Played && !isBathingBaby_24Played)
        {
            StartCoroutine(PlayBathingBaby());
        }
        else
        {
            StopCoroutine(PlayBathingBaby());
        }
    }

    public bool isBathingBaby_24Played;
    private IEnumerator PlayBathingBaby()
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isBathingBabyPlayed);
        currentDialogue = bathingBaby_24;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        MechanicsManager.Instance.isBathingBabyDialoguePlayed = true;
        isBathingBaby_24Played = true;
        StartCoroutine(PlayFinalPhoto());

        if (isBathingBaby_24Played && !isRepairSwing_25Played)
        {
            StartCoroutine(PlayRepairSwing());
        }
        else
        {
            StopCoroutine(PlayRepairSwing());
        }
    }

    public bool isFinalPhotoPlayed;
    private IEnumerator PlayFinalPhoto()
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isFinalPhotoOpened);
        currentDialogue = finalPhoto;
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(StartAutoDialogue(3f, false));
        yield return StartCoroutine(NextAutoDialogue(3f));
        yield return StartCoroutine(NextAutoDialogue(3f));
        yield return StartCoroutine(DialogueManager.instance.CloseDialogue(0.4f));

        yield return StartCoroutine(DialogueManager.instance.OpenDialogue(0.4f));
        yield return StartCoroutine(NextAutoDialogue(3f));
        yield return StartCoroutine(NextAutoDialogue(3f));
        yield return StartCoroutine(NextAutoDialogue(3f));
        yield return StartCoroutine(DialogueManager.instance.CloseDialogue(0.4f));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        MechanicsManager.Instance.isOpenMechanic = false;
        currentDialogue = null;
        isFinalPhotoPlayed = true;
        StopCoroutine(PlayFinalPhoto());
    }

    [SerializeField] private bool isRepairSwing_25Played;
    private IEnumerator PlayRepairSwing()
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isRepairSwingOpened);
        currentDialogue = repairSwing_25;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isRepairSwing_25Played = true;

        if (isRepairSwing_25Played && !isDoneSwing_26Played)
        {
            StartCoroutine(PlayDoneSwing());
        }
        else
        {
            StopCoroutine(PlayDoneSwing());
        }
    }

    [SerializeField] private bool isDoneSwing_26Played;
    private IEnumerator PlayDoneSwing()
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isRepairSwingPlayed);
        currentDialogue = doneSwing_26;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isDoneSwing_26Played = true;

        if (isDoneSwing_26Played && !isOpenCamera_27Played)
        {
            StartCoroutine(PlayOpenCamera());
        }
        else
        {
            StopCoroutine(PlayOpenCamera());
        }
    }

    [SerializeField] private bool isOpenCamera_27Played;
    private IEnumerator PlayOpenCamera()
    {
        StartCoroutine(NotificationManager.Instance.NotifEReadyCamera());
        yield return new WaitUntil(() => MechanicsManager.Instance.isCameraOpened);
        currentDialogue = openCamera_27;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isOpenCamera_27Played = true;

        if (isOpenCamera_27Played && !isPhotoTaken_28Played)
        {
            StartCoroutine(PlayPhotoTaken());
        }
        else
        {
            StopCoroutine(PlayPhotoTaken());
        }
    }

    public bool isPhotoTaken_28Played;
    private IEnumerator PlayPhotoTaken()
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isPhotoTaken);
        currentDialogue = photoTaken_28;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isPhotoTaken_28Played = true;

        if (isPhotoTaken_28Played && !isOpenDiary_29Played)
        {
            StartCoroutine(PlayOpenDiary());
        }
        else
        {
            StopCoroutine(PlayOpenDiary());
        }
    }

    public bool isOpenDiary_29Played;
    private IEnumerator PlayOpenDiary()
    {
        yield return new WaitUntil(() => !MechanicsManager.Instance.isOpenMechanic);
        StartCoroutine(NotificationManager.Instance.NotifEOpenDiary());
        yield return new WaitUntil(() => MechanicsManager.Instance.isDiaryOpened);
        currentDialogue = openDiary_29;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isOpenDiary_29Played = true;

        if (isOpenDiary_29Played && !isDragPhoto_30Played)
        {
            StartCoroutine(PlayDragPhoto());
        }
        else
        {
            StopCoroutine(PlayDragPhoto());
        }
    }

    public bool isDragPhoto_30Played;
    private IEnumerator PlayDragPhoto()
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isPhotoDragged);
        currentDialogue = dragPhoto_30;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isDragPhoto_30Played = true;

        if (isDragPhoto_30Played && !isShowDiary_31Played)
        {
            StartCoroutine(PlayShowDiary());
        }
        else
        {
            StopCoroutine(PlayShowDiary());
        }
    }

    public bool isShowDiary_31Played;
    private IEnumerator PlayShowDiary()
    {
        yield return new WaitForSeconds(1f);
        currentDialogue = showDiary_31;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DialogueManager.instance.StartConversationInMechanics(true));

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isShowDiary_31Played = true;
    }
}
