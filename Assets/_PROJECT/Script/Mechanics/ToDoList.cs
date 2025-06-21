using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
using DIALOGUE;
using UnityEngine.SceneManagement;

public class ToDoList : MonoBehaviour
{
    public Button buttonOpenTDL;
    [SerializeField] private Image imgF;
    [SerializeField] private Sprite normalF;
    [SerializeField] private Sprite onPressF;
    public bool isTDLOpen;
    [SerializeField] private GameObject[] taskObjects;
    private LineRenderer[] strikeLines;
    private bool[] isStrikeAnimating;
    private bool[] hasStrikedOut; // Track if strike-through has been completed

    [SerializeField] private SpaceMechanic spaceMechanic;

    [Header("Line Settings")]
    [SerializeField] private float lineWidth = 2f; // Increased default width
    [SerializeField] private float strikeOutDuration = 1f; // Set to 1 second
    [SerializeField] private Color lineColor = Color.black;

    [Header("Obj Off")]
    [SerializeField] private GameObject objCamera;
    [SerializeField] private GameObject objTDL;
    [SerializeField] private GameObject objMilk;
    [SerializeField] private GameObject objHotWater;

    private void Start()
    {
        buttonOpenTDL.gameObject.SetActive(false);
        spaceMechanic = GetComponent<SpaceMechanic>();
        strikeLines = new LineRenderer[taskObjects.Length];
        isStrikeAnimating = new bool[taskObjects.Length];
        hasStrikedOut = new bool[taskObjects.Length];

        for (int i = 0; i < taskObjects.Length; i++)
        {
            // Create line object as child of task object
            GameObject lineObj = new GameObject($"StrikeLine_{i}");
            lineObj.transform.SetParent(taskObjects[i].transform, false); // Set worldPositionStays to false
            lineObj.transform.localPosition = new Vector3(0, 0, -1f);

            LineRenderer line = lineObj.AddComponent<LineRenderer>();
            line.useWorldSpace = false; // Use local space
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;
            line.material = new Material(Shader.Find("Sprites/Default")); // Use sprite shader
            line.startColor = lineColor;
            line.endColor = lineColor;
            line.positionCount = 2;

            // Set initial positions in local space
            RectTransform rectTransform = taskObjects[i].GetComponent<RectTransform>();
            float width = rectTransform.rect.width;
            line.SetPosition(0, new Vector3(-width / 2, 0, 0));
            line.SetPosition(1, new Vector3(-width / 2, 0, 0));

            strikeLines[i] = line;
        }
    }

    private bool lastTDLOpen = false;
    private void Update()
    {
        // Cek hanya saat TDL baru dibuka (dari false ke true)
        if (isTDLOpen && !lastTDLOpen)
        {
            CheckStrikeOut();
        }
        lastTDLOpen = isTDLOpen;

        if (Input.GetKeyUp(KeyCode.F))
        {
            OnClickCloseTDL();
        }

        if (Input.GetKey(KeyCode.F))
        {
            imgF.sprite = onPressF;
        }

        if (SceneManager.GetActiveScene().name == "Act-1_Scene2_RuangTamu")
        {
            if (MechanicsManager.Instance.isCameraCollected)
            {
                objCamera.SetActive(false);
            }

            if (MechanicsManager.Instance.isTDLCollected)
            {
                objTDL.SetActive(false);
            }

            if (MechanicsManager.Instance.isMakingMilkPlayed)
            {
                objMilk.SetActive(false);
            }

            if (MechanicsManager.Instance.isCollectHotWater)
            {
                objHotWater.SetActive(false);
            }
        }
    }

    private bool isProcessingInput = false;
    private async void OnClickOpenTDL()
    {
        if (isProcessingInput) return;
        if (MechanicsManager.Instance.isTDLCollected && !MechanicsManager.Instance.isOpenMechanic && !DialogueManager.instance.isRunningConversation)
        {
            isProcessingInput = true;

            StartCoroutine(spaceMechanic.OpenItem(0.7f));
            await Task.Delay(700);
            isTDLOpen = true;
            MechanicsManager.Instance.isTDLOpen = true;
            CheckStrikeOut();

            isProcessingInput = false;
        }
    }

