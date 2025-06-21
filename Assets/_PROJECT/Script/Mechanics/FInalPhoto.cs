using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FInalPhoto : MonoBehaviour
{
    [SerializeField] private GameObject panelSwing;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private GameObject whiteFlash;
    [SerializeField] private GameObject blackTBC;
    [SerializeField] private Button buttonPhoto;
    [SerializeField] private Image imgButton;
    [SerializeField] private Sprite normalPhoto;
    [SerializeField] private Sprite onPressPhoto;
    [SerializeField] private Sprite disablePhoto;
    SpaceMechanic spaceMechanic;

    private bool isReady = false;

    void Start()
    {
        spaceMechanic = GetComponent<SpaceMechanic>();
        imgButton.sprite = disablePhoto;
        buttonPhoto.interactable = false;
        buttonPhoto.onClick.AddListener(OnPhotoUp); // OnClick untuk pointer up
    }

    void Update()
    {
        if (MechanicsManager.Instance.isFinalPhotoOpened && !isReady)
        {
            StartCoroutine(FadeInBlackAndSetup());
            isReady = true;
        }

        // Enable tombol jika dialog sudah selesai
        if (isReady && !buttonPhoto.interactable && DialogueTrigger.Instance.isFinalPhotoPlayed)
        {
            buttonPhoto.interactable = true;
            imgButton.sprite = normalPhoto;
        }
    }

    private IEnumerator FadeInBlackAndSetup()
    {
        yield return new WaitForSeconds(12f);
        blackScreen.SetActive(true);
        blackScreen.GetComponent<FadeImage>().FadeIn(0.4f);
        yield return new WaitForSeconds(0.4f);
        panelSwing.SetActive(false);
        blackScreen.GetComponent<FadeImage>().FadeOut(0.4f);
        yield return new WaitForSeconds(0.4f);
    }

    public void OnPhotoDown()
    {
        if (!buttonPhoto.interactable) return;
        imgButton.sprite = onPressPhoto;
    }

    public void OnPhotoUp()
    {
        if (!buttonPhoto.interactable) return;
        imgButton.sprite = normalPhoto;
        buttonPhoto.interactable = false;
        StartCoroutine(FlashAndBlack());
    }

    public void OnPhotoEnter()
    {
        if (!buttonPhoto.interactable) return;
        imgButton.sprite = onPressPhoto;
    }

    public void OnPhotoExit()
    {
        if (!buttonPhoto.interactable) return;
        imgButton.sprite = normalPhoto;
    }

    private IEnumerator FlashAndBlack()
    {
        // Flash putih
        whiteFlash.SetActive(true);
        whiteFlash.GetComponent<FadeImage>().FadeIn(0.05f);
        yield return new WaitForSeconds(0.05f);
        whiteFlash.GetComponent<FadeImage>().FadeOut(0.05f);
        yield return new WaitForSeconds(0.05f);

        yield return new WaitForSeconds(4f);

        blackTBC.SetActive(true);
        blackTBC.GetComponent<FadeImage>().FadeIn(0.5f);
        yield return new WaitForSeconds(0.5f);
        MechanicsManager.Instance.isFinalPhotoPlayed = true;
    }
}
