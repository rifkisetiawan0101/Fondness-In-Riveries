using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpaceMechanic : MonoBehaviour
{
    [SerializeField] private RectTransform space;
    [SerializeField] private GameObject blackPanel;
    [SerializeField] private GameObject currentMechanic;
    bool activeOnce = false;
    bool activeSpaceOnce = false;

    void Start()
    {
        blackPanel.SetActive(false);
        // blackPanelOut.SetActive(false);
        space.gameObject.SetActive(false);
    }

    void Update()
    {
        // Fade in first time
        if (blackPanel.activeSelf && !activeOnce)
        {
            activeOnce = true;
            StartCoroutine(OpenMechanic(0.7f));
            // blackPanelIn.GetComponent<FadeImage>().FadeInOut(.7f);
            // currentMechanic.SetActive(true);
            // MechanicsManager.Instance.isOpenMechanic = true;
        }
    }

    public void SetMechanic(bool value)
    {
        currentMechanic.SetActive(value);
        MechanicsManager.Instance.isOpenMechanic = value;
    }

    public IEnumerator OpenMechanic(float fade)
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<FadeImage>().FadeInOut(fade);
        yield return new WaitForSeconds(fade);
        currentMechanic.SetActive(true);
        yield return new WaitForSeconds(fade);
        MechanicsManager.Instance.isOpenMechanic = true;
        blackPanel.SetActive(false);
    }

    public IEnumerator CloseMechanic(float fade)
    {
        if (!activeSpaceOnce)
        {
            space.gameObject.SetActive(true);
            space.GetComponent<FadeImage>().FadeIn(1f);

            activeSpaceOnce = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && space.gameObject.activeInHierarchy)
        {
            blackPanel.SetActive(true);
            blackPanel.GetComponent<FadeImage>().FadeInOut(fade);
            StartCoroutine(PlaySpaceForDone());
            space.GetComponent<FadeImage>().FadeOut(fade);
            yield return new WaitForSeconds(fade);
            currentMechanic.SetActive(false);
            yield return new WaitForSeconds(fade);
            MechanicsManager.Instance.isOpenMechanic = false;
            blackPanel.SetActive(false);
        }
    }

    [Header("--- On Press Space ---")]
    [SerializeField] private Image spaceForDoneImage;
    [SerializeField] private Sprite[] spaceForDoneFrames;
    [SerializeField] private Sprite spaceForDoneNormal;
    private bool isPlayingAnimation = false; // Pindah jadi field class

    private IEnumerator PlaySpaceForDone()
    {
        if (!isPlayingAnimation)
        {
            isPlayingAnimation = true;
            for (int i = 0; i < spaceForDoneFrames.Length; i++)
            {
                spaceForDoneImage.sprite = spaceForDoneFrames[i];
                yield return new WaitForSeconds(0.083f); // 12 FPS
            }
            isPlayingAnimation = false;
            spaceForDoneImage.sprite = spaceForDoneNormal;
        }
    }

    public IEnumerator OpenItem(float fade)
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<FadeImage>().FadeInOut(fade);
        yield return new WaitForSeconds(fade);
        currentMechanic.SetActive(true);
        yield return new WaitForSeconds(fade);
        MechanicsManager.Instance.isOpenMechanic = true;
        blackPanel.SetActive(false);
    }

    public IEnumerator CloseItem(float fade)
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<FadeImage>().FadeInOut(fade);
        yield return new WaitForSeconds(fade);
        currentMechanic.SetActive(false);
        yield return new WaitForSeconds(fade);
        MechanicsManager.Instance.isOpenMechanic = false;
        blackPanel.SetActive(false);
    }
}
