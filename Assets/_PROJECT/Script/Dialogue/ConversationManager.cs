using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE
{
    public class ConversationManager : MonoBehaviour
    {
        public static ConversationManager Instance;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject); // Menghindari duplikasi instance
                return;
            } else Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private DialogueManager dialogueManager => DialogueManager.instance;
        private Coroutine process = null;
        public bool isRunning => process != null;
        private bool dialogueNext = false; // ketika mencet space buat next

        // samain architect nya dengan yang ada di DialogueManager
        private DialogueArchitect architect = null;

        [Header("Dialogue Next")]
        public int currentLineIndex = 0;
        public List<string> currentConversation;
        public bool HasNextLine => currentLineIndex < currentConversation?.Count - 1;
        public ConversationManager(DialogueArchitect architect)
        {
            this.architect = architect;
            dialogueManager.onDialogue_Next += OnDialogue_Next;
        }

        private void OnDialogue_Next()
        {
            dialogueNext = true;
        }

        public void StartConversation(List<string> conversation)
        {
            currentLineIndex = 0;
            currentConversation = conversation;
            StopConversation();
            process = dialogueManager.StartCoroutine(RunningConversation(conversation));
        }

        public void StopConversation()
        {
            if (!isRunning)
                return;
            
            dialogueManager.StopCoroutine(process);
            process = null;
        }

        
        IEnumerator RunningConversation(List<string> conversation)
        {
            // MechanicsManager.Instance.isOpenMechanic = true;
            for (int i = 0; i < conversation.Count; i++)
            {
                currentLineIndex = i;
                if (string.IsNullOrWhiteSpace(conversation[i]))
                    continue;

                DIALOGUE_LINE line = DialogueParser.Parse(conversation[i]);

                //Show dialogue
                if (line.hasDialogue)
                    yield return Line_RunDialogue(line);

                if (line.hasCommands)
                    yield return Line_RunCommands(line);
            }
            currentLineIndex = 0;
            currentConversation = null;
        }

        IEnumerator Line_RunDialogue(DIALOGUE_LINE line)
        {
            //Build Dialogue
            architect.BuildDialogue(line.speaker + line.dialogue);
            while (architect.isBuilding)
            {
                dialogueManager.isRunningConversation = true;
                if (dialogueNext)
                {
                    architect.ForceComplete();
                    dialogueNext = false;
                }
                yield return null;
            }

            // Wait For Next Input
            while (!dialogueNext) { yield return null; }
            dialogueNext = false;
            dialogueManager.isRunningConversation = false;
        }

        IEnumerator Line_RunCommands(DIALOGUE_LINE line)
        {
            Debug.Log(line.commands);
            yield return null;
        }
    }
}