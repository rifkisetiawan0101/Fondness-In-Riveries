using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DIALOGUE;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); 
            return;
        }
        else Instance = this;
        DontDestroyOnLoad(gameObject);
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    [SerializeField] private float moveSpeed = 500f;
    [SerializeField] private float normalMoveSpeed = 250f;
    [SerializeField] private float reducedMoveSpeed = 200f;
    private Vector2 movement;
    private Rigidbody2D rb;

    [Header("---Animator---")]
    private Animator animator;
    public bool isMakMoving;
    public bool isMakReach;
    public bool isMakCrouching;
    public bool isCarryBaby;
    public bool isCarryBottle;
    public bool isDialogue;

    [Header("---Cooldown---")]
    [SerializeField] private float walkCooldown = 2f; 
    private float walkCooldownTimer = 0f;
    private bool wasDialogue = false;
    private bool wasReaching = false;
    private bool wasCrouching = false;
    private bool wasCarryingBaby = false;
    private bool wasWalking = false;

    [Header("---Camera---")]
    [SerializeField] private float cameraOffsetSmoothTime = 0.3f;
    private float currentCameraOffset = 0f;
    private float cameraOffsetVelocity = 0f;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [Header("---Managers---")]
    [SerializeField] private MechanicsManager mechanicsManager;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private ToDoList toDoList;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        mechanicsManager = FindObjectOfType<MechanicsManager>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        toDoList = FindObjectOfType<ToDoList>();

        if (virtualCamera != null)
        {
            virtualCamera.Follow = transform;
        }


        if (SceneManager.GetActiveScene().name == "Act-1_Scene2_KamarIbu")
        {
            isCarryBaby = true;
            PlayerData playerData = new PlayerData(1);
            playerData.SetPlayerPosition(this);
        }
    }

    private void Update()
    {
        if (virtualCamera == null)
        {
            var mode = SceneManager.GetActiveScene().buildIndex > 0 ? LoadSceneMode.Additive : LoadSceneMode.Single;
            OnSceneLoaded(SceneManager.GetActiveScene(), mode);
        }

        if (SceneManager.GetActiveScene().name == "Main Menu") { return; }

        animator.SetBool("isMakReach", isMakReach);
        animator.SetBool("isMakCrouching", isMakCrouching);
        animator.SetBool("isMakMoving", isMakMoving);
        animator.SetBool("isCarryBaby", isCarryBaby);
        animator.SetBool("isDialogue", isDialogue);
        isDialogue = DialogueManager.instance.isRunningConversation;

        if (mechanicsManager.isGameStart && !mechanicsManager.isOpenMechanic && !pauseMenu.isMenuActive && !toDoList.isTDLOpen)
        {
            if (!isMakReach && !isMakCrouching && !isCarryBaby) // kondisi jalan normal
            {
                if (movement != Vector2.zero)
                {
                    isMakMoving = true;
                    isMakReach = false;
                    isMakCrouching = false;
                    animator.SetFloat("Horizontal", movement.x);
                    animator.SetFloat("LastHorizontal", movement.x);
                }
                else
                {
                    isMakMoving = false;
                    animator.SetFloat("Horizontal", 0); // Set ke Idle
                }
            }
            else if (isMakReach || isMakCrouching || isCarryBaby)
            {
                if (movement != Vector2.zero)
                {
                    isMakMoving = true;
                    animator.SetFloat("Horizontal", movement.x);
                    animator.SetFloat("LastHorizontal", movement.x);
                }
                else
                {
                    isMakMoving = false;
                    animator.SetFloat("Horizontal", 0); // Set ke Idle
                }
            }

            var framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            float targetOffset = 0f;
            float lastHorizontal = animator.GetFloat("LastHorizontal");

            if (lastHorizontal > 0)
            {
                targetOffset = 200f;
            }
            else if (lastHorizontal < 0)
            {
                targetOffset = -200f;
            }

            currentCameraOffset = Mathf.SmoothDamp(currentCameraOffset, targetOffset, ref cameraOffsetVelocity, cameraOffsetSmoothTime);
            framingTransposer.m_TrackedObjectOffset.x = currentCameraOffset;
        }

        if (!mechanicsManager.isGameStart || mechanicsManager.isOpenMechanic || pauseMenu.isMenuActive || toDoList.isTDLOpen)
        {
            movement = Vector2.zero;
            isMakMoving = false;
            animator.SetFloat("Horizontal", 0);
        }

        if (wasDialogue && !isDialogue) { walkCooldownTimer = walkCooldown; } // mulai cooldown
        wasDialogue = isDialogue;
        if (walkCooldownTimer > 0f) { walkCooldownTimer -= Time.deltaTime; }

        // Periksa perubahan kondisi untuk mengubah moveSpeed
        bool isSpecialCondition = isMakReach || isMakCrouching || isCarryBaby;
        bool wasSpecialCondition = wasReaching || wasCrouching || wasCarryingBaby;

        if (isSpecialCondition != wasSpecialCondition)
        {
            // Kondisi berubah, update moveSpeed sekali saja
            moveSpeed = isSpecialCondition ? reducedMoveSpeed : normalMoveSpeed;

            // Update status sebelumnya
            wasReaching = isMakReach;
            wasCrouching = isMakCrouching;
            wasCarryingBaby = isCarryBaby;
        }
        
        bool nowWalking = movement != Vector2.zero && mechanicsManager.isGameStart && !mechanicsManager.isOpenMechanic && !pauseMenu.isMenuActive && !toDoList.isTDLOpen;

        if (nowWalking)
        {
            // Jika belum play atau sudah selesai, play lagi
            if (!wasWalking || !AudioManager.Instance.SFXSource.isPlaying)
            {
                AudioManager.Instance.SFXSource.PlayOneShot(AudioManager.Instance.makWalking);
                wasWalking = true;
            }
        }
        else
        {
            wasWalking = false;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void SetTriggerDialogue()
    {
        if (animator != null)
        {
            animator = GetComponent<Animator>();
            animator.SetTrigger("OnDialogue");
            Debug.Log("Set Trigger Dialogue");
        }
    }

    public void SetTriggerStopDialogue()
    {
        if (animator != null)
        {
            animator = GetComponent<Animator>();
            animator.SetTrigger("OnStopDialogue");
            Debug.Log("Set Trigger Stop Dialogue");
        }
    }

    private void OnWalk(InputValue value)
    {
        if (walkCooldownTimer > 0f)
        {
            movement = Vector2.zero;
            return;
        }

        if (mechanicsManager.isGameStart && !mechanicsManager.isOpenMechanic && !pauseMenu.isMenuActive && !toDoList.isTDLOpen)
        {
            movement = value.Get<Vector2>();
        }
        else
        {
            movement = Vector2.zero;
        }
    }


    // ### Pending Dulu ###
    // private void OnReach(InputValue value)
    // {
    //     bool isReachPressed = value.isPressed;

    //     var framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    //     if (!isMakMoving && isReachPressed)
    //     {
    //         isMakReach = true;
    //         isMakCrouching = false;
    //         framingTransposer.m_ScreenY = 0.6f;
    //         transform.position = new Vector2(transform.position.x, 50);
    //     }
    // }

    // private void OnCrouching(InputValue value)
    // {
    //     bool isCrouchPressed = value.isPressed;

    //     var framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    //     if (!isMakMoving && isCrouchPressed)
    //     {
    //         isMakCrouching = true;
    //         isMakReach = false;
    //         framingTransposer.m_ScreenY = 0.4f;
    //         transform.position = new Vector2(transform.position.x, -50);
    //     }
    // }

    private void OnNormal(InputValue value)
    {
        bool isNormalPressed = value.isPressed;

        var framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        if ((isMakReach == true || isMakCrouching == true) && isNormalPressed)
        {
            isMakReach = false;
            isMakCrouching = false;
            framingTransposer.m_ScreenY = 0.5f;
            transform.position = new Vector2(transform.position.x, 0);
        }
    }
}
