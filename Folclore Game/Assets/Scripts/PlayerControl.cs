using System;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private static PlayerControl Instance;
    private Controls controls;

    #region PlayerComponents

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    #endregion

    #region Movement Variables

    private Vector2 moveDirection;
    private bool isMoving;
    private bool isJumping;

    #endregion

    #region SerializedField Variables

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    #endregion

    private float lastGroundedTime;
    private float lastJumpTime;
    
    [SerializeField] private float jumpCoyoteTime;
    [SerializeField] private float jumpBufferTime;
    void Awake()
    {
        #region Singleton

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        #endregion

        SetInput();
        GetComponents();
    }

    private void Start()
    {
        lastJumpTime = jumpBufferTime;
        lastGroundedTime = jumpCoyoteTime;
    }

    void OnMove(InputAction.CallbackContext value)
    {
        moveDirection = value.ReadValue<Vector2>();
    }

    void OnJump(InputAction.CallbackContext value)
    {
        isJumping = value.ReadValueAsButton();
        
        if (isJumping && lastGroundedTime > 0 && lastJumpTime > 0)
        {
            Jump();
        }
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(moveDirection * (speed * Time.deltaTime));
    }


    void CheckJumpTimers()
    {
        lastGroundedTime -= Time.deltaTime;
        lastJumpTime -= Time.deltaTime;

        if(lastJumpTime <= 0)lastJumpTime = 0;
        if (lastGroundedTime <= 0) lastGroundedTime = 0;
        
        print(lastGroundedTime);
        print(lastJumpTime);
    }
    void Jump()
    {
        rb.AddForce(Vector2.up * (jumpForce * 100));
        lastJumpTime = jumpBufferTime;
        CheckJumpTimers();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            lastGroundedTime = jumpCoyoteTime;
            CheckJumpTimers();
        }
    }


    void SetInput()
    {
        controls = new Controls();

        controls.Player.Move.started += OnMove;
        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMove;

        controls.Player.Jump.started += OnJump;
        controls.Player.Jump.performed += OnJump;
        controls.Player.Jump.canceled += OnJump;
    }

    void GetComponents()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Move.started -= OnMove;
        controls.Player.Move.performed -= OnMove;
        controls.Player.Move.canceled -= OnMove;
        controls.Disable();
    }
}