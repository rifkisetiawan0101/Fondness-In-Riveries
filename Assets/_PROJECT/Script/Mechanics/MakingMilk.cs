using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DIALOGUE;

public class MakingMilkMechanic : MonoBehaviour
{
    public int jumlahTuanganSelesai = 3;
    public Sprite[] botolSprite;
    public GameObject botolSusu;
    //public AudioClip sfxSelesai;

    private AudioSource audioSource;
    public int jumlahTuangan = 0;
    [SerializeField] private Image botolSusuImage;
    private bool isMechanicDone;

    [SerializeField] private GameObject blokir;
    [SerializeField] private SpaceMechanic spaceMechanic;
    [SerializeField] private GameObject panel1;
    [SerializeField] private GameObject panel2;

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        botolSusuImage = botolSusu.GetComponent<Image>();
        panel1.SetActive(false);
        panel2.SetActive(false);
        spaceMechanic = GetComponent<SpaceMechanic>();
    }

    private void Update()
    {
        if (DialogueTrigger.Instance.isMakingMilk_18Played && !DialogueManager.instance.isRunningConversation)
        {
            StartCoroutine(spaceMechanic.CloseMechanic(0.7f)); // klik space

            if (Input.GetKeyDown(KeyCode.Space) && isMechanicDone)
            {
                PlayerMovement.Instance.isCarryBottle = true;
                isMechanicDone = false;
            }
        }

        if (!MechanicsManager.Instance.isMakingMilkPlayed)
        {
            botolSusuImage.color = new Color(255, 255, 255, 1);
        }

        bool activeOnce = false;
        if (MechanicsManager.Instance.isMakingMilkOpened && !activeOnce)
        {
            StartCoroutine(BlokirUI());
            activeOnce = true;
        }
    }
    private IEnumerator BlokirUI()
    {
        yield return new WaitForSeconds(2.5f);
        blokir.GetComponent<Image>().raycastTarget = false;
        yield return new WaitForSeconds(0.5f);
        blokir.SetActive(false);
    }

    public void TambahTuangan()
    {
        jumlahTuangan++;
        botolSusuImage.sprite = botolSprite[jumlahTuangan];

        Debug.Log("Tuangan ke-" + jumlahTuangan);
        if (jumlahTuangan >= jumlahTuanganSelesai)
        {
            StartCoroutine(SelesaiMekanisme(0.7f));
        }
    }

    private IEnumerator SelesaiMekanisme(float fade)
    {
        //if (sfxSelesai != null)
        //{
        //    audioSource.PlayOneShot(sfxSelesai);
        //    Debug.Log("Sfx Diputar");
        //}
        panel1.SetActive(true);
        panel1.GetComponent<FadeImage>().FadeIn(fade);
        yield return new WaitForSeconds(fade);

        yield return new WaitForSeconds(fade * 2);

        panel2.SetActive(true);
        panel2.GetComponent<FadeImage>().FadeIn(fade);
        yield return new WaitForSeconds(fade);

        isMechanicDone = true;
        MechanicsManager.Instance.isMakingMilkPlayed = true;
        Debug.Log("Botol susu telah penuh!");
        Debug.Log("Mekanisme selesai!");
    }
}
