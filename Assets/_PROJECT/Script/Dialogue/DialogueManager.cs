using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace DIALOGUE
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager instance;
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else instance = this;
            DontDestroyOnLoad(gameObject);
            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        }

        public DialogueArchitect architect;
        [Header("---Dialogue Components---")]
        public GameObject dialogueContainer;
        private TextMeshProUGUI dialogueText;
        public ConversationManager conversationManager;
        public delegate void DialogueSystemEvent();
        public event DialogueSystemEvent onDialogue_Next;

        // private bool isInitialized = false;
        private bool checkStart;

        // referensi kalo mau klik layar next
        public void OnDialogue_Next()
        {
            onDialogue_Next?.Invoke();
        }

        // public void Say(string speaker, string dialogue)
        // {
        //     List<string> conversation = new List<string>() { $"{speaker} \"{dialogue}\"" };
        //     Say(conversation);
        // }

        public void Say(List<string> conversation)
        {
            conversationManager.StartConversation(conversation);
        }

        public bool isAutoConversation = false;
        public IEnumerator StartConversationInMechanics(bool isDialogueInMechanic)
        {
            // OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
            MechanicsManager.Instance.isOpenMechanic = isDialogueInMechanic ? true : false;
            isAutoConversation = false;
            dialogueShadow.gameObject.SetActive(true);
            checkStart = true;
            if (checkStart == true)
            {
                List<string> lines = FileManager.ReadTextAsset(currentDialogue, false);
                StartCoroutine(OpenDialogue(0.4f));
                Say(lines);
                spaceForNext.SetActive(true);
                spaceForNextImage.sprite = conversationManager.HasNextLine ? spaceForNextNormal : spaceForDoneNormal;
            }
            checkStart = false;
            yield return null;
        }

        public IEnumerator StartAutoConversationInMechanics(bool isDialogueInMechanic)
        {
            MechanicsManager.Instance.isOpenMechanic = isDialogueInMechanic ? true : false;
            isAutoConversation = true;
            dialogueShadow.gameObject.SetActive(true);
            checkStart = true;
            if (checkStart == true)
            {
                List<string> lines = FileManager.ReadTextAsset(currentDialogue, false);
                StartCoroutine(OpenDialogue(0.4f));
                Say(lines);
                spaceForNext.SetActive(false);
            }
            checkStart = false;
            yield return null;
        }

        public IEnumerator CloseDialogue(float fade)
        {
            yield return null;
            isRunningConversation = false;
            dialogueContainer.GetComponent<FadeImage>().FadeOutCanvasGroup(fade);
            // dialogueShadow.GetComponent<FadeImage>().FadeOut(fade);
            yield return new WaitForSeconds(fade);
        }

        public IEnumerator OpenDialogue(float fade)
        {
            // dipanggil di dialoguetrigger dalam 2 kondisi:
            // 1. di dalam NextAutoDialogue()
            // 2. ketika isAutoConversation = false dan mau space for done
            yield return null;
            isRunningConversation = true;
            if (dialogueContainer.GetComponent<CanvasGroup>().alpha == 0f)
            {
                dialogueContainer.SetActive(true);
                dialogueContainer.GetComponent<FadeImage>().FadeInCanvasGroup(fade);
            }

            if (!isAutoConversation)
            {
                spaceForNext.SetActive(true);
                spaceForNextImage.GetComponent<FadeImage>().FadeIn(fade);
            }
            else
            {
                spaceForNext.SetActive(false);
            }

            // isRunningConversation = true;
        }

        private void Update()
        {
            if (SceneManager.GetActiveScene().name == "Main Menu") { return; }

            if (dialogueContainer == null)
            {
                // var mode = SceneManager.GetActiveScene().buildIndex > 0 ? LoadSceneMode.Additive : LoadSceneMode.Single;
                OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
            }

            SetCurrentDialogue(SceneManager.GetActiveScene(), LoadSceneMode.Single);

            // Ganti sprite normal jika sedang ada next line
            if (!isPlayingAnimation && spaceForNext.activeSelf)
            {
                if (conversationManager.HasNextLine || architect.isBuilding)
                {
                    if (spaceForNextImage.sprite != spaceForNextNormal)
                        spaceForNextImage.sprite = spaceForNextNormal;
                    activeOnce = false;
                }
                else // Sudah di akhir dialog
                {
                    if (!activeOnce)
                    {
                        activeOnce = true;
                        spaceForNextImage.GetComponent<FadeImage>()?.FadeOut(0.1f);
                        spaceForNextImage.sprite = spaceForDoneNormal;
                        spaceForNextImage.GetComponent<FadeImage>()?.FadeIn(0.1f);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && !isAutoConversation && isRunningConversation && !isPlayingAnimation && Time.time - lastSpaceTime > spaceCooldown)
            {
                lastSpaceTime = Time.time;
                if (conversationManager.HasNextLine)
                {
                    StartCoroutine(PlaySpaceForNext());
                    PlayerMovement.Instance.SetTriggerDialogue();
                }
                else if (!conversationManager.HasNextLine && !architect.isBuilding)
                {
                    StartCoroutine(PlaySpaceForDone());
                    PlayerMovement.Instance.SetTriggerStopDialogue();
                }
                OnDialogue_Next();
            }
        }

        public bool isRunningConversation;
        [SerializeField] private DialogueTrigger triggerAct_1;
        [SerializeField] private TextAsset currentDialogue;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            LoadComponents();
        }

        private void LoadComponents()
        {
            if (SceneManager.GetActiveScene().name == "Main Menu") { return; }

            GameObject mainCanvas = GameObject.Find("Canvas - Main");
            RectTransform dialogueGroup = mainCanvas.transform.Find("[8] - Dialogue") as RectTransform;

            dialogueContainer = dialogueGroup.Find("DialogueContainer").gameObject;
            dialogueText = dialogueContainer.transform.Find("DialogueText").GetComponent<TextMeshProUGUI>();
            dialogueShadow = dialogueContainer.transform.Find("DialogueShadow").gameObject;
            RectTransform shadowRect = dialogueShadow.GetComponent<RectTransform>();
            spaceForNext = dialogueContainer.transform.Find("Space ForNext").gameObject;
            spaceForNextImage = spaceForNext.GetComponent<Image>();

            // isInitialized = true;
            architect = new DialogueArchitect(dialogueText, shadowRect);
            conversationManager = new ConversationManager(architect);
        }

        private void SetCurrentDialogue(Scene scene, LoadSceneMode mode)
        {
            // Check specific scenes for dialogue trigger
            if (scene.name == "Act-1_Scene1_KamarIbu" || scene.name == "Act-1_Scene2_RuangTamu" || scene.name == "Act-1_Scene3_KamarMandi")
            {
                triggerAct_1 = GameObject.Find("DialogTrigger Act-1")?.GetComponent<DialogueTrigger>();
                currentDialogue = triggerAct_1.currentDialogue;
            }
        }

        [Header("--- On Press Space ---")]
        [SerializeField] private GameObject spaceForNext;
        [SerializeField] public GameObject dialogueShadow;
        [SerializeField] private Image spaceForNextImage;
        [SerializeField] private Sprite[] spaceForNextFrames;
        [SerializeField] private Sprite spaceForNextNormal;
        [SerializeField] private Sprite[] spaceForDoneFrames;
        [SerializeField] private Sprite spaceForDoneNormal;

        private bool isPlayingAnimation = false;
        private bool activeOnce = false;

        [SerializeField] private float spaceCooldown = 0.5f;
        private float lastSpaceTime = 0f;

        private IEnumerator PlaySpaceForNext()
        {
            if (!isPlayingAnimation)
            {
                isPlayingAnimation = true;
                for (int i = 0; i < spaceForNextFrames.Length; i++)
                {
                    spaceForNextImage.sprite = spaceForNextFrames[i];
                    yield return new WaitForSeconds(0.083f); // 12 FPS
                }
                isPlayingAnimation = false;
                spaceForNextImage.sprite = spaceForNextNormal;
            }
        }

        private IEnumerator PlaySpaceForDone()
        {
            if (!isPlayingAnimation)
            {
                isPlayingAnimation = true;
                for (int i = 0; i < spaceForDoneFrames.Length; i++)
                {
                    spaceForNextImage.sprite = spaceForDoneFrames[i];
                    yield return new WaitForSeconds(0.083f); // 12 FPS
                }
                isPlayingAnimation = false;
                spaceForNextImage.sprite = spaceForDoneNormal;
                dialogueContainer.GetComponent<FadeImage>().FadeOutCanvasGroup(0.4f);
            }
        }
    }
}