using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public static class SaveSystem
{
    public static readonly string FilePath = Path.Combine(
        Application.persistentDataPath, "game-save.json");

    /// <summary>
    /// Simpan snapshot current state ke disk.
    /// </summary>
    public static bool SaveGame(int saveVersion)
    {
        try
        {
            Debug.Log("▶️ [SaveGame] inside TRY block");

            DialogueTrigger.Instance.UpdateDialogueFlags();
            MechanicsManager.Instance.UpdateMechanicsFlags();

            var data = new PlayerData(saveVersion);

            string json = JsonUtility.ToJson(data, true);
            Debug.Log($"▶️ [SaveGame] JSON size: {json.Length}");

            // optional: hitung checksum lalu simpan di data.fileChecksum

            File.WriteAllText(FilePath, json); // write all at once
            Debug.Log($"Game saved to {FilePath}");
            return true;
        }
        catch (IOException ex)
        {
            Debug.LogError($"I/O error saving game: {ex.Message}");
            return false;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Unexpected error saving game: {ex}");
            return false;
        }
    }

    /// <summary>
    /// Load data dan kembalikan PlayerData-nya tanpa langsung apply.
    /// </summary>
    public static PlayerData LoadGameData()
    {
        if (!File.Exists(FilePath))
        {
            Debug.LogWarning("Save file not found.");
            return null;
        }
        try
        {
            string json = File.ReadAllText(FilePath);
            var data = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Save file loaded.");
            return data;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error loading save file: {ex}");
            return null;
        }
    }

    /// <summary>
    /// Untuk Continue Game: load data, load scene sesuai data.currentSceneName, lalu apply data.
    /// </summary>
    public static async void ContinueGame(GameObject popUp)
    {
        var data = LoadGameData();
        if (data == null)
        {
            popUp.SetActive(true);
            popUp.GetComponent<FadeImage>().FadeIn(0.7f);
            await Task.Delay(2000);
            popUp.GetComponent<FadeImage>().FadeOut(0.7f);
            await Task.Delay(700);
            popUp.SetActive(false);

            Debug.LogWarning("No valid save data found.");
            return;
        }

        // Load scene yang terakhir disimpan
        TransitionManager.Instance.LoadAsyncScene(
            data.currentSceneName,
            MechanicsManager.Instance.isScene1Loaded
        );

        await Task.Delay(500);

        float timeout = 10f;
        float timer = 0f;
        
        while (timer < timeout)
        {
            if (PlayerMovement.Instance != null && 
                DialogueTrigger.Instance != null && 
                MechanicsManager.Instance != null &&
                ObjectiveManager.Instance != null)
            {
                break;
            }
            
            await Task.Delay(100);
            timer += 0.1f;
        }

        if (PlayerMovement.Instance == null || 
            DialogueTrigger.Instance == null || 
            MechanicsManager.Instance == null ||
            ObjectiveManager.Instance == null)
        {
            Debug.LogError("Failed to find required components after scene load.");
            return;
        }

        await Task.Delay(100);
        DialogueTrigger.Instance.UpdateDialogueFlags();
        MechanicsManager.Instance.UpdateMechanicsFlags(); 
        data.ApplyData();
    }

    /// <summary>
    /// Untuk New Game: buang file save lama (jika ada), lalu load scene awal tanpa apply data.
    /// </summary>
    public static void NewGame()
    {
        // Hapus file lama
        if (File.Exists(FilePath))
        {
            File.Delete(FilePath);
            Debug.Log("Existing save deleted, starting new game.");
        }

        // Langsung load scene baru tanpa apply
        TransitionManager.Instance.LoadAsyncScene(
            "Act-1_Scene1_KamarIbu",
            MechanicsManager.Instance.isScene1Loaded
        );
    }
}
