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
            DontDestroyOnLoad(gameObject); // Jika perlu instance bertahan antar scene
        }

        private DialogueManager dialogueManager => DialogueManager.instance;
        private Coroutine process = null;
        public bool isRunning => process != null;
        private bool dialogueNext = false; // ketika mencet space buat next

        // samain architect nya dengan yang ada di DialogueManager
        private DialogueArchitect architect = null;
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
            dialogueManager.dialogueContainer.SetActive(true);
            for(int i = 0; i < conversation.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(conversation[i]))
                    continue;

                DIALOGUE_LINE line = DialogueParser.Parse(conversation[i]);
                
                //Show dialogue
                if (line.hasDialogue)
                    yield return Line_RunDialogue(line);

                if (line.hasCommands)
                    yield return Line_RunCommands(line);
            }
            dialogueManager.dialogueContainer.SetActive(false);
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