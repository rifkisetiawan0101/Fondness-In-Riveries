using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DIALOGUE;

public class CloseCurtain : MonoBehaviour
{
    public GameObject closeCurtainBackground;
    public RectTransform background;
    public RectTransform shadowLeft, shadowRight;
    public RectTransform[] leftFrames;
    public RectTransform[] rightFrames;
    public Slider sliderLeft, sliderRight;
    public float delayLength = 3f;

    [SerializeField] private SpaceMechanic spaceMechanic;
    public bool isRightSwiped = false;
    public bool isLeftSwiped = false;
    bool isMechanicBegin = false;
    public bool isCurtainReady = false;

    [SerializeField] private GameObject lightWindow;

    void Start()
    {
        spaceMechanic = GetComponent<SpaceMechanic>();

        closeCurtainBackground.SetActive(false);

        shadowLeft.gameObject.SetActive(true);
        shadowRight.gameObject.SetActive(true);

        sliderRight.gameObject.SetActive(false);
        sliderLeft.gameObject.SetActive(false);

        sliderLeft.value = 0f;
        sliderRight.value = 0f;

        isRightSwiped = false;
        isLeftSwiped = false;
    }

    void Update()
    {
        if (MechanicsManager.Instance.isCloseCurtainOpened && isCurtainReady)
        {
            if (!isMechanicBegin)
            {
                sliderRight.gameObject.SetActive(false);
                sliderLeft.gameObject.SetActive(true);

                shadowLeft.GetComponent<FadeImage>().FadeIn(.5f);
                sliderLeft.GetComponent<FadeImage>().FadeIn(.5f);

                isMechanicBegin = true;
            }

            // if (!isRightSwiped || !isLeftSwiped)
            // {
                if (!isLeftSwiped && !isRightSwiped && sliderLeft.value > .9f)
                {
                    Debug.Log("Left done");

                    StartCoroutine(FadeInLeftFrames());
                    IEnumerator FadeInLeftFrames()
                    {
                        isLeftSwiped = true;
                        isRightSwiped = false;

                        sliderLeft.value = 1f;

                        shadowLeft.GetComponent<FadeImage>().FadeOut(.5f);
                        sliderLeft.GetComponent<FadeImage>().FadeOut(.5f);

                        yield return new WaitForSeconds(1f);
                        leftFrames[0].GetComponent<FadeImage>().FadeIn(.5f);
                        yield return new WaitForSeconds(1.5f);
                        leftFrames[1].GetComponent<FadeImage>().FadeIn(.5f);
                        yield return new WaitForSeconds(3f);

                        sliderRight.gameObject.SetActive(true);
                        shadowRight.GetComponent<FadeImage>().FadeIn(.5f);
                        sliderRight.GetComponent<FadeImage>().FadeIn(.5f);
                    }
                }
                else if (isLeftSwiped && !isRightSwiped && sliderRight.value > .9f)
                {
                    Debug.Log("Right done");

                    StartCoroutine(FadeInRightFrames());
                    IEnumerator FadeInRightFrames()
                    {
                        isRightSwiped = true;
                        isLeftSwiped = true;

                        sliderRight.value = 1f;

                        shadowRight.GetComponent<FadeImage>().FadeOut(.5f);
                        sliderRight.GetComponent<FadeImage>().FadeOut(.5f);

                        yield return new WaitForSeconds(1f);
                        rightFrames[0].GetComponent<FadeImage>().FadeIn(.5f);
                        yield return new WaitForSeconds(1.5f);
                        rightFrames[1].GetComponent<FadeImage>().FadeIn(.5f);
                        yield return new WaitForSeconds(1.5f);

                        MechanicsManager.Instance.isCloseCurtainPlayed = true;
                        closeCurtainBackground.SetActive(true);
                    }
                }
            }

            if (MechanicsManager.Instance.isCloseCurtainPlayed && !DialogueManager.instance.isRunningConversation)
            {
                StartCoroutine(spaceMechanic.CloseMechanic(0.4f));
                lightWindow.SetActive(false);
            }
        // }

        if (MechanicsManager.Instance.isCloseCurtainPlayed && !activeOnce)
        {
            closeCurtainBackground.SetActive(true);
            activeOnce = true;
        }
    }

    private bool activeOnce;
}