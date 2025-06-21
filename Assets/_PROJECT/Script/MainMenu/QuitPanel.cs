using UnityEngine;
using UnityEngine.UI;

public class QuitPanel : MonoBehaviour
{
    [Header("Quit Button")]
    public Button confirmQuitButton;

    private void Start()
    {
        confirmQuitButton.onClick.AddListener(OnConfirmQuitClicked);
    }

    private void OnConfirmQuitClicked()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
} 