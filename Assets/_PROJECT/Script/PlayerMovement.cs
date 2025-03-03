using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // Menghindari duplikasi instance
            return;
        } else Instance = this;
    }

    [SerializeField] private float moveSpeed = 500f;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    public bool isMakMoving = false;
    public bool isMakTiptoe = false;
    public bool isMakCrouching = false;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private MechanicsManager mechanicsManager;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private ToDoList toDoList;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mechanicsManager = FindObjectOfType<MechanicsManager>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        toDoList = FindObjectOfType<ToDoList>();
    }

    private void Update() 
    {
        animator.SetBool("isMakTiptoe", isMakTiptoe);
        animator.SetBool("isMakCrouching", isMakCrouching);
        animator.SetBool("isMakMoving", isMakMoving);
        
        if (mechanicsManager.isGameStart && !mechanicsManager.isOpenMechanic && !pauseMenu.isMenuActive && !toDoList.isTDLOpen)
        {
            if (!isMakTiptoe && !isMakCrouching) // kondisi jalan normal
            {
                moveSpeed = 500f;
                MovementAD();
                if (movement != Vector2.zero)
                {    
                    isMakMoving = true;
                    isMakTiptoe = false;
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
            else if (isMakTiptoe || isMakCrouching)
            {   
                moveSpeed = 200f;
                MovementAD();
                if (movement != Vector2.zero)
                {
                    animator.SetFloat("Horizontal", movement.x);
                    animator.SetFloat("LastHorizontal", movement.x);
                }
                else
                {
                    animator.SetFloat("Horizontal", 0); // Set ke Idle
                }
            }

            var framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            
            if ((isMakTiptoe == true || isMakCrouching == true) && Input.GetKeyDown(KeyCode.LeftShift)) 
            {
                isMakTiptoe = false;
                isMakCrouching = false;
                framingTransposer.m_ScreenY = 0.5f;
                transform.position = new Vector2(transform.position.x, 0);
            }

            // Kondisi Tiptoe
            if (!isMakMoving && Input.GetKeyDown(KeyCode.W))
            {
                isMakTiptoe = true;
                isMakCrouching = false;
                framingTransposer.m_ScreenY = 0.6f;
                transform.position = new Vector2(transform.position.x, 250);
            }

            // Kondisi Crouch
            if (!isMakMoving && Input.GetKeyDown(KeyCode.S))
            {
                isMakCrouching = true;
                isMakTiptoe = false;
                framingTransposer.m_ScreenY = 0.4f;
                transform.position = new Vector2(transform.position.x, -250);
            }
        }
    }

    private void FixedUpdate() 
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void MovementAD()
    {
        if (Input.GetKey(KeyCode.A)) { movement.x = -1; }
        else if (Input.GetKey(KeyCode.D)) { movement.x = 1; }
        else { movement.x = 0; }
    }
}
