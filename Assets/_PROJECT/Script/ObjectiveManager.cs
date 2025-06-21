using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else Instance = this;
        DontDestroyOnLoad(gameObject);
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    [SerializeField] private GameObject objectiveContainer;
    [SerializeField] private GameObject objective;
    [SerializeField] private TextMeshProUGUI objectiveText;
    [SerializeField] private string[] objectiveContent;
    public bool[] isObjectiveDone;

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
        if(SceneManager.GetActiveScene().name == "Main Menu") { return; }
        GameObject mainCanvas = GameObject.Find("Canvas - Main");
        RectTransform objectivesGroup = mainCanvas.transform.Find("[5] - Objectives") as RectTransform;

        objectiveContainer = objectivesGroup.transform.Find("ObjectiveContainer").gameObject;
        objective = objectiveContainer.transform.Find("Objective").gameObject;
        GameObject objectiveTextObject = objectiveContainer.transform.Find("ObjectiveText").gameObject;
        objectiveText = objectiveTextObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (objectiveContainer == null)
        {
            var mode = SceneManager.GetActiveScene().buildIndex > 0 ? LoadSceneMode.Additive : LoadSceneMode.Single;
            OnSceneLoaded(SceneManager.GetActiveScene(), mode);
        }

        _ = OpenCloseObjectice();
    }

    private bool activeOnce;

    private async Task OpenCloseObjectice()
    {
        if (!MechanicsManager.Instance.isOpenMechanic && objectiveContainer != null)
        {
            activeOnce = false;
            if (isObjectiveDone[0] == true)
            {
                objectiveContainer.SetActive(true);
                objectiveContainer.GetComponent<FadeImage>().FadeInCanvasGroup(0.4f);
            }
        }
        else if (MechanicsManager.Instance.isOpenMechanic && !activeOnce)
        {
            activeOnce = true;
            objectiveContainer.GetComponent<FadeImage>().FadeOutCanvasGroup(0.4f);
            await Task.Delay(400);
            objectiveContainer.SetActive(false);
        }
    }

    public void SetObjective(bool isDone0, bool isDone1, bool isDone2, bool isDone3)
    {
        isObjectiveDone[0] = isDone0;
        isObjectiveDone[1] = isDone1;
        isObjectiveDone[2] = isDone2;
        isObjectiveDone[3] = isDone3;

        if (isObjectiveDone[0] && !isObjectiveDone[1] && !isObjectiveDone[2] && !isObjectiveDone[3])
        {
            objectiveText.text = objectiveContent[0];
        }
        else if (isObjectiveDone[0] && isObjectiveDone[1] && !isObjectiveDone[2] && !isObjectiveDone[3])
        {
            objectiveText.text = objectiveContent[1];
        }
        else if (isObjectiveDone[0] && isObjectiveDone[1] && isObjectiveDone[2] && !isObjectiveDone[3])
        {
            objectiveText.text = objectiveContent[2];
        }
        else if (isObjectiveDone[0] && isObjectiveDone[1] && isObjectiveDone[2] && isObjectiveDone[3])
        {
            objectiveText.text = objectiveContent[3];
        }
    }
}
