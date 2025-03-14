using System.Collections;
using UnityEngine;
using UnityEngine.UI; 

public class PourWaterManager : MonoBehaviour
{
    [SerializeField] private GameObject currentMechanic;
    public Animator coldWaterAnimator;
    public Animator hotWaterAnimator;
    public Image tubImage;
    public Sprite filledTubSprite; // Sprite gambar bak setelah terisi
    
    private int jmlTuanganSelesai = 4;
    private bool isMechanicDone;
    private bool canPourCold = true; // Awalnya bisa tuang air dingin
    private bool canPourHot;
    private string animationTrigger = "PlayAnimation";

    [SerializeField] private int coldWaterCount = 0;
    [SerializeField] private int hotWaterCount = 0;
    [SerializeField] private GameObject baskom;
    //[SerializeField] private GameObject arrel;
    [SerializeField] private GameObject qToPourCold;
    [SerializeField] private GameObject eToPourHot;
    [SerializeField] private GameObject objPourWater;
    [SerializeField] private GameObject objBathingBaby;

    void Update()
    {
        if (MechanicsManager.Instance.isBoilWaterPlayed)
        {
            if (Input.GetKeyDown(KeyCode.Q) && canPourCold && coldWaterCount < jmlTuanganSelesai)
            {
                PourColdWater();
            }
            else if (Input.GetKeyDown(KeyCode.E) && canPourHot && hotWaterCount < jmlTuanganSelesai)
            {
                PourHotWater();
            }

            if (Input.GetKeyDown(KeyCode.Escape) && isMechanicDone)
            {
                objBathingBaby.SetActive(true);
                objPourWater.SetActive(false);
                currentMechanic.SetActive(false);
                MechanicsManager.Instance.isOpenMechanic = false;
                isMechanicDone = false;
                Destroy(baskom);
                //arrel.SetActive(true);
            }
        }
    }

    void PourColdWater()
    {
        coldWaterAnimator.SetTrigger(animationTrigger);
        coldWaterCount++;
        canPourCold = false;
        canPourHot = true;
        Debug.Log("Tuang Air Dingin. Jumlah: " + coldWaterCount);
        CheckTubStatus();
        qToPourCold.SetActive(false);
        eToPourHot.SetActive(true);
    }

    void PourHotWater()
    {
        hotWaterAnimator.SetTrigger(animationTrigger);
        hotWaterCount++;
        canPourHot = false;
        canPourCold = true;
        Debug.Log("Tuang Air Panas. Jumlah: " + hotWaterCount);
        CheckTubStatus();
        qToPourCold.SetActive(true);
        eToPourHot.SetActive(false);

        if (hotWaterCount >= jmlTuanganSelesai)
        {
            qToPourCold.SetActive(false);
        }
    }

    void CheckTubStatus()
    {
        if (coldWaterCount >= jmlTuanganSelesai && hotWaterCount >= jmlTuanganSelesai)
        {
            qToPourCold.SetActive(false);
            StartCoroutine(ChangeImageAfterDelay());
            Debug.Log("Bak sudah penuh!");
            MechanicsManager.Instance.isPourWaterPlayed = true;
        }
    }

    private IEnumerator ChangeImageAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        tubImage.sprite = filledTubSprite;
        isMechanicDone = true;
    }
}