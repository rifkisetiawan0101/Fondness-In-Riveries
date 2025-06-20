using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DIALOGUE;

public class DiaryBook : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private bool isDiaryOpen;
    [SerializeField] private GameObject diaryMechanic;
    [SerializeField] private GameObject photo;
    [SerializeField] private bool isDraggingPhoto;
    [SerializeField] private bool isPhotoDone;
    private Vector2 photoStartPos;
    [SerializeField] private RectTransform targetRect;
    [SerializeField] private RectTransform photoRect;
    [SerializeField] private RectTransform canvasRect;

    [SerializeField] private SpaceMechanic spaceMechanic;

    private void Start()
    {
        spaceMechanic = GetComponent<SpaceMechanic>();
        photoStartPos = photoRect.anchoredPosition;
    }

    private void Update()
    {
        if (MechanicsManager.Instance.isPhotoDragged && !DialogueManager.instance.isRunningConversation && Input.GetKeyDown(KeyCode.Space))
        {
            diaryMechanic.SetActive(false);
            MechanicsManager.Instance.isOpenMechanic = false;
        }

        if (MechanicsManager.Instance.isDiaryOpened && !MechanicsManager.Instance.isOpenMechanic && Input.GetKeyDown(KeyCode.B))
        {
            isDiaryOpen = !isDiaryOpen;
            MechanicsManager.Instance.isDiaryOpened = isDiaryOpen;

            if (isDiaryOpen)
            {
                StartCoroutine(spaceMechanic.CloseItem(0.7f));
            }
            else
            {
                StartCoroutine(spaceMechanic.OpenItem(0.7f));
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (MechanicsManager.Instance.isDiaryOpened)
        {
            if (eventData.pointerEnter == photo) { isDraggingPhoto = true; }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDraggingPhoto) { UpdatePhotoPosition(photoRect, eventData); }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDraggingPhoto)
        {
            HandleEndDrag(photoRect, targetRect, photoStartPos, eventData, ref isPhotoDone);
            isDraggingPhoto = false;
        }
    }

    private void UpdatePhotoPosition(RectTransform photoRect, PointerEventData eventData)
    {
        Vector2 localPointerPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, eventData.position, eventData.pressEventCamera, out localPointerPos);
        photoRect.anchoredPosition = localPointerPos;
    }

    private void HandleEndDrag(RectTransform photoRect, RectTransform targetRect, Vector2 startPos, PointerEventData eventData, ref bool isDone)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(targetRect, eventData.position, eventData.pressEventCamera))
        {
            photoRect.anchoredPosition = targetRect.anchoredPosition;
            isDone = true;
            MechanicsManager.Instance.isPhotoDragged = true;
        }
        else { photoRect.anchoredPosition = startPos; }
    }
}
