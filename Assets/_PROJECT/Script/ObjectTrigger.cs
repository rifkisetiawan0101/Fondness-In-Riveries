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
    private BoxCollider2D myCollider2D;
    
    [Header("Hover Sprite")]
    private SpriteRenderer hoverSprite;
    [SerializeField] private Sprite hoverBed;
    [SerializeField] private Sprite hoverCurtain;
    [SerializeField] private Sprite hoverSwitchLamp;
    [SerializeField] private Sprite hoverDoor;

    private void Start() 
    {
        mechanicsManager = FindObjectOfType<MechanicsManager>();
        myCollider2D = GetComponent<BoxCollider2D>();
        myCollider2D.enabled = false;

        hoverSprite = GetComponent<SpriteRenderer>();
        hoverSprite.sprite = null;
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
                if (!mechanicsManager.isSwingingBabyToSleepPlayed) { 
                    myCollider2D.enabled = true;
                    hoverSprite.sprite = hoverBed;
                } else { 
                    myCollider2D.enabled = false;
                    hoverSprite.sprite = null;
                }
                break;

            case MechanicName.closeCurtain:
                if (mechanicsManager.isSwingingBabyToSleepPlayed && !mechanicsManager.isCloseCurtainPlayed) { 
                    myCollider2D.enabled = true;
                    hoverSprite.sprite = hoverCurtain;
                } else { 
                    myCollider2D.enabled = false;
                    hoverSprite.sprite = null;
                }
                break;

            case MechanicName.turnOffLamp:
                if (mechanicsManager.isCloseCurtainPlayed && !mechanicsManager.isTurnOffLampPlayed) { 
                    myCollider2D.enabled = true;
                    hoverSprite.sprite = hoverSwitchLamp;
                } else {
                    myCollider2D.enabled = false;
                    hoverSprite.sprite = null;
                }
                break;

            case MechanicName.doorLivingRoom:
                if (mechanicsManager.isTurnOffLampPlayed) {
                    myCollider2D.enabled = true;
                    hoverSprite.sprite = hoverDoor;
                } else { 
                    myCollider2D.enabled = false;
                    hoverSprite.sprite = null;
                }
                break;

            case MechanicName.interactPhoto1:
            case MechanicName.interactPhoto2:
            case MechanicName.interactPhoto3:
            case MechanicName.interactPhoto4:
                if (mechanicsManager.isTurnOffLampPlayed)
                { myCollider2D.enabled = true; }
                break;

            case MechanicName.cameraPolaroid:
                if (mechanicsManager.isTurnOffLampPlayed && !mechanicsManager.isCameraCollected)
                { myCollider2D.enabled = true; } else { myCollider2D.enabled = false; }
                break;

            case MechanicName.toDoList:
                if (mechanicsManager.isCameraCollected && !mechanicsManager.isTDLCollected)
                { myCollider2D.enabled = true; } else { myCollider2D.enabled = false; }
                break;

            case MechanicName.makingMilk:
                if (mechanicsManager.isTDLCollected && !mechanicsManager.isMakingMilkPlayed)
                { myCollider2D.enabled = true; } else { myCollider2D.enabled = false; }
                break;

            case MechanicName.givingMilk:
                if (mechanicsManager.isMakingMilkPlayed && !mechanicsManager.isGivingMilkPlayed && !mechanicsManager.isCarryingArrelToBath)
                { myCollider2D.enabled = true; } 
                else if (mechanicsManager.isPourWaterPlayed && !mechanicsManager.isCarryingArrelToBath && !mechanicsManager.isGetBackBaby)
                { myCollider2D.enabled = true; } 
                else { myCollider2D.enabled = false; }
                break;

            case MechanicName.getWater:
                if (mechanicsManager.isGivingMilkPlayed && !mechanicsManager.isGetWaterPlayed)
                { myCollider2D.enabled = true; } else { myCollider2D.enabled = false; }
                break;

            case MechanicName.boilWater:
                if (mechanicsManager.isGetWaterPlayed && !mechanicsManager.isBoilWaterPlayed)
                { myCollider2D.enabled = true; } else { myCollider2D.enabled = false; }
                break;

            case MechanicName.pourWater:
                if (mechanicsManager.isBoilWaterPlayed && !mechanicsManager.isPourWaterPlayed)
                { myCollider2D.enabled = true; } else { myCollider2D.enabled = false; }
                break;

            case MechanicName.bathingBaby:
                if (!mechanicsManager.isBathingBabyPlayed && mechanicsManager.isCarryingArrelToBath)
                { myCollider2D.enabled = true; } else { myCollider2D.enabled = false; }
                break;

            case MechanicName.repairSwing:
                if (mechanicsManager.isBathingBabyPlayed && !mechanicsManager.isRepairSwingPlayed)
                { myCollider2D.enabled = true; } else { myCollider2D.enabled = false; }
                break;

            case MechanicName.photoMemoryAct1:
                if (mechanicsManager.isRepairSwingPlayed && !mechanicsManager.isPhotoTaken)
                { myCollider2D.enabled = true; } else { myCollider2D.enabled = false; }
                break;

            case MechanicName.diaryBook:
                if (mechanicsManager.isPhotoTaken && !mechanicsManager.isOpenMechanic && !mechanicsManager.isDiaryOpened)
                { myCollider2D.enabled = true; } else { myCollider2D.enabled = false; }
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
                SceneManager.LoadScene("Act-1 Ruang Tamu");
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
