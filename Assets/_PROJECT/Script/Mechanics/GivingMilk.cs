using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class GivingMilk : MonoBehaviour
{
    public Animator animator; 
    public string animationTrigger = "PlayAnimation"; 
    public GameObject cutScene;
    public bool isMechanicActive;

    private bool isMechanicDone;
    [SerializeField] private GameObject triggerUI;
    [SerializeField] private GameObject currentMechanic;
    [SerializeField] private GameObject giveMilk;
    [SerializeField] private GameObject arrel;
    [SerializeField] private GameObject arrelBawa;
    [SerializeField] private GameObject botolBawa;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isMechanicActive && !MechanicsManager.Instance.isPourWaterPlayed && !MechanicsManager.Instance.isBathingBabyPlayed)
        {   
            triggerUI.SetActive(false);
            StartCoroutine(PlayAnimation());
        } 
        else if (Input.GetKeyDown(KeyCode.E) && isMechanicActive && MechanicsManager.Instance.isPourWaterPlayed && !MechanicsManager.Instance.isBathingBabyPlayed)
        {   
            MechanicsManager.Instance.isGivingMilkPlayed2 = true;
            triggerUI.SetActive(false);
            arrel.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.E) && isMechanicActive && MechanicsManager.Instance.isPourWaterPlayed && MechanicsManager.Instance.isBathingBabyPlayed)
        {
            MechanicsManager.Instance.isGivingMilkPlayed3 = true;
            triggerUI.SetActive(false);
            arrel.SetActive(true);
            MechanicsManager.Instance.isGetBackBaby = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isMechanicActive && MechanicsManager.Instance.isGivingMilkDialoguePlayed && !MechanicsManager.Instance.isPourWaterPlayed && !MechanicsManager.Instance.isBathingBabyPlayed)
        {   
            StartCoroutine(SetBoolAfterDelay());
            botolBawa.SetActive(false);
            triggerUI.SetActive(true);
            giveMilk.SetActive(false);
            currentMechanic.SetActive(false);
            MechanicsManager.Instance.isOpenMechanic = false;
            isMechanicActive = false;
            isMechanicDone = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isMechanicActive && MechanicsManager.Instance.isGivingMilkPlayed2 && MechanicsManager.Instance.isPourWaterPlayed && !MechanicsManager.Instance.isBathingBabyPlayed)
        {
            MechanicsManager.Instance.isCarryingArrelToBath = true;
            Debug.Log($"isCarryingArrelToBath: {MechanicsManager.Instance.isCarryingArrelToBath}");
            arrelBawa.SetActive(true);
            triggerUI.SetActive(true);
            currentMechanic.SetActive(false);
            isMechanicActive = false;
            MechanicsManager.Instance.isOpenMechanic = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isMechanicActive && MechanicsManager.Instance.isGivingMilkPlayed3 && MechanicsManager.Instance.isBathingBabyPlayed && MechanicsManager.Instance.isGetBackBaby) 
        {
            arrelBawa.SetActive(false);
            currentMechanic.SetActive(false);
            MechanicsManager.Instance.isOpenMechanic = false;
            isMechanicActive = false;
            isMechanicDone = true;
        }
    }

    private IEnumerator PlayAnimation()
    {
        animator.SetTrigger(animationTrigger);
        //float animationDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(2.13f);
        MechanicsManager.Instance.isNowGivingMilk = true;
        yield return new WaitUntil(() => !MechanicsManager.Instance.isNowGivingMilk);
        PlayVideo();
        Debug.Log("Video diputar");
        yield return new WaitForSeconds(13f);
        cutScene.SetActive(false);
        isMechanicDone = true;
        MechanicsManager.Instance.isGivingMilkPlayed = true;
    }

    private IEnumerator SetBoolAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        MechanicsManager.Instance.isGoingToGetWater = true;
    }

    private void PlayVideo()
    {
        cutScene.SetActive(true);
    }
}
