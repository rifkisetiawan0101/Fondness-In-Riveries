using UnityEngine.UI;
using UnityEngine;

public class SendokSusu : MonoBehaviour
{
    public Sprite sendokKosongSprite;
    public Sprite sendokBerisiSprite;
    public MakingMilkMechanic makingMilkScript;

    private Image sendokSpriteImage;
    private bool isSendokKosong = true;
    private bool isDragging = false;

    void Start()
    {
        sendokSpriteImage = GetComponent<Image>();

        if (makingMilkScript == null)
        {
            Debug.LogError("Referensi ke MakingMilkMechanic belum diatur!");
        }
    }

    void Update()
    {
        if (makingMilkScript != null && MechanicsManager.Instance.isMakingMilkOpened)
        {
            CheckMouseInput();
            if (isDragging)
            {
                DragSendok();
            }
        }
    }

    void CheckMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;

            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {
                isDragging = true;
                Debug.Log("Mulai drag sendok.");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                isDragging = false;
                Debug.Log("Berhenti drag sendok.");
            }
        }
    }

    void DragSendok()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;
        transform.position = mousePosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Trigger dengan: {other.gameObject.name}");

        if (other.CompareTag("KalengSusu") && isSendokKosong && (makingMilkScript.jumlahTuangan < makingMilkScript.jumlahTuanganSelesai))
        {
            sendokSpriteImage.sprite = sendokBerisiSprite;
            isSendokKosong = false;
            Debug.Log("Sendok diisi susu.");
        }
        else if (other.CompareTag("BotolSusu") && !isSendokKosong)
        {
            sendokSpriteImage.sprite = sendokKosongSprite;
            isSendokKosong = true;
            makingMilkScript.TambahTuangan();
        }
    }
}
