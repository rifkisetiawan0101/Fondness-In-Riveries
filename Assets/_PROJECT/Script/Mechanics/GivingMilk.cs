using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DIALOGUE;

public class GivingMilk : MonoBehaviour
{
    private bool carryBabyOnce;
    [SerializeField] private GameObject arrelInObj;

    SpaceMechanic spaceMechanic;
    private void Start()
    {
        spaceMechanic = GetComponent<SpaceMechanic>();
    }

    void OnEnable()
    {
        spaceMechanic = GetComponent<SpaceMechanic>();
    }

    void Update()
    {
        if (MechanicsManager.Instance.isGivingMilkOpened && !MechanicsManager.Instance.isGivingMilkPlayed)
        {
            StartCoroutine(PlayCutScene());// nyusuin
            PlayerMovement.Instance.isCarryBottle = false;
        }

        if (MechanicsManager.Instance.isPourWaterPlayed && MechanicsManager.Instance.isGivingMilkPlayed2 && !MechanicsManager.Instance.isBathingBabyPlayed && !carryBabyOnce)
        {
            carryBabyOnce = true;
            arrelInObj.SetActive(false);//ngambil arel
            PlayerMovement.Instance.isCarryBaby = true;
        }

        if (MechanicsManager.Instance.isBathingBabyPlayed && MechanicsManager.Instance.isGivingMilkPlayed3 && carryBabyOnce)
        {
            carryBabyOnce = false;
            arrelInObj.SetActive(true);// naro arel
            PlayerMovement.Instance.isCarryBaby = false;
        }

        if (MechanicsManager.Instance.isGivingMilkPlayed && !DialogueManager.instance.isRunningConversation)
        {
            StartCoroutine(spaceMechanic.CloseMechanic(0.7f)); // klik space
        }
    }

    [Header("Cutscene")]
    [SerializeField] private Image cutSceneImage;
    [SerializeField] private Sprite[] cutSceneFrames;
    [SerializeField] private Sprite cutSceneFramesWet;
    private bool isPlayingAnimation = false;

    private IEnumerator PlayCutScene()
    {
        yield return new WaitForSeconds(2f);
        if (!isPlayingAnimation)
        {
            isPlayingAnimation = true;
            for (int i = 0; i < cutSceneFrames.Length; i++)
            {
                cutSceneImage.sprite = cutSceneFrames[i];
                yield return new WaitForSeconds(2f);
            }
            yield return new WaitUntil(() => DialogueTrigger.Instance.isGivingMilk_20Played);
            yield return new WaitForSeconds(1f);
            cutSceneImage.sprite = cutSceneFramesWet;
        }
    }
}
