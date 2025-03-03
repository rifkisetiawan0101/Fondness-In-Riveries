using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicsManager : MonoBehaviour
{
    public static MechanicsManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // Menghindari duplikasi instance
            return;
        } else Instance = this;
        DontDestroyOnLoad(gameObject); // Jika perlu instance bertahan antar scene
    }

    public bool isGameStart;
    public bool isOpenMechanic;
    public string currentTriggerMechanic;

    public bool isSwingingBabyToSleepOpened;
    public bool isSwingComplete;
    public bool isPutBabySleep;
    public bool isSwingingBabyToSleepPlayed;
    public bool isCloseCurtainOpened;
    public bool isCloseCurtainPlayed;
    public bool isTurnOffLampOpened;
    public bool isTurnOffLampPlayed;
    public bool isInteractPhoto_1Opened;
    public bool isInteractPhoto_2Opened;
    public bool isInteractPhoto_3Opened;
    public bool isInteractPhoto_4Opened;
    public bool isInteractPhoto_1Played;
    public bool isInteractPhoto_2Played;
    public bool isInteractPhoto_3Played;
    public bool isInteractPhoto_4Played;
    public bool isCameraCollected;
    public bool isTDLCollected;
    public bool isTDLOpen;
    public bool isMakingMilkOpened;
    public bool isMakingMilkPlayed;
    public bool isGivingMilkOpened;
    public bool isGivingMilkDialoguePlayed;
    public bool isNowGivingMilk;
    public bool isGivingMilkPlayed;
    public bool isGivingMilkPlayed2;
    public bool isGivingMilkPlayed3;
    public bool isGetWaterOpened;
    public bool isGoingToGetWater;
    public bool isGetWaterPlayed;
    public bool isBoilWaterOpened;
    public bool isBoilWaterPlayed;
    public bool isPourWaterOpened;
    public bool isPourWaterPlayed;
    public bool isCarryingArrelToBath;
    public bool isCarryingArrelToBathPlayed;
    public bool isGetBackBaby;
    public bool isBathingBabyOpened;
    public bool isBathingBabyDialoguePlayed;
    public bool isBathingBabyPlayed;
    public bool isRepairSwingOpened;
    public bool isRepairSwingPlayed;
    public bool isCameraReady;
    public bool isCameraOpened;
    public bool isPhotoTaken;
    public bool isDiaryOpened;
    public bool isPhotoDragged;
}

public enum MechanicName {  // isi dengan mekanik lain
    swingingBabyToSleep, 
    closeCurtain,
    turnOffLamp,
    interactPhoto1,
    interactPhoto2,
    interactPhoto3,
    interactPhoto4,
    cameraPolaroid,
    toDoList,
    makingMilk,
    givingMilk,
    getWater,
    boilWater,
    pourWater,
    bathingBaby,
    repairSwing,
    photoMemoryAct1,
    diaryBook,
    doorLivingRoom,
    none
}