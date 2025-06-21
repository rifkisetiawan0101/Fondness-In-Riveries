using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[System.Serializable]
public class PlayerData
{
    public int saveVersion = 1;
    public string currentSceneName;
    public float[] playerPosition;
    public bool isCarryBaby;
    public bool isGameStart;
    public bool[] isDialoguePlayed;
    public bool[] isMechanicCompleted;
    public bool[] isObjectiveCompleted;

    public PlayerData(int version)
    {
        saveVersion = version;

        switch (version)
        {
            case 1:
                currentSceneName = "Act-1_Scene2_RuangTamu";
                playerPosition = new float[] { -5480, 0f, 0f };
                break;

            case 2:
                currentSceneName = "Act-1_Scene1_KamarIbu";
                playerPosition = new float[] { 2700f, 0f, 0f };
                break;

            case 3:
                currentSceneName = "Act-1_Scene3_KamarMandi";
                playerPosition = new float[] { -580, 0f, 0f };
                break;

            case 4:
                currentSceneName = "Act-1_Scene2_RuangTamu";
                playerPosition = new float[] { 5520, 0f, 0f };
                break;

            default:
                break;
        }

        isCarryBaby = false;
        isGameStart = true;

        // helper extension
        isDialoguePlayed = DialogueTrigger.Instance.allDialogueFlags.CopyAndReturn();
        isMechanicCompleted = MechanicsManager.Instance.allMechanicsFlags.CopyAndReturn();
        isObjectiveCompleted = ObjectiveManager.Instance.isObjectiveDone.CopyAndReturn();
    }

    public void ApplyData()
    {
        if (PlayerMovement.Instance == null ||
            DialogueTrigger.Instance == null ||
            MechanicsManager.Instance == null ||
            ObjectiveManager.Instance == null)
        {
            Debug.LogError("Cannot apply data - required instances are missing");
            return;
        }

        PlayerMovement.Instance.transform.position = new Vector3(
            playerPosition[0],
            playerPosition[1],
            playerPosition[2]
        );

        PlayerMovement.Instance.isCarryBaby = isCarryBaby;
        MechanicsManager.Instance.isGameStart = isGameStart;

        if (isDialoguePlayed != null && isDialoguePlayed.Length > 0)
            DialogueTrigger.Instance.ApplyDialogueFlags(isDialoguePlayed);

        if (isMechanicCompleted != null && isMechanicCompleted.Length > 0)
            MechanicsManager.Instance.ApplyMechanicsFlags(isMechanicCompleted);

        if (isObjectiveCompleted != null && isObjectiveCompleted.Length > 0)
            ObjectiveManager.Instance.isObjectiveDone = (bool[])isObjectiveCompleted.Clone();

        Debug.Log("PlayerData applied successfully");
    }

    public void SetPlayerPosition(PlayerMovement player)
    {
        if (File.Exists(SaveSystem.FilePath))
        {
            player.transform.position = new Vector3(playerPosition[0], playerPosition[1], playerPosition[2]);
        }
    }
}

public static class ArrayExtensions
{
    public static T[] CopyAndReturn<T>(this T[] source)
    {
        return (T[])source.Clone();
    }
}

