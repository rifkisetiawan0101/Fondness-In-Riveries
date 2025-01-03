using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // public DialogueContainer dialogueContainer = new DialogueContainer();
    public GameObject dialogueContainer;
    public TextMeshProUGUI dialogueText;  
    public static DialogueManager instance;

    private void Awake() 
    {
        if (instance == null) 
            instance = this;
        else
            DestroyImmediate(gameObject);
    }
}
