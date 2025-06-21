using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DIALOGUE;
using System;

public class SwingBabyToSleep : MonoBehaviour
{
    public RectTransform basketWithBaby;
    public RectTransform guideLeft;
    public RectTransform guideRight;
    public RectTransform babyPanel;
    public RectTransform babyPanel2;
    public RectTransform basketButton;
    public RectTransform basketOutline;
    public RectTransform makMomongArel;
    public Animator makArelAnim;
    public RectTransform panelBeforeBabySleep;

    public float speed = 200f;
    public float pointLeft = -10f;
    public float pointRight = 10f;
    public int swingCountRequired = 6;
    public float delayLength = 1.5f;

    SpaceMechanic space;

    int swingCount = 0;
    bool isSwingDialogueTriggered = false;
    // bool isBabySleepDialogueTriggered = false;
    bool movingLeft = true;
    bool activeOnceStarted = false;
    bool activeOncePlayed = false;
    bool isAllowInteract = true;
    float holdDuration = .7f;
    float holdTimer = 0f;


    void Start()
    {
        space = GetComponent<SpaceMechanic>();

        guideLeft.gameObject.SetActive(true);
        guideRight.gameObject.SetActive(true);

        babyPanel.gameObject.SetActive(false);
        basketWithBaby.gameObject.SetActive(false);

        basketButton.gameObject.SetActive(false);

        isAllowInteract = false;

        makArelAnim.SetBool("isFinal", false);

        MechanicsManager.Instance.isSwingComplete = false;
        MechanicsManager.Instance.isPutBabySleep = false;
    }
    void Update()
    {
        if (!MechanicsManager.Instance.isSwingComplete && MechanicsManager.Instance.isSwingingBabyToSleepOpened)
        {
            if (!activeOnceStarted)
            {
                activeOnceStarted = true;
                StartCoroutine(DelayedFadeInGuideLeft());
            }

            // Hold Q or E for a certain duration before executing the code below

            bool qHeld = Input.GetKey(KeyCode.Q);
            bool eHeld = Input.GetKey(KeyCode.E);

            if (isAllowInteract)
            {
                if ((movingLeft && qHeld) || (!movingLeft && eHeld))
                {
                    holdTimer += Time.deltaTime;

                    if (movingLeft && qHeld)
                    {
                        guideLeft.GetComponentInChildren<UIAnim>().PlayAnimation();
                    }
                    else if (!movingLeft && eHeld)
                    {
                        guideRight.GetComponentInChildren<UIAnim>().PlayAnimation();
                    }

                    if (holdTimer >= holdDuration)
                    {
                        // If the player holds Q or E long enough, move the baby
                        MoveBaby();
                        isAllowInteract = false;
                        StartCoroutine(ToggleGuides());
                        StartCoroutine(EnableInputAfterDelay());
                        guideLeft.GetComponentInChildren<UIAnim>().ResetAnimation();
                        guideRight.GetComponentInChildren<UIAnim>().ResetAnimation();
                        holdTimer = 0f;
                    }
                }
                else
                {
                    holdTimer = 0f; // reset if key is released

                    if (movingLeft)
                    {
                        guideLeft.GetComponentInChildren<UIAnim>().ResetAnimation();
                    }
                    else
                    {
                        guideRight.GetComponentInChildren<UIAnim>().ResetAnimation();
                    }
                }
            }
        }

        else if (DialogueTrigger.Instance.isSwingingBaby_3Played && !isSwingDialogueTriggered)
        {
            if (Input.GetKeyDown(KeyCode.E) && isAllowInteract)
            {
                // crossfade black screen first
                panelBeforeBabySleep.GetComponent<FadeImage>().FadeInOut(.7f);
                // play baby panel is called on fade in panel complete

                isAllowInteract = false; // Prevents this block from running again
                isSwingDialogueTriggered = true; // Prevents this block from running again

            }
        }

        if (MechanicsManager.Instance.isSwingingBabyToSleepPlayed && DialogueTrigger.Instance.isPutBabySleep_4Played && !DialogueManager.instance.isRunningConversation)
        {
            StartCoroutine(space.CloseMechanic(.7f));
        }

        if (MechanicsManager.Instance.isSwingingBabyToSleepPlayed && !MechanicsManager.Instance.isGivingMilkPlayed2 && !activeOncePlayed)
        {
            objArell.SetActive(true);
            activeOncePlayed = true;
        }
    }

