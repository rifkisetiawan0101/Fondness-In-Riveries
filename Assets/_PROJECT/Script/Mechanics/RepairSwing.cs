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

    [SerializeField] private SpaceMechanic spaceMechanic;

    private void Start()
    {
        spaceMechanic = GetComponent<SpaceMechanic>();
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
        panelBaby.GetComponent<FadeImage>().FadeIn(0.7f);
        yield return new WaitForSeconds(0.7f);

        // Tandai bahwa mekanik sudah dimainkan
        MechanicsManager.Instance.isRepairSwingPlayed = true;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !DialogueManager.instance.isRunningConversation);

        panelBaby.GetComponent<FadeImage>().FadeOut(0.7f);
        yield return new WaitForSeconds(0.7f);
    }
}
