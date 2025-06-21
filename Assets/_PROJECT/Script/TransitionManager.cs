using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // Menghindari duplikasi instance
            return;
        } else Instance = this;
        DontDestroyOnLoad(gameObject); // Jika perlu instance bertahan antar scene
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    [Header ("---Loading Screen---")]
    [SerializeField] private GameObject cameraMain;
    [SerializeField] private GameObject eventSystem;
    [SerializeField] private GameObject canvasOverlay;
    [SerializeField] private GameObject loadingScreen;

    [Header ("---Progress Bar---")]
    // [SerializeField] private Image bgProgressBar;
    // [SerializeField] private Image progressBar;
    // [SerializeField] private float progress;
    // private float targetProgress;
    // private float currentProgress;
    // [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private GameObject loadingIcon;

    [Header("Scene References")]
    [SerializeField] private Scene previousScene;
    [SerializeField] private Scene targetScene;
    public async void LoadAsyncScene(string sceneName, bool isSceneLoaded)
    {
        previousScene = SceneManager.GetActiveScene();
        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<FadeImage>().FadeInCanvasGroup(0.5f);
        // loadingIcon.GetComponent<FadeImage>().FadeIn(1f);
        // bgProgressBar.GetComponent<FadeImage>().FadeIn(1f);
        // progressBar.GetComponent<FadeImage>().FadeIn(1f);
        // progressBar.fillAmount = 0f;
        // currentProgress = 0f;
        // targetProgress = 0f;
        isSceneLoaded = true;

        await Task.Delay(500);

        var asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            // progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // Normalize progress
            // targetProgress = progress;
            loadingIcon.transform.Rotate(0f, 0f, -60f);
            await Task.Delay(100);
        }

        // targetProgress = 1f;
        asyncLoad.allowSceneActivation = true;
        await asyncLoad;

        targetScene = SceneManager.GetSceneByName(sceneName);

        if (targetScene.isLoaded)
        {
            await SceneManager.UnloadSceneAsync(previousScene);
            await Task.Delay(500);
            SceneManager.SetActiveScene(targetScene);
            await Task.Delay(1000);
            loadingScreen.GetComponent<FadeImage>().FadeOutCanvasGroup(0.5f);
            // loadingIcon.GetComponent<FadeImage>().FadeOut(1f);
            // bgProgressBar.GetComponent<FadeImage>().FadeOut(1f);
            // progressBar.GetComponent<FadeImage>().FadeOut(1f);
            await Task.Delay(500);
            loadingScreen.SetActive(false);
        }
        else if (!targetScene.isLoaded)
        {
            Debug.LogError($"Failed to load target scene: {sceneName}");
            return;
        }
    }

    void Update()
    {
        if (cameraMain == null || canvasOverlay == null)
        {
            var mode = SceneManager.GetActiveScene().buildIndex > 0 ? LoadSceneMode.Additive : LoadSceneMode.Single;
            OnSceneLoaded(SceneManager.GetActiveScene(), mode);
        }

        // if (progressBar != null) {
        //     currentProgress = Mathf.Lerp(currentProgress, targetProgress, Time.deltaTime * smoothSpeed);
        //     progressBar.fillAmount = currentProgress;
        // }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        cameraMain = GameObject.Find("Camera - Main");
        eventSystem = GameObject.Find("EventSystem");

        canvasOverlay = GameObject.Find("Canvas - Overlay");
        loadingScreen = canvasOverlay.transform.Find("LoadingScreen").gameObject;
        loadingIcon = loadingScreen.transform.Find("LoadingIcon").gameObject;
        // GameObject progressBarObject = loadingScreen.transform.Find("ProgressBar").gameObject;
        // progressBar = progressBarObject.GetComponent<Image>();
        // GameObject bgProgressBarObject = loadingScreen.transform.Find("BgProgress").gameObject;
        // bgProgressBar = bgProgressBarObject.GetComponent<Image>();
    }
}
