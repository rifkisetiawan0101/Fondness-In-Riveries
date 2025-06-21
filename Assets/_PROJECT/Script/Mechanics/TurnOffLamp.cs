using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DIALOGUE;
using UnityEngine.Rendering.Universal;

public class TurnOffLamp : MonoBehaviour
{
    public RectTransform background;
    public RectTransform buttonOn;
    public RectTransform buttonOnOutline;
    public RectTransform buttonOff;
    public RectTransform bayanganNyala;
    public RectTransform gelapMati;
    public Transform dustParticleMap;
    public Transform dustParticleMech;
    public float delayLength = 3f;
    
    [SerializeField] private SpaceMechanic spaceMechanic;

    [SerializeField] private GameObject overlayOffLamp;
    [SerializeField] private Light2D globalLight;
    

    Button buttonOnBtn;
    //Outline buttonOnOutline;
    void Start()
    {
        spaceMechanic = GetComponent<SpaceMechanic>();
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

        overlayOffLamp.SetActive(false);
        globalLight.intensity = 0.75f;
    }

    void Update()
    {
        buttonOnBtn.interactable = MechanicsManager.Instance.isTurnOffLampOpened;
        dustParticleMap.gameObject.SetActive(MechanicsManager.Instance.isTurnOffLampPlayed && !MechanicsManager.Instance.isOpenMechanic);

        if (DialogueTrigger.Instance.isTurnOffLamp_9Played)
        {
            StartCoroutine(spaceMechanic.CloseMechanic(0.7f));
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

        overlayOffLamp.SetActive(true);
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
}