using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DIALOGUE;

public class RepairSwing : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject fabricLeft;
    [SerializeField] private GameObject fabricRight;
    [SerializeField] private GameObject fabricCenter;
    [SerializeField] private GameObject panelBaby;
    
    [SerializeField] private bool isFabricLeftDragged;
    [SerializeField] private bool isFabricRightDragged;
    [SerializeField] private bool isFabricLeftDone;
    [SerializeField] private bool isFabricRightDone;

    private Vector2 fabricLeftStartPos;
    private Vector2 fabricRightStartPos;
    [SerializeField] private RectTransform handleLeftRect;
    [SerializeField] private RectTransform handleRightRect;
    [SerializeField] private RectTransform fabricLeftRect;
    [SerializeField] private RectTransform fabricRightRect;
    [SerializeField] private RectTransform canvasRect;
    private bool isFabricClick = false;

    private void Start()
    {
        fabricLeftStartPos = fabricLeftRect.anchoredPosition;
        fabricRightStartPos = fabricRightRect.anchoredPosition;
        fabricCenter.GetComponent<Button>().interactable = false;
    }

    private void Update()
    {
        if (isFabricLeftDone && isFabricRightDone && !isFabricClick) 
        {
            fabricCenter.GetComponent<Button>().interactable = true;
            fabricCenter.GetComponent<Image>().color = Color.blue;
            
            fabricCenter.GetComponent<Button>().onClick.RemoveAllListeners();
            fabricCenter.GetComponent<Button>().onClick.AddListener(() =>
            {
                StartCoroutine(OnFabricCenterClicked());
                isFabricClick = true;
                fabricCenter.GetComponent<Button>().interactable = false;
            });
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (MechanicsManager.Instance.isRepairSwingOpened)
        {
            if (eventData.pointerEnter == fabricLeft) { isFabricLeftDragged = true; }
            else if (eventData.pointerEnter == fabricRight) { isFabricRightDragged = true; }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isFabricLeftDragged) { UpdateFabricPosition(fabricLeftRect, eventData); }
        else if (isFabricRightDragged) { UpdateFabricPosition(fabricRightRect, eventData); }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isFabricLeftDragged)
        {
            HandleEndDrag(fabricLeftRect, handleLeftRect, fabricLeftStartPos, eventData, ref isFabricLeftDone);
            isFabricLeftDragged = false;
        }

        if (isFabricRightDragged)
        {
            HandleEndDrag(fabricRightRect, handleRightRect, fabricRightStartPos, eventData, ref isFabricRightDone);
            isFabricRightDragged = false;
        }
    }

    private void UpdateFabricPosition(RectTransform fabricRect, PointerEventData eventData)
    {
        Vector2 localPointerPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, eventData.position, eventData.pressEventCamera, out localPointerPos);
        fabricRect.anchoredPosition = localPointerPos;
    }

    private void HandleEndDrag(RectTransform fabricRect, RectTransform handleRect, Vector2 startPos, PointerEventData eventData, ref bool isDone)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(handleRect, eventData.position, eventData.pressEventCamera))
        {
            fabricRect.anchoredPosition = handleRect.anchoredPosition;
            isDone = true;
        }
        else { fabricRect.anchoredPosition = startPos; }
    }

    private IEnumerator OnFabricCenterClicked()
    {
        yield return StartCoroutine(FadeInPanel(panelBaby, 0.5f));

        // Tandai bahwa mekanik sudah dimainkan
        MechanicsManager.Instance.isRepairSwingPlayed = true;
        yield return new WaitForSeconds(0.6f);
        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);

        yield return StartCoroutine(FadeOutPanel(panelBaby, 0.5f));
    }

    // Fungsi untuk menaikkan transparansi panel
    private IEnumerator FadeInPanel(GameObject panel, float duration)
    {
        panel.SetActive(true);
        Image panelImage = panel.GetComponent<Image>();
        if (panelImage != null)
        {
            Color panelColor = panelImage.color;
            panelColor.a = 0;
            panelImage.color = panelColor;

            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Clamp01(elapsedTime / duration);
                panelColor.a = alpha;
                panelImage.color = panelColor;
                yield return null;
            }

            panelColor.a = 1;
            panelImage.color = panelColor;
        }
    }

    // Fungsi untuk menurunkan transparansi panel
    private IEnumerator FadeOutPanel(GameObject panel, float duration)
    {
        Image panelImage = panel.GetComponent<Image>();
        if (panelImage != null)
        {
            Color panelColor = panelImage.color;
            panelColor.a = 1;
            panelImage.color = panelColor;

            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Clamp01(1 - (elapsedTime / duration));
                panelColor.a = alpha;
                panelImage.color = panelColor;
                yield return null;
            }

            panelColor.a = 0;
            panelImage.color = panelColor;
        }
        panel.SetActive(false);
    }
}