    private async void OnClickCloseTDL()
    {
        if (isProcessingInput) return;
        if (MechanicsManager.Instance.isTDLCollected && isTDLOpen && !DialogueManager.instance.isRunningConversation)
        {
            isProcessingInput = true;
            imgF.sprite = normalF;
            await Task.Delay(100);
            StartCoroutine(spaceMechanic.CloseItem(0.7f));
            await Task.Delay(700);
            isTDLOpen = false;
            MechanicsManager.Instance.isTDLOpen = false;

            isProcessingInput = false;
        }
    }

    private void CheckStrikeOut()
    {
        if (MechanicsManager.Instance.isMakingMilkPlayed && !hasStrikedOut[0] && !isStrikeAnimating[0])
        {
            StartCoroutine(StrikeOut(0));
        }
        if (MechanicsManager.Instance.isGivingMilkPlayed && !hasStrikedOut[1] && !isStrikeAnimating[1])
        {
            StartCoroutine(StrikeOut(1));
        }
        if (MechanicsManager.Instance.isPourWaterPlayed && !hasStrikedOut[2] && !isStrikeAnimating[2])
        {
            StartCoroutine(StrikeOut(2));
        }
    }

    private IEnumerator StrikeOut(int taskIndex)
    {
        if (taskIndex >= taskObjects.Length) yield break;

        isStrikeAnimating[taskIndex] = true;
        LineRenderer line = strikeLines[taskIndex];

        // Get local space positions
        RectTransform rectTransform = taskObjects[taskIndex].GetComponent<RectTransform>();
        float width = rectTransform.rect.width;
        Vector3 startPos = new Vector3(-width / 2, 0, 0);
        Vector3 endPos = new Vector3(width / 2, 0, 0);

        // Set initial positions
        line.SetPosition(0, startPos);
        line.SetPosition(1, startPos);

        float elapsedTime = 0f;

        while (elapsedTime < strikeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / strikeOutDuration;

            Vector3 currentEndPos = Vector3.Lerp(startPos, endPos, t);
            line.SetPosition(1, currentEndPos);

            yield return null;
        }

        // Ensure final position is exact
        line.SetPosition(1, endPos);
        isStrikeAnimating[taskIndex] = false;
        hasStrikedOut[taskIndex] = true; // Mark as completed
    }

    [Header("Button States")]
    [SerializeField] private Sprite normalOpen;
    [SerializeField] private Sprite hoverOpen;
    [SerializeField] private Sprite onPressOpen;
    [SerializeField] private Image buttonOpenTDLImg;

    public void OpenPointerDown(BaseEventData eventData)
    {
        buttonOpenTDLImg.sprite = onPressOpen;
    }

    public void OpenPointerUp(BaseEventData eventData)
    {
        buttonOpenTDLImg.sprite = normalOpen;
        OnClickOpenTDL();
    }

    public void OpenPointerEnter(BaseEventData eventData)
    {
        buttonOpenTDLImg.sprite = hoverOpen;
    }

    public void OpenPointerExit(BaseEventData eventData)
    {
        buttonOpenTDLImg.sprite = normalOpen;
    }

    [SerializeField] private Sprite normalClose;
    [SerializeField] private Sprite hoverClose;
    [SerializeField] private Sprite onPressClose;
    [SerializeField] private Image buttonCloseTDLImg;

    public void ClosePointerDown(BaseEventData eventData)
    {
        buttonCloseTDLImg.sprite = onPressClose;
    }

    public void ClosePointerUp(BaseEventData eventData)
    {
        buttonCloseTDLImg.sprite = normalClose;
        OnClickCloseTDL();
    }

    public void ClosePointerEnter(BaseEventData eventData)
    {
        buttonCloseTDLImg.sprite = hoverClose;
    }

    public void ClosePointerExit(BaseEventData eventData)
    {
        buttonCloseTDLImg.sprite = normalClose;
    }
}