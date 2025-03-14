using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class MakingMilkMechanic : MonoBehaviour
{
    public int jumlahTuanganSelesai = 5;
    public Sprite botolSusuPenuhSprite; 
    public GameObject botolSusu;
    //public AudioClip sfxSelesai;

    private AudioSource audioSource;
    public int jumlahTuangan = 0;
    private Image botolSusuImage;
    private bool isMechanicDone;

    [SerializeField] private GameObject currentMechanic;
    [SerializeField] private GameObject botolBawa;

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        botolSusuImage = botolSusu.GetComponent<Image>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isMechanicDone)
        {
            currentMechanic.SetActive(false);
            MechanicsManager.Instance.isOpenMechanic = false;
            botolBawa.SetActive(true);
            isMechanicDone = false;
        }
    }

    public void TambahTuangan()
    {
        jumlahTuangan++;
        Debug.Log("Tuangan ke-" + jumlahTuangan);

        if (jumlahTuangan >= jumlahTuanganSelesai)
        {
            StartCoroutine(SelesaiMekanisme());
        }
    }

    private IEnumerator SelesaiMekanisme()
    {
        //if (sfxSelesai != null)
        //{
        //    audioSource.PlayOneShot(sfxSelesai);
        //    Debug.Log("Sfx Diputar");
        //}

        yield return new WaitForSeconds(3f);
        isMechanicDone = true;
        botolSusuImage.sprite = botolSusuPenuhSprite;
        MechanicsManager.Instance.isMakingMilkPlayed = true;
        Debug.Log("Botol susu telah penuh!");
        Debug.Log("Mekanisme selesai!");
    }
}
