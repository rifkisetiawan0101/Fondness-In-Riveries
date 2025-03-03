using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class BathingBaby : MonoBehaviour
{
    public bool isMechanicActive;
    private bool isMechanicDone;
    private bool isWashing;
    private bool isArrelInTub;
    private float washingTime = 0f;
    private float washingDuration = 5f;

    [SerializeField] private GameObject triggerUI;
    [SerializeField] private GameObject currentMechanic;
    [SerializeField] private Image arrelTub;
    [SerializeField] private Sprite ArrelEmptyTub;
    [SerializeField] private Sprite ArrelTub;

    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;

    void Start()
    {
        triggerUI.SetActive(true);
        graphicRaycaster = arrelTub.canvas.GetComponent<GraphicRaycaster>();
        eventSystem = EventSystem.current;
        if (eventSystem == null)
        {
            GameObject eventSystemObject = new GameObject("EventSystem");
            eventSystem = eventSystemObject.AddComponent<EventSystem>();
        }
        pointerEventData = new PointerEventData(eventSystem);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isMechanicActive && MechanicsManager.Instance.isCarryingArrelToBath)
            {
                isMechanicActive = false;
                triggerUI.SetActive(false);

                if (!isArrelInTub && !isMechanicDone)
                {
                    arrelTub.sprite = ArrelTub;
                    isArrelInTub = true;
                    isWashing = true;
                    washingTime = 0f;
                    Debug.Log("Ayo mulai menggosok Arrel!");
                }
            }
            else if (isMechanicDone && MechanicsManager.Instance.isBathingBabyDialoguePlayed)
            {
                arrelTub.sprite = ArrelEmptyTub;
                isArrelInTub = false;
                isMechanicDone = false;
                Debug.Log("Selesai menggosok, Arrel sudah diangkat");
                triggerUI.SetActive(false);
            }
        }

        if (isWashing && Input.GetMouseButton(0))
        {
            pointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject == arrelTub.gameObject)
                {
                    washingTime += Time.deltaTime;
                    Debug.Log("Menggosok Arrel... Waktu: " + washingTime.ToString("F2") + " detik");

                    if (washingTime >= washingDuration)
                    {
                        isWashing = false;
                        isMechanicDone = true;
                        MechanicsManager.Instance.isBathingBabyPlayed = true;
                        Debug.Log("Mekanisme selesai! Arrell sudah bersih!");
                    }
                    return;
                }
            }

            Debug.Log("Klik tidak terdeteksi pada arrelTub! Pastikan klik pada objek yang benar.");
        }

        if (Input.GetKeyDown(KeyCode.Escape) && MechanicsManager.Instance.isBathingBabyPlayed && isMechanicDone == false)
        {
            currentMechanic.SetActive(false);
            isMechanicActive = false;
            isWashing = false;
            isArrelInTub = false;
            washingTime = 0f;
            MechanicsManager.Instance.isOpenMechanic = false;
            MechanicsManager.Instance.isCarryingArrelToBath = false;

            Debug.Log("Keluar Mekanisme dan isCarryingArrelToBath dimatikan.");
        }

        if (!triggerUI.activeSelf && MechanicsManager.Instance.isBathingBabyDialoguePlayed && isMechanicDone)
        {
            triggerUI.SetActive(true);
        }
        else if (!triggerUI.activeSelf && MechanicsManager.Instance.isBathingBabyDialoguePlayed && !isMechanicDone)
        {
            triggerUI.SetActive(false);
        }
    }
}
