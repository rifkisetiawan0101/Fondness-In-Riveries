using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DIALOGUE;

public class CloseCurtain : MonoBehaviour
{
    [SerializeField] private GameObject curentMechanic; 
    public RectTransform guideRight;
    public RectTransform guideLeft;
    public Slider sliderLeft, sliderRight;

    public float delayLength = 3f;

    bool isRightSwiped = false;
    bool isLeftSwiped = false;
    bool isMechanicBegin = false;

    void Start()
    {
        guideRight.gameObject.SetActive(false);
        guideLeft.gameObject.SetActive(false);

        sliderRight.gameObject.SetActive(false);
        sliderLeft.gameObject.SetActive(false);

        sliderLeft.value = 0f;
        sliderRight.value = 0f;

        isRightSwiped = false;
        isLeftSwiped = false;
    }

    void Update()
    {
        if (DialogueTrigger.Instance.isCloseCurtain_5Played)
        {
            if (!isMechanicBegin)
            {
                guideRight.gameObject.SetActive(true);
                guideLeft.gameObject.SetActive(false);

                sliderRight.gameObject.SetActive(true);
                sliderLeft.gameObject.SetActive(false);    

                isMechanicBegin = true;
            }
            
            // Detect when the left mouse button is released
            if (!isRightSwiped || !isLeftSwiped)
            {
                if (!isRightSwiped && sliderRight.value > .9f)
                {
                    Debug.Log("Swiped Left");
                    
                    isRightSwiped = true;
                    isLeftSwiped = false;

                    sliderRight.value = 1f;

                    sliderRight.gameObject.SetActive(false);
                    sliderLeft.gameObject.SetActive(true);

                    guideRight.gameObject.SetActive(false);
                    guideLeft.gameObject.SetActive(true);
                }
                else if (isRightSwiped && !isLeftSwiped && sliderLeft.value > .9f)
                {
                    Debug.Log("Swiped Right");

                    isRightSwiped = true;
                    isLeftSwiped = true;

                    sliderLeft.value = 1f;

                    sliderRight.gameObject.SetActive(false);
                    sliderLeft.gameObject.SetActive(false);

                    guideLeft.gameObject.SetActive(false);
                    guideRight.gameObject.SetActive(false);
                }

                // Check if both swipes are completed
                if (isRightSwiped && isLeftSwiped)
                {
                    Debug.Log("Both swipes completed");

                    // StartCoroutine(DisableMechanic());
                    MechanicsManager.Instance.isCloseCurtainPlayed = true;
                    // You can add further actions here
                }
            }

            if (MechanicsManager.Instance.isCloseCurtainPlayed && !DialogueManager.instance.isRunningConversation && Input.GetKeyDown(KeyCode.Space))
            {
                curentMechanic.SetActive(false);
                MechanicsManager.Instance.isOpenMechanic = false;
            }
        }
    }

    // private IEnumerator DisableMechanic()
    // {
    //     yield return new WaitForSeconds(delayLength);

    //     GetComponent<DisableMechanic>().DisableThisMechanic();
    // }
}