using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }
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

    [SerializeField] private GameObject notifMoveSide;
    [SerializeField] private GameObject notifCameraCollected;
    [SerializeField] private GameObject notifTDLCollected;
    [SerializeField] private GameObject notifFOpenTDL;
    [SerializeField] private GameObject notifEReadyCamera;
    [SerializeField] private GameObject notifEOpenDiary;

    private void Update()
    {
        if (notifCameraCollected == null)
        {
            var mode = SceneManager.GetActiveScene().buildIndex > 0 ? LoadSceneMode.Additive : LoadSceneMode.Single;
            OnSceneLoaded(SceneManager.GetActiveScene(), mode);
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject mainCanvas = GameObject.Find("Canvas - Main");
        GameObject worldCanvas = GameObject.Find("Canvas - World");
        RectTransform UIGroup = mainCanvas.transform.Find("[9] - UI") as RectTransform;

        notifMoveSide = worldCanvas.transform.Find("NotifMoveSide").gameObject;
        
        notifCameraCollected = UIGroup.transform.Find("NotifCameraCollected").gameObject;
        notifTDLCollected = UIGroup.transform.Find("NotifTDLCollected").gameObject;
        notifFOpenTDL = UIGroup.transform.Find("Notif F OpenTDL").gameObject;
        notifEReadyCamera = UIGroup.transform.Find("Notif E ReadyCamera").gameObject;
        notifEOpenDiary = UIGroup.transform.Find("Notif E OpenDiary").gameObject;
    }

    private IEnumerator FadeInNotif(GameObject notif, float duration)
    {
        notif.SetActive(true);
        Image notifImage = notif.GetComponent<Image>();
        if (notifImage != null)
        {
            Color notifColor = notifImage.color;
            notifColor.a = 0;
            notifImage.color = notifColor;

            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Clamp01(elapsedTime / duration);
                notifColor.a = alpha;
                notifImage.color = notifColor;
                yield return null;
            }

            notifColor.a = 1;
            notifImage.color = notifColor;
        }
    }

    // Fungsi untuk menurunkan transparansi notif
    private IEnumerator FadeOutNotif(GameObject notif, float duration)
    {
        Image notifImage = notif.GetComponent<Image>();
        if (notifImage != null)
        {
            Color notifColor = notifImage.color;
            notifColor.a = 1;
            notifImage.color = notifColor;

            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Clamp01(1 - (elapsedTime / duration));
                notifColor.a = alpha;
                notifImage.color = notifColor;
                yield return null;
            }

            notifColor.a = 0;
            notifImage.color = notifColor;
        }
        notif.SetActive(false);
    }

    public IEnumerator NotifMoveSide()
    {
        yield return StartCoroutine(FadeInNotif(notifMoveSide, 0.4f));
        yield return new WaitUntil (() => PlayerMovement.Instance.isMakMoving);
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(FadeOutNotif(notifMoveSide, 0.4f));
        StopCoroutine(NotifMoveSide());
    }

    public IEnumerator NotifCameraCollected()
    {
        yield return StartCoroutine(FadeInNotif(notifCameraCollected, 0.4f));
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(FadeOutNotif(notifCameraCollected, 0.4f));
        StopCoroutine(NotifCameraCollected());
    }

    public IEnumerator NotifTDLCollected()
    {
        yield return StartCoroutine(FadeInNotif(notifTDLCollected, 0.4f));
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(FadeOutNotif(notifTDLCollected, 0.4f));
        StopCoroutine(NotifTDLCollected());
    }

    public IEnumerator NotifFOpenTDL()
    {
        yield return StartCoroutine(FadeInNotif(notifFOpenTDL, 0.4f));
        yield return new WaitUntil (() => MechanicsManager.Instance.isTDLOpen);
        yield return StartCoroutine(FadeOutNotif(notifFOpenTDL, 0.4f));
        StopCoroutine(NotifFOpenTDL());
    }

    public IEnumerator NotifEReadyCamera()
    {
        yield return StartCoroutine(FadeInNotif(notifEReadyCamera, 0.4f));
        yield return new WaitUntil (() => MechanicsManager.Instance.isCameraReady);
        yield return StartCoroutine(FadeOutNotif(notifEReadyCamera, 0.4f));
        StopCoroutine(NotifEReadyCamera());
    }

    public IEnumerator NotifEOpenDiary()
    {
        yield return StartCoroutine(FadeInNotif(notifEOpenDiary, 0.4f));
        yield return new WaitUntil (() => MechanicsManager.Instance.isDiaryOpened);
        yield return StartCoroutine(FadeOutNotif(notifEOpenDiary, 0.4f));
        StopCoroutine(NotifEOpenDiary());
    }
}
