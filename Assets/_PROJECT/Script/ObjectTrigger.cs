using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIALOGUE;
using UnityEngine.UI;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    public MechanicName mechanicName;
    [SerializeField] private MechanicsManager mechanicsManager;
    [SerializeField] private GameObject eToInteract;
    [SerializeField] private Sprite onPressSprite;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private GameObject mechanics;
    [SerializeField] private bool playerInTrigger = false;

    private BoxCollider2D objCollider2D;
    // private Animator objAnimator;

    // [Header("Hover Sprite")]
    // private SpriteRenderer objHoverSprite;

    private void Start()
    {
        mechanicsManager = FindObjectOfType<MechanicsManager>();
        objCollider2D = GetComponent<BoxCollider2D>();
        objCollider2D.enabled = false;
        // objAnimator = GetComponent<Animator>();
        // if (objAnimator != null)
        // { objAnimator.enabled = false; }

        // objHoverSprite = GetComponent<SpriteRenderer>();
        // objHoverSprite.enabled = false;
    }

    private void Update()
    {
        EnableMechanic();
        if (playerInTrigger && !PlayerMovement.Instance.isMakMoving && !mechanicsManager.isOpenMechanic && Input.GetKey(KeyCode.E))
        {
            eToInteract.GetComponent<Image>().sprite = onPressSprite;
        }

        if (playerInTrigger && !PlayerMovement.Instance.isMakMoving && !mechanicsManager.isOpenMechanic && Input.GetKeyUp(KeyCode.E))
        {
            eToInteract.GetComponent<Image>().sprite = normalSprite;
            _ = RunMechanic();
        }

        if (mechanicName == MechanicName.photoMemoryAct1 && playerInTrigger && !DialogueManager.instance.isRunningConversation && Input.GetKeyUp(KeyCode.E))
        {
            eToInteract.GetComponent<Image>().sprite = normalSprite;
            _ = RunMechanic();
        }
    }

    private void EnableMechanic()
    {
        switch (mechanicName)
        {
            case MechanicName.swingingBabyToSleep:
                if (!mechanicsManager.isSwingingBabyToSleepOpened && !DialogueManager.instance.isRunningConversation)
                { objCollider2D.enabled = true; }
                else
                {
                    objCollider2D.enabled = false;
                    // objAnimator.enabled = false; 
                }
                break;

            case MechanicName.closeCurtain:
                if (mechanicsManager.isSwingingBabyToSleepPlayed && !mechanicsManager.isCloseCurtainOpened && !DialogueManager.instance.isRunningConversation)
                { objCollider2D.enabled = true; }
                else { objCollider2D.enabled = false; }
                // objAnimator.enabled = false;
                break;

            case MechanicName.turnOffLamp:
                if (mechanicsManager.isCloseCurtainPlayed && !mechanicsManager.isTurnOffLampPlayed && !DialogueManager.instance.isRunningConversation)
                { objCollider2D.enabled = true; }
                else
                {
                    objCollider2D.enabled = false;
                    // objAnimator.enabled = false;
                }
                break;

            case MechanicName.doorLivingRoom_fromMaRoom:
                if (mechanicsManager.isTurnOffLampPlayed && !DialogueManager.instance.isRunningConversation)
                { objCollider2D.enabled = true; }
                else
                {
                    objCollider2D.enabled = false;
                    // objAnimator.enabled = false;
                }
                break;

            case MechanicName.interactPhoto1:
            case MechanicName.interactPhoto2:
            case MechanicName.interactPhoto3:
            case MechanicName.interactPhoto4:
                if (mechanicsManager.isTurnOffLampPlayed && !DialogueManager.instance.isRunningConversation)
                { objCollider2D.enabled = true; }
                break;

            case MechanicName.doorMaRoom:
                if (mechanicsManager.isCameraCollected && !DialogueManager.instance.isRunningConversation)
                { objCollider2D.enabled = true; }
                else { objCollider2D.enabled = false; }
                break;

            case MechanicName.cameraPolaroid:
                if (!mechanicsManager.isCameraCollected && !DialogueManager.instance.isRunningConversation)
                { objCollider2D.enabled = true; }
                else { objCollider2D.enabled = false; }
                break;

            case MechanicName.grandmaDoor:
                if (DialogueTrigger.Instance.isCollectCamera_15Played & !DialogueManager.instance.isRunningConversation)
                { objCollider2D.enabled = true; }
                break;

            case MechanicName.toDoList:
                if (DialogueTrigger.Instance.isCollectCamera_15Played && !mechanicsManager.isTDLCollected && !DialogueManager.instance.isRunningConversation)
                { objCollider2D.enabled = true; }
                else { objCollider2D.enabled = false; }
                break;

            case MechanicName.makingMilk:
                if (DialogueTrigger.Instance.isCollectTDL_17Played && !mechanicsManager.isMakingMilkPlayed && !DialogueManager.instance.isRunningConversation)
                { objCollider2D.enabled = true; }
                else { objCollider2D.enabled = false; }
                break;

            case MechanicName.givingMilk:
                if (mechanicsManager.isMakingMilkPlayed && !mechanicsManager.isGivingMilkPlayed && !DialogueManager.instance.isRunningConversation)
                { objCollider2D.enabled = true; } // nyusuin
                else if (mechanicsManager.isPourWaterPlayed && !mechanicsManager.isGivingMilkPlayed2 && !DialogueManager.instance.isRunningConversation)
                { objCollider2D.enabled = true; } // ambil arel
                else if (mechanicsManager.isGivingMilkPlayed2)
                { objCollider2D.enabled = false; }
                // else if (mechanicsManager.isBathingBabyPlayed && !mechanicsManager.isGivingMilkPlayed3 && !DialogueManager.instance.isRunningConversation)
                    // { objCollider2D.enabled = true; } // naro arel
                    // else { objCollider2D.enabled = false; }
                    break;

            // --- crashing
            // case MechanicName.getWater:
            //     if (mechanicsManager.isGivingMilkPlayed && !mechanicsManager.isGetWaterPlayed && !DialogueManager.instance.isRunningConversation)
            //     { objCollider2D.enabled = true; }
            //     else { objCollider2D.enabled = false; }
            //     break;

            // case MechanicName.boilWater:
            //     if (mechanicsManager.isGetWaterPlayed && !mechanicsManager.isBoilWaterPlayed && !DialogueManager.instance.isRunningConversation)
            //     { objCollider2D.enabled = true; }
            //     else { objCollider2D.enabled = false; }
            //     break;
            // crashing ---

            case MechanicName.hotWater:
                if (mechanicsManager.isGivingMilkPlayed && !DialogueManager.instance.isRunningConversation)
                { objCollider2D.enabled = true; }
                else { objCollider2D.enabled = false; }
                break;

            case MechanicName.doorKamarMandi:
                if (mechanicsManager.isCollectHotWater && !DialogueManager.instance.isRunningConversation)
                { objCollider2D.enabled = true; }
                else { objCollider2D.enabled = false; }
                break;

            case MechanicName.pourWater:
                if (mechanicsManager.isCollectHotWater && !mechanicsManager.isPourWaterPlayed && !DialogueManager.instance.isRunningConversation)
                { objCollider2D.enabled = true; }
                else { objCollider2D.enabled = false; }
                break;

            case MechanicName.bathingBaby:
                if (!mechanicsManager.isBathingBabyPlayed && mechanicsManager.isGivingMilkPlayed2 && !DialogueManager.instance.isRunningConversation)
                { objCollider2D.enabled = true; }
                else { objCollider2D.enabled = false; }
                break;

            case MechanicName.doorLivingRoom_fromKamarMandi:
                if (mechanicsManager.isPourWaterPlayed && !DialogueManager.instance.isRunningConversation)
                { objCollider2D.enabled = true; }
                else { objCollider2D.enabled = false; }
                break;

            case MechanicName.finalPhoto:
                if (mechanicsManager.isBathingBabyPlayed && !DialogueManager.instance.isRunningConversation)
                { objCollider2D.enabled = true; }
                else { objCollider2D.enabled = false; }
                break;

                // --- crashing
                // case MechanicName.repairSwing:
                //     if (mechanicsManager.isBathingBabyPlayed && !mechanicsManager.isRepairSwingPlayed && !DialogueManager.instance.isRunningConversation)
                //     { objCollider2D.enabled = true; }
                //     else { objCollider2D.enabled = false; }
                //     break;

                // case MechanicName.photoMemoryAct1:
                //     if (mechanicsManager.isRepairSwingPlayed && !mechanicsManager.isPhotoTaken && !DialogueManager.instance.isRunningConversation)
                //     { objCollider2D.enabled = true; }
                //     else { objCollider2D.enabled = false; }
                //     break;

                // case MechanicName.diaryBook:
                //     if (mechanicsManager.isPhotoTaken && !mechanicsManager.isOpenMechanic && !mechanicsManager.isDiaryOpened && !DialogueManager.instance.isRunningConversation)
                //     { objCollider2D.enabled = true; }
                //     else { objCollider2D.enabled = false; }
                //     break;
                // crashing ---
        }
    }

    private async Task RunMechanic()
    {
        await Task.Delay(100);
        switch (mechanicName)
        {
            case MechanicName.swingingBabyToSleep:
                OpenMechanics(ref mechanicsManager.isSwingingBabyToSleepOpened);
                PlayerMovement.Instance.isCarryBaby = false;
                break;

            case MechanicName.closeCurtain:
                OpenMechanics(ref mechanicsManager.isCloseCurtainOpened);
                break;

            case MechanicName.turnOffLamp:
                OpenMechanics(ref mechanicsManager.isTurnOffLampOpened);
                break;

            case MechanicName.doorLivingRoom_fromMaRoom:
                SaveSystem.SaveGame(1);
                TransitionManager.Instance.LoadAsyncScene("Act-1_Scene2_RuangTamu", MechanicsManager.Instance.isScene2Loaded);
                await Task.Delay(2000);
                PlayerMovement.Instance.transform.position = new Vector3(-5480f, 0f, 0f);
                break;

            case MechanicName.interactPhoto1:
                OpenMechanics(ref mechanicsManager.isInteractPhoto_1Opened);
                break;

            case MechanicName.interactPhoto2:
                OpenMechanics(ref mechanicsManager.isInteractPhoto_2Opened);
                break;

            case MechanicName.interactPhoto3:
                OpenMechanics(ref mechanicsManager.isInteractPhoto_3Opened);
                break;

            case MechanicName.interactPhoto4:
                OpenMechanics(ref mechanicsManager.isInteractPhoto_4Opened);
                break;

            case MechanicName.doorMaRoom:
                SaveSystem.SaveGame(2);
                TransitionManager.Instance.LoadAsyncScene("Act-1_Scene1_KamarIbu", MechanicsManager.Instance.isScene1Loaded);
                await Task.Delay(2000);
                PlayerMovement.Instance.transform.position = new Vector3(2700f, 0f, 0f);
                break;

            case MechanicName.cameraPolaroid:
                mechanicsManager.isCameraCollected = true;
                eToInteract.GetComponent<FadeImage>().FadeOut(0.4f);
                await Task.Delay(400);
                eToInteract.SetActive(false);
                gameObject.SetActive(false);
                break;

            case MechanicName.grandmaDoor:
                mechanicsManager.isGrandmaDoorOpened = true;
                eToInteract.GetComponent<FadeImage>().FadeOut(0.4f);
                await Task.Delay(400);
                StartCoroutine(DialogueTrigger.Instance.PlayGrandmaDoor());
                break;

            case MechanicName.toDoList:
                mechanicsManager.isTDLCollected = true;
                eToInteract.GetComponent<FadeImage>().FadeOut(0.4f);
                await Task.Delay(400);
                ObjectiveManager.Instance.SetObjective(true, false, false, false); // make milk
                ToDoList tdl = GameObject.Find("[ToDoList]").GetComponent<ToDoList>();
                tdl.buttonOpenTDL.gameObject.SetActive(true);
                eToInteract.SetActive(false);
                gameObject.SetActive(false);
                break;

            case MechanicName.makingMilk:
                OpenMechanics(ref mechanicsManager.isMakingMilkOpened);
                ObjectiveManager.Instance.SetObjective(true, true, false, false); // give milk
                await Task.Delay(1000);
                gameObject.SetActive(false);
                break;

            case MechanicName.givingMilk:
                if (mechanicsManager.isMakingMilkPlayed && !mechanicsManager.isGivingMilkPlayed && !mechanicsManager.isPourWaterPlayed)
                {
                    OpenMechanics(ref mechanicsManager.isGivingMilkOpened);
                    ObjectiveManager.Instance.SetObjective(true, true, true, false); // prepare bath
                } // nyusuin
                else if (mechanicsManager.isPourWaterPlayed && !mechanicsManager.isGivingMilkPlayed2)
                {
                    GameObject objArell = GameObject.Find("ObjArell");
                    objArell.SetActive(false);
                    mechanicsManager.isGivingMilkPlayed2 = true;
                    // OpenMechanics(ref mechanicsManager.isGivingMilkPlayed2);
                } // ambil arel
                // else if (mechanicsManager.isBathingBabyPlayed && !mechanicsManager.isGivingMilkPlayed3)
                // { OpenMechanics(ref mechanicsManager.isGivingMilkPlayed3); } // naro arel
                break;

            // crashing
            // case MechanicName.getWater:
            //     OpenMechanics(ref mechanicsManager.isGetWaterOpened);
            //     break;

            // case MechanicName.boilWater:
            //     OpenMechanics(ref mechanicsManager.isBoilWaterOpened);
            //     break;

            case MechanicName.hotWater:
                eToInteract.GetComponent<FadeImage>().FadeOut(0.4f);
                await Task.Delay(400);
                MechanicsManager.Instance.isCollectHotWater = true;
                eToInteract.SetActive(false);
                gameObject.SetActive(false);
                break;

            case MechanicName.doorKamarMandi:
                SaveSystem.SaveGame(3);
                TransitionManager.Instance.LoadAsyncScene("Act-1_Scene3_KamarMandi", MechanicsManager.Instance.isScene3Loaded);
                await Task.Delay(2000);
                PlayerMovement.Instance.transform.position = new Vector3(-580f, 0f, 0f);
                break;

            case MechanicName.pourWater:
                OpenMechanics(ref mechanicsManager.isPourWaterOpened);
                ObjectiveManager.Instance.SetObjective(true, true, true, true);
                break;

            case MechanicName.bathingBaby:
                OpenMechanics(ref mechanicsManager.isBathingBabyOpened);
                break;

            case MechanicName.finalPhoto:
                OpenMechanics(ref mechanicsManager.isFinalPhotoOpened);
                break;

            case MechanicName.doorLivingRoom_fromKamarMandi:
                SaveSystem.SaveGame(4);
                TransitionManager.Instance.LoadAsyncScene("Act-1_Scene2_RuangTamu", MechanicsManager.Instance.isScene2Loaded);
                await Task.Delay(2000);
                PlayerMovement.Instance.transform.position = new Vector3(5520f, 0f, 0f);
                break;

            case MechanicName.repairSwing:
                OpenMechanics(ref mechanicsManager.isRepairSwingOpened);
                break;

            case MechanicName.photoMemoryAct1:
                mechanics.SetActive(true);
                mechanicsManager.isOpenMechanic = true;
                mechanicsManager.isCameraReady = true;
                break;

            case MechanicName.diaryBook:
                mechanics.SetActive(true);
                mechanicsManager.isOpenMechanic = true;
                mechanicsManager.isDiaryOpened = true;
                break;

            case MechanicName.none:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            if (!mechanicsManager.isOpenMechanic)
            {
                eToInteract.SetActive(true);
                eToInteract.GetComponent<FadeImage>().FadeIn(0.4f);
            }
            mechanicsManager.currentTriggerMechanic = mechanicName.ToString();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            eToInteract.GetComponent<FadeImage>().FadeOut(0.4f);
        }
        mechanicsManager.currentTriggerMechanic = MechanicName.none.ToString();
    }

    private void OpenMechanics(ref bool booleanMechanics)
    {
        eToInteract.SetActive(false);
        mechanics.SetActive(true);
        mechanicsManager.isOpenMechanic = true;
        booleanMechanics = true;
    }
}