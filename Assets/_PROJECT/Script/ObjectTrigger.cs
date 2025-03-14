using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectTrigger : MonoBehaviour
{
    public MechanicName mechanicName;
    [SerializeField] private MechanicsManager mechanicsManager;
    [SerializeField] private GameObject eToInteract;
    [SerializeField] private GameObject mechanics;
    [SerializeField] private bool playerInTrigger = false;
    private BoxCollider2D objCollider2D;
    private Animator objAnimator;
    
    [Header("Hover Sprite")]
    private SpriteRenderer objHoverSprite;

    private void Start() 
    {
        mechanicsManager = FindObjectOfType<MechanicsManager>();
        objCollider2D = GetComponent<BoxCollider2D>();
        objCollider2D.enabled = false;
        objAnimator = GetComponent<Animator>();
        if (objAnimator != null)
        { objAnimator.enabled = false; }

        objHoverSprite = GetComponent<SpriteRenderer>();
        objHoverSprite.enabled = false;
    }

    private void Update() 
    {
        EnableMechanic();

        if (playerInTrigger && !PlayerMovement.Instance.isMakMoving && !mechanicsManager.isOpenMechanic && Input.GetKeyDown(KeyCode.E))
        {
            RunMechanic();
        }

        if (mechanicName == MechanicName.photoMemoryAct1 && playerInTrigger && !DialogueManager.instance.isRunningConversation && Input.GetKeyDown(KeyCode.E))
        {
            RunMechanic();
        }
    }

    private void EnableMechanic()
    {
        switch(mechanicName)
        {
            case MechanicName.swingingBabyToSleep:
                if (!mechanicsManager.isSwingingBabyToSleepPlayed && !DialogueManager.instance.isRunningConversation) 
                { objCollider2D.enabled = true; } else { objCollider2D.enabled = false; objAnimator.enabled = false; }
                break;

            case MechanicName.closeCurtain:
                if (mechanicsManager.isSwingingBabyToSleepPlayed && !mechanicsManager.isCloseCurtainPlayed) 
                { objCollider2D.enabled = true; } else { objCollider2D.enabled = false; objAnimator.enabled = false; }
                break;

            case MechanicName.turnOffLamp:
                if (mechanicsManager.isCloseCurtainPlayed && !mechanicsManager.isTurnOffLampPlayed) 
                { objCollider2D.enabled = true; } else { objCollider2D.enabled = false; objAnimator.enabled = false; }
                break;

            case MechanicName.doorLivingRoom:
                if (mechanicsManager.isTurnOffLampPlayed) 
                { objCollider2D.enabled = true; } else { objCollider2D.enabled = false; objAnimator.enabled = false; }
                break;

            case MechanicName.interactPhoto1:
            case MechanicName.interactPhoto2:
            case MechanicName.interactPhoto3:
            case MechanicName.interactPhoto4:
                if (mechanicsManager.isTurnOffLampPlayed)
                { objCollider2D.enabled = true; }
                break;

            case MechanicName.cameraPolaroid:
                if (mechanicsManager.isTurnOffLampPlayed && !mechanicsManager.isCameraCollected)
                { objCollider2D.enabled = true; } else { objCollider2D.enabled = false; }
                break;

            case MechanicName.toDoList:
                if (mechanicsManager.isCameraCollected && !mechanicsManager.isTDLCollected)
                { objCollider2D.enabled = true; } else { objCollider2D.enabled = false; }
                break;

            case MechanicName.makingMilk:
                if (mechanicsManager.isTDLCollected && !mechanicsManager.isMakingMilkPlayed)
                { objCollider2D.enabled = true; } else { objCollider2D.enabled = false; }
                break;

            case MechanicName.givingMilk:
                if (mechanicsManager.isMakingMilkPlayed && !mechanicsManager.isGivingMilkPlayed && !mechanicsManager.isCarryingArrelToBath)
                { objCollider2D.enabled = true; } 
                else if (mechanicsManager.isPourWaterPlayed && !mechanicsManager.isCarryingArrelToBath && !mechanicsManager.isGetBackBaby)
                { objCollider2D.enabled = true; } 
                else { objCollider2D.enabled = false; }
                break;

            case MechanicName.getWater:
                if (mechanicsManager.isGivingMilkPlayed && !mechanicsManager.isGetWaterPlayed)
                { objCollider2D.enabled = true; } else { objCollider2D.enabled = false; }
                break;

            case MechanicName.boilWater:
                if (mechanicsManager.isGetWaterPlayed && !mechanicsManager.isBoilWaterPlayed)
                { objCollider2D.enabled = true; } else { objCollider2D.enabled = false; }
                break;

            case MechanicName.pourWater:
                if (mechanicsManager.isBoilWaterPlayed && !mechanicsManager.isPourWaterPlayed)
                { objCollider2D.enabled = true; } else { objCollider2D.enabled = false; }
                break;

            case MechanicName.bathingBaby:
                if (!mechanicsManager.isBathingBabyPlayed && mechanicsManager.isCarryingArrelToBath)
                { objCollider2D.enabled = true; } else { objCollider2D.enabled = false; }
                break;

            case MechanicName.repairSwing:
                if (mechanicsManager.isBathingBabyPlayed && !mechanicsManager.isRepairSwingPlayed)
                { objCollider2D.enabled = true; } else { objCollider2D.enabled = false; }
                break;

            case MechanicName.photoMemoryAct1:
                if (mechanicsManager.isRepairSwingPlayed && !mechanicsManager.isPhotoTaken)
                { objCollider2D.enabled = true; } else { objCollider2D.enabled = false; }
                break;

            case MechanicName.diaryBook:
                if (mechanicsManager.isPhotoTaken && !mechanicsManager.isOpenMechanic && !mechanicsManager.isDiaryOpened)
                { objCollider2D.enabled = true; } else { objCollider2D.enabled = false; }
                break;
        }
    }

    private void RunMechanic()
    {
        switch(mechanicName)
        {
            case MechanicName.swingingBabyToSleep:
                OpenMechanics(ref mechanicsManager.isSwingingBabyToSleepOpened);
                break;

            case MechanicName.closeCurtain:
                OpenMechanics(ref mechanicsManager.isCloseCurtainOpened);
                break;

            case MechanicName.turnOffLamp:
                OpenMechanics(ref mechanicsManager.isTurnOffLampOpened);
                break;

            case MechanicName.doorLivingRoom:
                SceneManager.LoadScene("Act-1_Scene2_RuangTamu");
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

            case MechanicName.cameraPolaroid:
                eToInteract.SetActive(false);
                mechanicsManager.isCameraCollected = true;
                gameObject.SetActive(false);
                break;

            case MechanicName.toDoList:
                eToInteract.SetActive(false);
                mechanicsManager.isOpenMechanic = false;
                mechanicsManager.isTDLCollected = true;
                gameObject.SetActive(false);
                break;

            case MechanicName.makingMilk:
                OpenMechanics(ref mechanicsManager.isMakingMilkOpened);
                break;

            case MechanicName.givingMilk:
                if (mechanicsManager.isMakingMilkPlayed && !mechanicsManager.isGivingMilkPlayed && !mechanicsManager.isCarryingArrelToBath)
                {
                    OpenMechanics(ref mechanicsManager.isGivingMilkOpened);
                }
                else if (mechanicsManager.isPourWaterPlayed && !mechanicsManager.isCarryingArrelToBath && !mechanicsManager.isGetBackBaby)
                {
                    OpenMechanics(ref mechanicsManager.isGetWaterOpened);
                }
                break;

            case MechanicName.getWater:
                OpenMechanics(ref mechanicsManager.isGetWaterOpened);
                break;

            case MechanicName.boilWater:
                OpenMechanics(ref mechanicsManager.isBoilWaterOpened);
                break;

            case MechanicName.pourWater:
                OpenMechanics(ref mechanicsManager.isPourWaterOpened);
                break;

            case MechanicName.bathingBaby:
                OpenMechanics(ref mechanicsManager.isBathingBabyOpened);
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
                objHoverSprite.enabled = true;
                objAnimator.enabled = true;
                eToInteract.SetActive(true);
            }
            mechanicsManager.currentTriggerMechanic = mechanicName.ToString();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            objHoverSprite.enabled = false;
            objAnimator.enabled = false;
            objAnimator.Rebind();
            eToInteract.SetActive(false);
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
