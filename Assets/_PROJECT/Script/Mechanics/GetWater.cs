using System.Collections;
using UnityEngine;
using DIALOGUE;

public class GetWater : MonoBehaviour
{
    [SerializeField] private GameObject currentMechanic; 
    public Animator animator;
    public string animationTrigger = "PlayAnimation";
    public bool isMechanicActive;

    private bool isMechanicDone;
    [SerializeField] private GameObject baskom;
    [SerializeField] private GameObject triggerUI;

    [SerializeField] private SpaceMechanic spaceMechanic;

    void Start() {
        spaceMechanic = GetComponent<SpaceMechanic>();
    }

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
            baskom.SetActive(true);
            isMechanicDone = false;
            isMechanicActive = false;
        }
        
        if (MechanicsManager.Instance.isGetWaterPlayed && !DialogueManager.instance.isRunningConversation)
        {
            StartCoroutine(spaceMechanic.CloseMechanic(0.7f)); // klik space
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
