using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 100f;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public bool isMakMoving = false;
    public bool isMakTiptoe = false;
    public bool isMakCrouching = false;

    [SerializeField] private Sprite makTiptoe;
    [SerializeField] private Sprite makCrouching;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() 
    {
        
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
            if (!isMakTiptoe && !isMakCrouching)
            {
                animator.SetFloat("Horizontal", 0); // Set ke Idle
            }
        }

        animator.SetBool("isMakTiptoe", isMakTiptoe);
        animator.SetBool("isMakCrouching", isMakCrouching);

        // Kondisi Tiptoe
        if (!isMakMoving && Input.GetKeyDown(KeyCode.W))
        {
            isMakTiptoe = true;
            isMakCrouching = false;
            spriteRenderer.sprite = makTiptoe;
        }

        // Kondisi Crouch
        if (!isMakMoving && Input.GetKeyDown(KeyCode.S))
        {
            isMakCrouching = true;
            isMakTiptoe = false;
            spriteRenderer.sprite = makCrouching;
        }
    }

    private void FixedUpdate() 
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
