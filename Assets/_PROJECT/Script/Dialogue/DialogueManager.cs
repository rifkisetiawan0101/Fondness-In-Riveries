using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace DIALOGUE
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager instance;
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject); // Menghindari duplikasi instance
                return;
            } else instance = this;
            DontDestroyOnLoad(gameObject); // Jika perlu instance bertahan antar scene
            Initialize();
        }

        public GameObject dialogueContainer;
        public TextMeshProUGUI dialogueText;
        private ConversationManager conversationManager;
        private DialogueArchitect architect;
        public delegate void DialogueSystemEvent();
        public event DialogueSystemEvent onDialogue_Next;

        private bool isInitialized = false;
        private bool checkStart;

        private void Initialize()
        {
            if (isInitialized)
                return;

            architect = new DialogueArchitect(dialogueText);
            conversationManager = new ConversationManager(architect);
        }

        // referensi kalo mau klik layar next
        public void OnDialogue_Next()
        {
            onDialogue_Next?.Invoke();
        }

        public void Say(string speaker, string dialogue)
        {
            List<string> conversation = new List<string>() { $"{speaker} \"{dialogue}\"" };
            Say(conversation);
        }

        public void Say(List<string> conversation)
        {
            conversationManager.StartConversation(conversation);
        }
        
        public void StartConversation()
        {
            checkStart = true;
            if (checkStart == true)
            {
                List<string> lines = FileManager.ReadTextAsset(currentDialogue, false);
                Say(lines);
            }
            checkStart = false;
        }

        private void Update() 
        {
            DialogueCheck();
            // DialogueTrigger.Instance.ChooseDialogue();

            if(Input.GetKeyDown(KeyCode.Space) && isRunningConversation) 
            {
                OnDialogue_Next();
            }
        }

        public bool isRunningConversation; 
        [SerializeField] private DialogueTrigger triggerAct_1;
        [SerializeField] private TextAsset currentDialogue;
        private void DialogueCheck()
        {
            if (SceneManager.GetActiveScene().name == "Act-1 Kamar Ibu" || SceneManager.GetActiveScene().name == "Act-1 Ruang Tamu")
            {
                currentDialogue = triggerAct_1.currentDialogue;
            }
        }
    }
}