using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PourWaterManager : MonoBehaviour
{
    [SerializeField] private GameObject currentMechanic;
    public Animator coldWaterAnimator;
    public Animator hotWaterAnimator;
    public Image tubImage;
    public Sprite[] filledTubSprite; // Sprite gambar bak setelah terisi

    private int jmlTuanganSelesai = 2;
    private bool canPourCold = true; // Awalnya bisa tuang air dingin
    private bool canPourHot;

    [SerializeField] private int coldWaterCount = 0;
    [SerializeField] private int hotWaterCount = 0;
    [SerializeField] private GameObject baskom;
    //[SerializeField] private GameObject arrel;
    [SerializeField] private GameObject qToPourCold;
    [SerializeField] private GameObject eToPourHot;
    [SerializeField] private GameObject objPourWater;
    [SerializeField] private GameObject objBathingBaby;

    [SerializeField] private SpaceMechanic spaceMechanic;

    void Start() {
        spaceMechanic = GetComponent<SpaceMechanic>();
    }

    private int totalPourCount = 0; // Tambahkan ini

    void Update()
    {
        if (DialogueTrigger.Instance.isPourWater_23Played)
        {
            if (totalPourCount < jmlTuanganSelesai * 2)
            {
                if (totalPourCount % 2 == 0) // Giliran Q (cold)
                {
                    qToPourCold.SetActive(canPourCold);
                    eToPourHot.SetActive(false);
                    if (canPourCold && Input.GetKeyDown(KeyCode.Q) && coldWaterCount < jmlTuanganSelesai)
                    {
                        canPourCold = false;
                        StartCoroutine(PourColdWater());
                    }
                }
                else // Giliran E (hot)
                {
                    qToPourCold.SetActive(false);
                    eToPourHot.SetActive(canPourHot);
                    if (canPourHot && Input.GetKeyDown(KeyCode.E) && hotWaterCount < jmlTuanganSelesai)
                    {
                        canPourHot = false;
                        StartCoroutine(PourHotWater());
                    }
                }
            }
            else
            {
                qToPourCold.SetActive(false);
                eToPourHot.SetActive(false);
            }
        }

        if (MechanicsManager.Instance.isPourWaterPlayed && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(spaceMechanic.CloseMechanic(0.7f));
            objBathingBaby.SetActive(true);
            objPourWater.SetActive(false);
        }
    }

    private IEnumerator PourColdWater()
    {
        coldWaterCount++;
        totalPourCount++;
        yield return StartCoroutine(PlayAnimCold());
        Debug.Log("Tuang Air Dingin. Jumlah: " + coldWaterCount);
        canPourHot = true; // Setelah anim cold selesai, baru boleh hot
        CheckTubStatus();
    }

    private IEnumerator PourHotWater()
    {
        hotWaterCount++;
        totalPourCount++;
        yield return StartCoroutine(PlayAnimHot());
        ChangeImage();
        Debug.Log("Tuang Air Panas. Jumlah: " + hotWaterCount);
        canPourCold = true; // Setelah anim hot selesai, baru boleh cold
        CheckTubStatus();
    }

    void CheckTubStatus()
    {
        if (coldWaterCount >= jmlTuanganSelesai && hotWaterCount >= jmlTuanganSelesai)
        {
            qToPourCold.SetActive(false);
            eToPourHot.SetActive(false);
            Debug.Log("Bak sudah penuh!");
            MechanicsManager.Instance.isPourWaterPlayed = true;
            StartCoroutine(spaceMechanic.CloseMechanic(0.7f));
        }
    }

    private void ChangeImage()
    {
        switch (hotWaterCount)
        {
            case 1:
                tubImage.sprite = filledTubSprite[1];
                break;

            case 2:
                tubImage.sprite = filledTubSprite[2];
                break;

            case 3:
                tubImage.sprite = filledTubSprite[3];
                break;
        }
    }

    [Header("Animation")]
    [SerializeField] private Image animColdImage;
    [SerializeField] private Image animHotImage;
    [SerializeField] private Sprite[] animColdFrames;
    [SerializeField] private Sprite[] animHotFrames;
    private bool isPlayingAnimation = false;

    private IEnumerator PlayAnimCold()
    {
        if (!isPlayingAnimation)
        {
            animColdImage.gameObject.SetActive(true);
            isPlayingAnimation = true;
            for (int i = 0; i < animColdFrames.Length; i++)
            {
                animColdImage.sprite = animColdFrames[i];
                yield return new WaitForSeconds(0.1875f);
            }
            isPlayingAnimation = false;
            animColdImage.sprite = animColdFrames[0];
        }
    }

    private IEnumerator PlayAnimHot()
    {
        if (!isPlayingAnimation)
        {
            animHotImage.gameObject.SetActive(true);
            isPlayingAnimation = true;
            for (int i = 0; i < animHotFrames.Length; i++)
            {
                animHotImage.sprite = animHotFrames[i];
                yield return new WaitForSeconds(0.1875f);
            }
            isPlayingAnimation = false;
            animHotImage.sprite = animHotFrames[0];
        }
    }
}