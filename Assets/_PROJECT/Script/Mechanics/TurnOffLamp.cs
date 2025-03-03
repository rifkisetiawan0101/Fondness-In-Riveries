using UnityEngine;
using UnityEngine.UI;

public class TurnOffLamp : MonoBehaviour
{
    public RectTransform buttonOn;
    public RectTransform buttonOff;

    Button buttonOnBtn;
    Outline buttonOnOutline;
    void Start()
    {
        buttonOnBtn = buttonOn.GetComponent<Button>();
        buttonOnOutline = buttonOn.GetComponent<Outline>();

        buttonOnBtn.interactable = false;
        buttonOnOutline.enabled = false;

        buttonOn.gameObject.SetActive(true);
        buttonOff.gameObject.SetActive(false);
    }

    void Update()
    {
        buttonOnBtn.interactable = DialogueTrigger.Instance.isTurnOffLamp_6Played;
        buttonOnOutline.enabled = DialogueTrigger.Instance.isTurnOffLamp_6Played;
    }
    
    public void TurnOffLampButton()
    {
        buttonOn.gameObject.SetActive(false);
        buttonOff.gameObject.SetActive(true);

        MechanicsManager.Instance.isTurnOffLampPlayed = true;
        // Abis itu matiin mekanik ini
    }
}
