using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadGamePanel : MonoBehaviour
{
    [Header("Load Game Buttons")]
    public Button[] loadGameButtons;

    private void Start()
    {
        // Setup semua button load game
        foreach (Button button in loadGameButtons)
        {
            button.onClick.AddListener(() => LoadGame());
        }
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("Act-1_Scene1_KamarIbu");
    }
} 