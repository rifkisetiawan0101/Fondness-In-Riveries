using UnityEngine;

public class CloseCurtain : MonoBehaviour
{
    public RectTransform guideRight;
    public RectTransform guideLeft;

    Vector2 startMousePosition;
    Vector2 endMousePosition;

    bool isRightSwiped = false;
    bool isLeftSwiped = false;

    void Start()
    {
        // Ensure only the right guide is active at the start
        guideRight.gameObject.SetActive(true);
        guideLeft.gameObject.SetActive(false);

        isRightSwiped = false;
        isLeftSwiped = true;
    }

    void Update()
    {
        if (DialogueTrigger.Instance.isCloseCurtain_5Played)
        {
            // Detect when the left mouse button is pressed
            if (Input.GetMouseButtonDown(0))
            {
                startMousePosition = Input.mousePosition;
            }

            // Detect when the left mouse button is released
            if (Input.GetMouseButtonUp(0) && !isRightSwiped || !isLeftSwiped)
            {
                endMousePosition = Input.mousePosition;

                if (!isRightSwiped && endMousePosition.x < startMousePosition.x)
                {
                    Debug.Log("Swiped Left");
                    
                    isRightSwiped = true;
                    isLeftSwiped = false;

                    guideRight.gameObject.SetActive(false);
                    guideLeft.gameObject.SetActive(true);
                }
                else if (!isLeftSwiped && endMousePosition.x > startMousePosition.x)
                {
                    Debug.Log("Swiped Right");

                    isRightSwiped = true;
                    isLeftSwiped = true;

                    guideLeft.gameObject.SetActive(false);
                    guideRight.gameObject.SetActive(false);
                }

                // Check if both swipes are completed
                if (isRightSwiped && isLeftSwiped)
                {
                    Debug.Log("Both swipes completed");
                    MechanicsManager.Instance.isCloseCurtainPlayed = true;
                    // You can add further actions here
                }
            }
        }
    }
}