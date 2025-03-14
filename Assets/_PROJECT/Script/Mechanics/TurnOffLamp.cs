using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DIALOGUE;

public class TurnOffLamp : MonoBehaviour
{
    [SerializeField] private GameObject currentMechanic;
    public RectTransform buttonOn;
    public RectTransform buttonOnOutline;
    public RectTransform buttonOff;
    public RectTransform bayanganNyala;
    public RectTransform gelapMati;
    public Transform dustParticleMap;
    public Transform dustParticleMech;
    
    public float delayLength = 3f;

    Button buttonOnBtn;
    //Outline buttonOnOutline;
    void Start()
    {
        buttonOnBtn = buttonOn.GetComponent<Button>();
        //buttonOnOutline = buttonOn.GetComponent<Outline>();

        buttonOnBtn.interactable = false;
        //buttonOnOutline.enabled = false;

        buttonOn.gameObject.SetActive(true);
        buttonOnOutline.gameObject.SetActive(false);
        buttonOff.gameObject.SetActive(false);
        bayanganNyala.gameObject.SetActive(true);
        gelapMati.gameObject.SetActive(false);
        dustParticleMap.gameObject.SetActive(false);
    }

    void Update()
    {
        buttonOnBtn.interactable = DialogueTrigger.Instance.isTurnOffLamp_6Played;
        dustParticleMap.gameObject.SetActive(MechanicsManager.Instance.isTurnOffLampPlayed && !MechanicsManager.Instance.isOpenMechanic);

        if (MechanicsManager.Instance.isTurnOffLampPlayed && !DialogueManager.instance.isRunningConversation && Input.GetKeyDown(KeyCode.Space))
        {
            currentMechanic.SetActive(false);
            MechanicsManager.Instance.isOpenMechanic = false;
        }
    }
    
    public void TurnOffLampButton()
    {
        buttonOn.gameObject.SetActive(false);
        buttonOff.gameObject.SetActive(true);
        bayanganNyala.gameObject.SetActive(false);
        gelapMati.gameObject.SetActive(true);
        dustParticleMech.gameObject.SetActive(false);

        MechanicsManager.Instance.isTurnOffLampPlayed = true;
        // StartCoroutine(DisableMechanic());
        // Abis itu matiin mekanik ini
    }

    public void OnPointerEnterButton()
    {
        if (buttonOnBtn.interactable)
        {
            buttonOnOutline.gameObject.SetActive(true);
        }
    }

    public void OnPointerExitButton()
    {
        buttonOnOutline.gameObject.SetActive(false);
    }

    // private IEnumerator DisableMechanic()
    // {
    //     yield return new WaitForSeconds(delayLength);

    //     GetComponent<DisableMechanic>().DisableThisMechanic();
    // }
}