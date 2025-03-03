using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public static DialogueTrigger Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // Menghindari duplikasi Instance
            return;
        } else Instance = this;
    }

    [Header("-- Dialogue --")]
    public TextAsset currentDialogue;

    [SerializeField] private TextAsset startHumming_1;
    [SerializeField] private TextAsset moveSideScroll_2;
    [SerializeField] private TextAsset swingingBaby_3;
    [SerializeField] private TextAsset putSleepBaby_4;
    [SerializeField] private TextAsset closeCurtain_5;
    [SerializeField] private TextAsset turnOffLamp_6;
    [SerializeField] private TextAsset afterTurnOffLamp_7;
    [SerializeField] private TextAsset interactPhoto1_8;
    [SerializeField] private TextAsset interactPhoto2_9;
    [SerializeField] private TextAsset interactPhoto3_10;
    [SerializeField] private TextAsset interactPhoto4_11;
    [SerializeField] private TextAsset searchCamera_12;
    [SerializeField] private TextAsset collectCamera_13;
    [SerializeField] private TextAsset grandmaDoor_14;
    [SerializeField] private TextAsset collectTDL_15;
    [SerializeField] private TextAsset makingMilk_16;
    [SerializeField] private TextAsset givingMilk_17;
    [SerializeField] private TextAsset afterGivingMilk_18;
    [SerializeField] private TextAsset getWater_19;
    [SerializeField] private TextAsset pourWater_20;
    [SerializeField] private TextAsset bathingBaby_21;
    [SerializeField] private TextAsset repairSwing_22;
    [SerializeField] private TextAsset doneSwing_23;
    [SerializeField] private TextAsset openCamera_24;
    [SerializeField] private TextAsset photoTaken_25;
    [SerializeField] private TextAsset openDiary_26;
    [SerializeField] private TextAsset dragPhoto_27;
    [SerializeField] private TextAsset showDiary_28;
    
    //DialogueManager.instance.StartConversation();
    private void Start()
    {
        if (!isStartHumming_1Played) {
            StartCoroutine(PlayStartHumming());
        } else {
            StopCoroutine(PlayStartHumming());
        }

        if (!isCollectTDL_15Played) {
            StartCoroutine(PlayCollectTDL());
        } else {
            StopCoroutine(PlayCollectTDL());
        }
    }

    [SerializeField] private bool isStartHumming_1Played;
    private IEnumerator PlayStartHumming()
    {
        yield return new WaitForSeconds(1.5f);
        currentDialogue = startHumming_1;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();
        
        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isStartHumming_1Played = true;

        if (isStartHumming_1Played && !isMoveSideScroll_2Played) {
            StartCoroutine(PlayMoveSideScroll());
        } else {
            StopCoroutine(PlayMoveSideScroll());
        }
    }

    [SerializeField] private bool isMoveSideScroll_2Played;
    private IEnumerator PlayMoveSideScroll()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(NotificationManager.Instance.NotifMoveSide());
        MechanicsManager.Instance.isGameStart = true;
        yield return new WaitUntil (() => PlayerMovement.Instance.isMakMoving);
        currentDialogue = moveSideScroll_2;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();
        
        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isMoveSideScroll_2Played = true;

        if (isMoveSideScroll_2Played && !isSwingingBaby_3Played) {
            StartCoroutine(PlaySwingingBaby());
        } else {
            StopCoroutine(PlaySwingingBaby());
        }
    }

    [SerializeField] public bool isSwingingBaby_3Played;
    public IEnumerator PlaySwingingBaby()
    {
        yield return new WaitUntil (() => MechanicsManager.Instance.isSwingComplete);
        currentDialogue = swingingBaby_3;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();
        
        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isSwingingBaby_3Played = true;

        if (isSwingingBaby_3Played && !isPutBabySleep_4Played) {
            StartCoroutine(PlayBabySleep());
        } else {
            StopCoroutine(PlayBabySleep());
        }
    }

    [SerializeField] public bool isPutBabySleep_4Played = false;
    private IEnumerator PlayBabySleep()
    {
        yield return new WaitUntil (() => MechanicsManager.Instance.isPutBabySleep);
        currentDialogue = putSleepBaby_4;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();
        
        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isPutBabySleep_4Played = true;

        if (isPutBabySleep_4Played && !isCloseCurtain_5Played) {
            StartCoroutine(PlayCloseCurtain());
        } else {
            StopCoroutine(PlayCloseCurtain());
        }
    }

    [SerializeField] public bool isCloseCurtain_5Played = false;
    private IEnumerator PlayCloseCurtain()
    {
        yield return new WaitUntil (() => MechanicsManager.Instance.isCloseCurtainOpened);
        currentDialogue = closeCurtain_5;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();
        
        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isCloseCurtain_5Played = true;

        if (isCloseCurtain_5Played && !isTurnOffLamp_6Played) {
            StartCoroutine(PlayTurnOffLamp());
        } else {
            StopCoroutine(PlayTurnOffLamp());
        }
    }

    [SerializeField] public bool isTurnOffLamp_6Played = false;
    private IEnumerator PlayTurnOffLamp()
    {
        yield return new WaitUntil (() => MechanicsManager.Instance.isTurnOffLampOpened);
        currentDialogue = turnOffLamp_6;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();
        
        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isTurnOffLamp_6Played = true;

        if (isTurnOffLamp_6Played && !isAfterTurnOffLamp_7Played) {
            StartCoroutine(PlayAfterTurnOffLamp());
        } else {
            StopCoroutine(PlayAfterTurnOffLamp());
        }
    }

    [SerializeField] public bool isAfterTurnOffLamp_7Played = false;
    private IEnumerator PlayAfterTurnOffLamp()
    {
        yield return new WaitUntil (() => MechanicsManager.Instance.isTurnOffLampPlayed);
        yield return new WaitUntil (() => !MechanicsManager.Instance.isOpenMechanic);
        currentDialogue = afterTurnOffLamp_7;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();

        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isAfterTurnOffLamp_7Played = true;

        if (isAfterTurnOffLamp_7Played) {
            StartCoroutine(PlayInteractPhoto1());
            StartCoroutine(PlayInteractPhoto2());
            StartCoroutine(PlayInteractPhoto3());
            StartCoroutine(PlayInteractPhoto4());
        }

        if (isAfterTurnOffLamp_7Played && !isSearchCamera_12Played) {
            StartCoroutine(PlaySearchCamera());
        } else {
            StopCoroutine(PlaySearchCamera());
        }
    }

    [SerializeField] public bool isPlayInteractPhoto1_8Played = false;
    private IEnumerator PlayInteractPhoto1()
    {
        if (!isPlayInteractPhoto1_8Played)
        {
            yield return new WaitUntil (() => MechanicsManager.Instance.isInteractPhoto_1Opened);
            currentDialogue = interactPhoto1_8;
            yield return new WaitForSeconds(0.5f);
            DialogueManager.instance.StartConversation();
            
            yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
            currentDialogue = null;
            isPlayInteractPhoto1_8Played = true;
            StopCoroutine(PlayInteractPhoto1());   
        }
    }

    [SerializeField] public bool isPlayInteractPhoto2_9Played = false;
    private IEnumerator PlayInteractPhoto2()
    {
        if (!isPlayInteractPhoto2_9Played)
        {
            yield return new WaitUntil (() => MechanicsManager.Instance.isInteractPhoto_2Opened);
            currentDialogue = interactPhoto2_9;
            yield return new WaitForSeconds(0.5f);
            DialogueManager.instance.StartConversation();
            
            yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
            currentDialogue = null;
            isPlayInteractPhoto2_9Played = true;
            StopCoroutine(PlayInteractPhoto2());
        }
    }

    [SerializeField] public bool isPlayInteractPhoto3_10Played = false;
    private IEnumerator PlayInteractPhoto3()
    {
        if (!isPlayInteractPhoto3_10Played)
        {
            yield return new WaitUntil (() => MechanicsManager.Instance.isInteractPhoto_3Opened);
            currentDialogue = interactPhoto3_10;
            yield return new WaitForSeconds(0.5f);
            DialogueManager.instance.StartConversation();
            
            yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
            currentDialogue = null;
            isPlayInteractPhoto3_10Played = true;
            StopCoroutine(PlayInteractPhoto3());
        }
    }

    [SerializeField] public bool isPlayInteractPhoto4_11Played = false;
    private IEnumerator PlayInteractPhoto4()
    {
        if (!isPlayInteractPhoto4_11Played)
        {
            yield return new WaitUntil (() => MechanicsManager.Instance.isInteractPhoto_4Opened);
            currentDialogue = interactPhoto4_11;
            yield return new WaitForSeconds(0.5f);
            DialogueManager.instance.StartConversation();
            
            yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
            currentDialogue = null;
            isPlayInteractPhoto4_11Played = true;
            StopCoroutine(PlayInteractPhoto4());
        }
    }

    [SerializeField] private bool isSearchCamera_12Played;
    private IEnumerator PlaySearchCamera()
    {
        currentDialogue = searchCamera_12;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();

        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isSearchCamera_12Played = true;

        if (isSearchCamera_12Played && !isCollectCamera_13Played) {
            StartCoroutine(PlayCollectCamera());
        } else {
            StopCoroutine(PlayCollectCamera());
        }
    }

    [SerializeField] private bool isCollectCamera_13Played;
    private IEnumerator PlayCollectCamera()
    {
        yield return new WaitUntil (() => MechanicsManager.Instance.isCameraCollected);
        StartCoroutine(NotificationManager.Instance.NotifCameraCollected());
        currentDialogue = collectCamera_13;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();

        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isCollectCamera_13Played = true;
    }

    [SerializeField] private bool isCollectTDL_15Played;
    private IEnumerator PlayCollectTDL()
    {
        yield return new WaitUntil (() => MechanicsManager.Instance.isTDLCollected);
        StartCoroutine(NotificationManager.Instance.NotifTDLCollected());
        StartCoroutine(NotificationManager.Instance.NotifFOpenTDL());
        currentDialogue = collectTDL_15;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();
        
        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isCollectTDL_15Played = true;

        if (isCollectTDL_15Played && !isMakingMilk_16Played) {
            StartCoroutine(PlayMakingMilk());
        } else {
            StopCoroutine(PlayMakingMilk());
        }
    }

    [SerializeField] private bool isMakingMilk_16Played;
    private IEnumerator PlayMakingMilk()
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isMakingMilkOpened);
        currentDialogue = makingMilk_16;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isMakingMilk_16Played = true;

        if (isMakingMilk_16Played && !isGivingMilk_17Played) {
            StartCoroutine(PlayGivingMilk());
        } else {
            StopCoroutine(PlayGivingMilk());
        }
    }

    [SerializeField] private bool isGivingMilk_17Played;
    private IEnumerator PlayGivingMilk()
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isNowGivingMilk);
        currentDialogue = givingMilk_17;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isGivingMilk_17Played = true;
        MechanicsManager.Instance.isNowGivingMilk = false;

        if (isGivingMilk_17Played && !isAfterGivingMilk_18Played) {
            StartCoroutine(PlayAfterGivingMilk());
        } else {
            StopCoroutine(PlayAfterGivingMilk());
        }
    }

    [SerializeField] private bool isAfterGivingMilk_18Played;
    private IEnumerator PlayAfterGivingMilk()
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isGivingMilkPlayed);
        currentDialogue = afterGivingMilk_18;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        MechanicsManager.Instance.isGivingMilkDialoguePlayed = true;
        isAfterGivingMilk_18Played = true;

        if (isAfterGivingMilk_18Played && !isGetWater_19Played) {
            StartCoroutine(PlayGetWater());
        } else {
            StopCoroutine(PlayGetWater());
        }
    }

    [SerializeField] private bool isGetWater_19Played;
    private IEnumerator PlayGetWater()
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isGoingToGetWater);
        currentDialogue = getWater_19;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isGetWater_19Played = true;

        if (isGetWater_19Played && !isPourWater_20Played) {
            StartCoroutine(PlayPourWater());
        } else {
            StopCoroutine(PlayPourWater());
        }
    }

    [SerializeField] private bool isPourWater_20Played;
    private IEnumerator PlayPourWater()
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isPourWaterOpened);
        currentDialogue = pourWater_20;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isPourWater_20Played = true;

        if (isPourWater_20Played && !isBathingBaby_21Played) {
            StartCoroutine(PlayBathingBaby());
        } else {
            StopCoroutine(PlayBathingBaby());
        }
    }

    [SerializeField] private bool isBathingBaby_21Played;
    private IEnumerator PlayBathingBaby()
    {
        yield return new WaitUntil(() => MechanicsManager.Instance.isBathingBabyPlayed);
        currentDialogue = bathingBaby_21;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();

        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        MechanicsManager.Instance.isBathingBabyDialoguePlayed = true;
        isBathingBaby_21Played = true;

        if (isBathingBaby_21Played && !isRepairSwing_22Played) {
            StartCoroutine(PlayRepairSwing());
        } else {
            StopCoroutine(PlayRepairSwing());
        }
    }

    [SerializeField] private bool isRepairSwing_22Played;
    private IEnumerator PlayRepairSwing()
    {
        yield return new WaitUntil (() => MechanicsManager.Instance.isRepairSwingOpened);
        currentDialogue = repairSwing_22;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();

        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isRepairSwing_22Played = true;

        if (isRepairSwing_22Played && !isDoneSwing_23Played) {
            StartCoroutine(PlayDoneSwing());
        } else {
            StopCoroutine(PlayDoneSwing());
        }
    }

    [SerializeField] private bool isDoneSwing_23Played;
    private IEnumerator PlayDoneSwing()
    {
        yield return new WaitUntil (() => MechanicsManager.Instance.isRepairSwingPlayed);
        currentDialogue = doneSwing_23;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();

        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isDoneSwing_23Played = true;

        if (isDoneSwing_23Played && !isOpenCamera_24Played) {
            StartCoroutine(PlayOpenCamera());
        } else {
            StopCoroutine(PlayOpenCamera());
        }
    }

    [SerializeField] private bool isOpenCamera_24Played;
    private IEnumerator PlayOpenCamera()
    {
        StartCoroutine(NotificationManager.Instance.NotifEReadyCamera());
        yield return new WaitUntil (() => MechanicsManager.Instance.isCameraOpened);
        currentDialogue = openCamera_24;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();

        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isOpenCamera_24Played = true;

        if (isOpenCamera_24Played && !isPhotoTaken_25Played) {
            StartCoroutine(PlayPhotoTaken());
        } else {
            StopCoroutine(PlayPhotoTaken());
        }
    }

    public bool isPhotoTaken_25Played;
    private IEnumerator PlayPhotoTaken()
    {
        yield return new WaitUntil (() => MechanicsManager.Instance.isPhotoTaken);
        currentDialogue = photoTaken_25;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();

        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isPhotoTaken_25Played = true;

        if (isPhotoTaken_25Played && !isOpenDiary_26Played) {
            StartCoroutine(PlayOpenDiary());
        } else {
            StopCoroutine(PlayOpenDiary());
        }
    }

    public bool isOpenDiary_26Played;
    private IEnumerator PlayOpenDiary()
    {
        yield return new WaitUntil (() => !MechanicsManager.Instance.isOpenMechanic);
        StartCoroutine(NotificationManager.Instance.NotifEOpenDiary());
        yield return new WaitUntil (() => MechanicsManager.Instance.isDiaryOpened);
        currentDialogue = openDiary_26;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();

        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isOpenDiary_26Played = true;

        if (isOpenDiary_26Played && !isDragPhoto_27Played) {
            StartCoroutine(PlayDragPhoto());
        } else {
            StopCoroutine(PlayDragPhoto());
        }
    }

    public bool isDragPhoto_27Played;
    private IEnumerator PlayDragPhoto()
    {
        yield return new WaitUntil (() => MechanicsManager.Instance.isPhotoDragged);
        currentDialogue = dragPhoto_27;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();

        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isDragPhoto_27Played = true;

        if (isDragPhoto_27Played && !isShowDiary_28Played) {
            StartCoroutine(PlayShowDiary());
        } else {
            StopCoroutine(PlayShowDiary());
        }
    }

    public bool isShowDiary_28Played;
    private IEnumerator PlayShowDiary()
    {
        yield return new WaitForSeconds(1f);
        currentDialogue = showDiary_28;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();

        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isShowDiary_28Played = true;
    }
}
