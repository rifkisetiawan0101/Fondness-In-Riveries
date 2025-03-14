using System.Collections;
using UnityEngine;

public class GetWater : MonoBehaviour
{
    [SerializeField] private GameObject currentMechanic; 
    public Animator animator;
    public string animationTrigger = "PlayAnimation";
    public bool isMechanicActive;

    private bool isMechanicDone;
    [SerializeField] private GameObject baskom;
    [SerializeField] private GameObject triggerUI;

    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.E) && isMechanicActive)
        {
            triggerUI.SetActive(false);
            StartCoroutine(PlayAnimation());
        }

        if (Input.GetKeyDown(KeyCode.Space) && isMechanicDone)
        {   
            triggerUI.SetActive(true);
            currentMechanic.SetActive(false);
            MechanicsManager.Instance.isOpenMechanic = false;
            baskom.SetActive(true);
            isMechanicDone = false;
            isMechanicActive = false;
        }
    }

    private IEnumerator PlayAnimation()
    {
        animator.SetTrigger(animationTrigger);
        //float animationDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(5);
        isMechanicDone = true;
        MechanicsManager.Instance.isGetWaterPlayed = true;
    }
}
