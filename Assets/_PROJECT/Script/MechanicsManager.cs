using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MechanicsManager : MonoBehaviour
{
    public static MechanicsManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // Menghindari duplikasi instance
            return;
        }
        else Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [Header("---General---")]
    public bool isGameStart;
    public bool isOpenMechanic;
    public string currentTriggerMechanic;
    public bool isScene1Loaded;
    public bool isScene2Loaded;
    public bool isScene3Loaded;

    [Header("---Mechanic Scene 1---")]
    public bool isSwingingBabyToSleepOpened;
    public bool isSwingComplete;
    public bool isPutBabySleep;
    public bool isBabyPanelOpened1;
    public bool isBabyPanelOpened2;
    public bool isSwingingBabyToSleepPlayed;
    public bool isCloseCurtainOpened;
    public bool isCloseCurtainPlayed;
    public bool isTurnOffLampOpened;
    public bool isTurnOffLampPlayed;
    public bool isInteractPhoto_1Opened;
    public bool isInteractPhoto_2Opened;
    public bool isInteractPhoto_3Opened;
    public bool isInteractPhoto_4Opened;

    [Header("---Mechanic Scene 2---")]
    public bool isCameraCollected;
    public bool isGrandmaDoorOpened;
    public bool isTDLCollected;
    public bool isTDLOpen;
    public bool isMakingMilkOpened;
    public bool isMakingMilkPlayed;
    public bool isGivingMilkOpened;
    public bool isGivingMilkPlayed;
    // --- crashing
    public bool isGivingMilkPlayed2;
    public bool isGivingMilkPlayed3;
    public bool isCollectHotWater; // crashing
    public bool isGetWaterOpened;
    public bool isGoingToGetWater;
    public bool isGetWaterPlayed;
    public bool isBoilWaterOpened;
    public bool isBoilWaterPlayed;
    // crashing ---
    public bool isPourWaterOpened;
    public bool isPourWaterPlayed;
    public bool isCarryingArrelToBath;
    public bool isBathingBabyOpened;
    public bool isBathingBabyDialoguePlayed;
    public bool isBathingBabyPlayed;
    // --- crashing
    public bool isRepairSwingOpened;
    public bool isRepairSwingPlayed;
    public bool isCameraReady;
    public bool isCameraOpened;
    public bool isPhotoTaken;
    public bool isDiaryOpened;
    public bool isPhotoDragged;
    // crashing ---
    public bool isFinalPhotoOpened;
    public bool isFinalPhotoPlayed;

    [Header("---All Mechanics Flags---")]
    public bool[] allMechanicsFlags;

    public void UpdateMechanicsFlags()
    {
        allMechanicsFlags = new bool[]
        {
            isSwingingBabyToSleepOpened,
            isSwingComplete,
            isPutBabySleep,
            isSwingingBabyToSleepPlayed,
            isBabyPanelOpened1,
            isBabyPanelOpened2,
            isCloseCurtainOpened,
            isCloseCurtainPlayed,
            isTurnOffLampOpened,
            isTurnOffLampPlayed,
            isInteractPhoto_1Opened,
            isInteractPhoto_2Opened,
            isInteractPhoto_3Opened,
            isInteractPhoto_4Opened,

            // Scene 2 Mechanics
            isCameraCollected,
            isGrandmaDoorOpened,
            isTDLCollected,
            isTDLOpen,
            isMakingMilkOpened,
            isMakingMilkPlayed,
            isGivingMilkOpened,
            isGivingMilkPlayed,
            isGivingMilkPlayed2,
            isGivingMilkPlayed3,
            isCollectHotWater,
            isGetWaterOpened,
            isGoingToGetWater,
            isGetWaterPlayed,
            isBoilWaterOpened,
            isBoilWaterPlayed,
            isPourWaterOpened,
            isPourWaterPlayed,
            isCarryingArrelToBath,
            isBathingBabyOpened,
            isBathingBabyDialoguePlayed,
            isBathingBabyPlayed,
            isRepairSwingOpened,
            isRepairSwingPlayed,
            isCameraReady,
            isCameraOpened,
            isPhotoTaken,
            isDiaryOpened,
            isPhotoDragged
        };
    }

    public void ApplyMechanicsFlags(bool[] flags)
    {
        // if (flags == null || flags.Length != allMechanicsFlags.Length) return;
        allMechanicsFlags = (bool[])flags.Clone();

        isSwingingBabyToSleepOpened = allMechanicsFlags[0];
        isSwingComplete = allMechanicsFlags[1];
        isPutBabySleep = allMechanicsFlags[2];
        isSwingingBabyToSleepPlayed = allMechanicsFlags[3];
        isBabyPanelOpened1 = allMechanicsFlags[4];
        isBabyPanelOpened2 = allMechanicsFlags[5];
        isCloseCurtainOpened = allMechanicsFlags[6];
        isCloseCurtainPlayed = allMechanicsFlags[7];
        isTurnOffLampOpened = allMechanicsFlags[8];
        isTurnOffLampPlayed = allMechanicsFlags[9];
        isInteractPhoto_1Opened = allMechanicsFlags[10];
        isInteractPhoto_2Opened = allMechanicsFlags[11];
        isInteractPhoto_3Opened = allMechanicsFlags[12];
        isInteractPhoto_4Opened = allMechanicsFlags[13];

        // Scene 2 Mechanics
        isCameraCollected = allMechanicsFlags[14];
        isGrandmaDoorOpened = allMechanicsFlags[15];
        isTDLCollected = allMechanicsFlags[16];
        isTDLOpen = allMechanicsFlags[17];
        isMakingMilkOpened = allMechanicsFlags[18];
        isMakingMilkPlayed = allMechanicsFlags[19];
        isGivingMilkOpened = allMechanicsFlags[20];
        isGivingMilkPlayed = allMechanicsFlags[21];
        isGivingMilkPlayed2 = allMechanicsFlags[22];
        isGivingMilkPlayed3 = allMechanicsFlags[23];
        isCollectHotWater = allMechanicsFlags[24];
        isGetWaterOpened = allMechanicsFlags[25];
        isGoingToGetWater = allMechanicsFlags[26];
        isGetWaterPlayed = allMechanicsFlags[27];
        isBoilWaterOpened = allMechanicsFlags[28];
        isBoilWaterPlayed = allMechanicsFlags[29];
        isPourWaterOpened = allMechanicsFlags[30];
        isPourWaterPlayed = allMechanicsFlags[31];
        isCarryingArrelToBath = allMechanicsFlags[32];
        isBathingBabyOpened = allMechanicsFlags[33];
        isBathingBabyDialoguePlayed = allMechanicsFlags[34];
        isBathingBabyPlayed = allMechanicsFlags[35];
        isRepairSwingOpened = allMechanicsFlags[36];
        isRepairSwingPlayed = allMechanicsFlags[37];
        isCameraReady = allMechanicsFlags[38];
        isCameraOpened = allMechanicsFlags[39];
        isPhotoTaken = allMechanicsFlags[40];
        isDiaryOpened = allMechanicsFlags[41];
        isPhotoDragged = allMechanicsFlags[42];
    }
}

public enum MechanicName
{  // isi dengan mekanik lain
    swingingBabyToSleep,
    closeCurtain,
    turnOffLamp,
    interactPhoto1,
    interactPhoto2,
    interactPhoto3,
    interactPhoto4,
    cameraPolaroid,
    grandmaDoor,
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
    doorLivingRoom_fromMaRoom,
    doorLivingRoom_fromKamarMandi,
    doorMaRoom,
    doorKamarMandi,
    none,

    // crashing
    hotWater,
    finalPhoto
}