using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwingBabyToSleep : MonoBehaviour
{
    public RectTransform baby;
    public RectTransform basket;
    public RectTransform guideLeft;
    public RectTransform guideRight;
    public RectTransform babyPanel;

    public float speed = 200f;
    public float pointLeft = -10f;
    public float pointRight = 10f;
    public int swingCountRequired = 4;
    public float delayLength = 1.5f;

    Button basketButton;
    Outline basketOutline;

    int swingCount = 0;
    bool isSwingDialogueTriggered = false;
    bool isBabySleepDialogueTriggered = false;
    bool movingLeft = true;

    void Start()
    {
        guideLeft.gameObject.SetActive(true);
        guideRight.gameObject.SetActive(false);
        babyPanel.gameObject.SetActive(false);

        basketButton = basket.GetComponent<Button>();
        basketOutline = basket.GetComponent<Outline>();

        basketOutline.enabled = false;
        basketButton.interactable = false;

        MechanicsManager.Instance.isSwingComplete = false;
        MechanicsManager.Instance.isPutBabySleep = false;
    }
    void Update()
    {
        if (!MechanicsManager.Instance.isSwingComplete)
        {
            if (movingLeft && Input.GetKey(KeyCode.Q))
            {
                MoveBaby(pointLeft);
            }
            else if (!movingLeft && Input.GetKey(KeyCode.E))
            {
                MoveBaby(pointRight);
            }
        }

        if (DialogueTrigger.Instance.isSwingingBaby_3Played && !isSwingDialogueTriggered)
        {
            isSwingDialogueTriggered = true;

            basketOutline.enabled = true;
            basketButton.interactable = true;
        }

        if (DialogueTrigger.Instance.isPutBabySleep_4Played && !isBabySleepDialogueTriggered)
        {
            isBabySleepDialogueTriggered = true;

            StartCoroutine(DisableMechanic());
        }
    }
    
    public void PlaySleepBabyAnim()
    {
        basketOutline.enabled = false;
        basketButton.interactable = false;

        StartCoroutine(PlayBabyPanel());
    }

    private IEnumerator PlayBabyPanel()
    {
        babyPanel.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(delayLength);
        
        babyPanel.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(delayLength);

        MechanicsManager.Instance.isPutBabySleep = true;
    }

    private IEnumerator DisableMechanic()
    {
        yield return new WaitForSeconds(delayLength);
        MechanicsManager.Instance.isSwingingBabyToSleepPlayed = true;
    }

    void MoveBaby(float target)
    {
        Vector3 babyPosition = baby.localPosition;

        if (movingLeft)
        {
            babyPosition.x = Mathf.MoveTowards(babyPosition.x, target, speed * Time.deltaTime);
        }
        else
        {
            babyPosition.x = Mathf.MoveTowards(babyPosition.x, target, speed * Time.deltaTime);
        }

        baby.localPosition = babyPosition;

        if (Mathf.Abs(baby.localPosition.x - target) < 0.1f)
        {
            ToggleGuides();
        }
    }

    void ToggleGuides()
    {
        movingLeft = !movingLeft;
        swingCount++;

        guideLeft.gameObject.SetActive(movingLeft);
        guideRight.gameObject.SetActive(!movingLeft);
        
        if (swingCount >= swingCountRequired)
        {
            MechanicsManager.Instance.isSwingComplete = true;
            
            guideLeft.gameObject.SetActive(false);
            guideRight.gameObject.SetActive(false);
        }
    }
}
