using UnityEngine;

public class BathingBaby : MonoBehaviour
{
    public bool isMechanicActive;
    private bool isMechanicDone;
    [SerializeField] private SpaceMechanic spaceMechanic;

    [Header("Noda Settings")]
    [SerializeField] private GameObject noda1;
    [SerializeField] private GameObject noda2;
    [SerializeField] private float waktuGosok = 2f; // waktu gosok per noda

    private float noda1Timer = 0f;
    private float noda2Timer = 0f;
    private bool noda1Bersih = false;
    private bool noda2Bersih = false;

    void Start()
    {
        spaceMechanic = GetComponent<SpaceMechanic>();
        noda1.SetActive(true);
        noda2.SetActive(true);
        noda1Bersih = false;
        noda2Bersih = false;
        noda1Timer = 0f;
        noda2Timer = 0f;
    }

    void Update()
    {
        // Gosok noda1
        if (!noda1Bersih && Input.GetMouseButton(0))
        {
            noda1Timer += Time.deltaTime;
            if (noda1Timer >= waktuGosok)
            {
                noda1Bersih = true;
                noda1.SetActive(false);
                Debug.Log("Noda 1 hilang!");
            }
        }

        // Gosok noda2
        if (!noda2Bersih && Input.GetMouseButton(1))
        {
            noda2Timer += Time.deltaTime;
            if (noda2Timer >= waktuGosok)
            {
                noda2Bersih = true;
                noda2.SetActive(false);
                Debug.Log("Noda 2 hilang!");
            }
        }

        // Jika kedua noda sudah bersih
        if (noda1Bersih && noda2Bersih && !isMechanicDone)
        {
            isMechanicDone = true;
            MechanicsManager.Instance.isBathingBabyPlayed = true;
            Debug.Log("Mekanisme selesai! Arrel sudah bersih!");
        }

        bool activeOnce = false;
        if (DialogueTrigger.Instance.isBathingBaby_24Played && !activeOnce)
        {
            activeOnce = true;
            StartCoroutine(spaceMechanic.CloseMechanic(0.7f));
        }

        if (Input.GetKeyDown(KeyCode.Space) && DialogueTrigger.Instance.isBathingBaby_24Played && isMechanicDone == false)
            {
                isMechanicActive = false;
                noda1Timer = 0f;
                noda2Timer = 0f;
                MechanicsManager.Instance.isCarryingArrelToBath = false;

                Debug.Log("Keluar Mekanisme dan isCarryingArrelToBath dimatikan.");

                StartCoroutine(spaceMechanic.CloseMechanic(0.7f)); // klik space
            }
    }
}