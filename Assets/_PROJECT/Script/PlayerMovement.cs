using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 500f;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public bool isMakMoving = false;
    public bool isMakTiptoe = false;
    public bool isMakCrouching = false;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private MechanicsManager mechanicsManager;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mechanicsManager = FindObjectOfType<MechanicsManager>();
    }

    private void Update() 
    {
        animator.SetBool("isMakTiptoe", isMakTiptoe);
        animator.SetBool("isMakCrouching", isMakCrouching);
        animator.SetBool("isMakMoving", isMakMoving);
        
        if (!isMakTiptoe && !isMakCrouching && !mechanicsManager.isOpenMechanic) // kondisi jalan normal
        {
            moveSpeed = 500f;
            movement.x = Input.GetAxisRaw("Horizontal");
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
        else if (isMakTiptoe || isMakCrouching && !mechanicsManager.isOpenMechanic)
        {   
            moveSpeed = 200f;
            movement.x = Input.GetAxisRaw("Horizontal");
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
            transform.position = new Vector2(transform.position.x, 50);
        }

        // Kondisi Crouch
        if (!isMakMoving && Input.GetKeyDown(KeyCode.S))
        {
            isMakCrouching = true;
            isMakTiptoe = false;
            framingTransposer.m_ScreenY = 0.4f;
            transform.position = new Vector2(transform.position.x, -50);
        }
    }

    private void FixedUpdate() 
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