    [SerializeField] private GameObject objArell;

    private IEnumerator DelayedFadeInGuideLeft()
    {
        print("DelayedFadeInGuideLeft called");
        yield return new WaitForSeconds(3F);
        guideLeft.GetComponent<FadeImage>().FadeIn(.7f);
        isAllowInteract = true;

    }

    void MoveBaby()
    {
        if (movingLeft)
        {
            print("MOVED LEFT");
            makArelAnim.Play("SwayLeft");
            movingLeft = false;


        }
        else
        {
            print("MOVED RIGHT");
            makArelAnim.Play("SwayRight");
            movingLeft = true;
        }

        swingCount++;
        print(swingCount);
        print(movingLeft);
    }

    IEnumerator ToggleGuides()
    {
        // Fade out the guide based on the direction of movement
        if (movingLeft)
        {
            guideRight.GetComponent<FadeImage>().FadeOut(.7f);
        }
        else
        {
            guideLeft.GetComponent<FadeImage>().FadeOut(.7f);
        }

        // Wait until isAllowInteract is true
        while (!isAllowInteract)
        {
            yield return null;
        }

        if (swingCount >= swingCountRequired - 1)
        {
            makArelAnim.SetBool("isFinal", true);
        }

        if (swingCount >= swingCountRequired)
        {
            // If the required swing count is reached, fade out both guides
            MechanicsManager.Instance.isSwingComplete = true;

            isAllowInteract = false;

            // Wait until DialogueTrigger.Instance.isSwingingBaby_3Played is true before executing the following lines
            yield return new WaitUntil(() => DialogueTrigger.Instance.isPutBabySleep_4Played);
            yield return new WaitForSeconds(0.5f);

            basketOutline.gameObject.SetActive(true);
            basketButton.gameObject.SetActive(true);

            basketButton.GetComponent<FadeImage>().FadeIn(.7f);
            basketOutline.GetComponent<FadeImage>().FadeIn(.7f);

            isAllowInteract = true;
            yield return null;
        }
        else
        {
            if (movingLeft)
            {
                guideLeft.GetComponent<FadeImage>().FadeIn(.7f);
            }
            else
            {
                guideRight.GetComponent<FadeImage>().FadeIn(.7f);
            }
        }
    }
    
    private IEnumerator EnableInputAfterDelay()
    {
        yield return new WaitForSeconds(delayLength);
        isAllowInteract = true;
    }

    public void PlayBabyPanel()
    {
        StartCoroutine(BabyPanel());
    }

    private IEnumerator BabyPanel()
    {
        MechanicsManager.Instance.isBabyPanelOpened1 = true;

        basketOutline.gameObject.SetActive(false);
        basketButton.gameObject.SetActive(false);

        makMomongArel.gameObject.SetActive(false);
        babyPanel.gameObject.SetActive(true);
        basketWithBaby.gameObject.SetActive(true);

        yield return new WaitUntil(() => DialogueTrigger.Instance.isPutBabySleep_5Played);

        yield return new WaitForSeconds(delayLength);

        MechanicsManager.Instance.isBabyPanelOpened2 = true;

        babyPanel2.GetComponent<FadeImage>().FadeIn(.7f);

        yield return new WaitUntil(() => DialogueTrigger.Instance.isPutBabySleep_6Played);

        MechanicsManager.Instance.isPutBabySleep = true;
        MechanicsManager.Instance.isSwingingBabyToSleepPlayed = true;

        yield return null;
    }
}

