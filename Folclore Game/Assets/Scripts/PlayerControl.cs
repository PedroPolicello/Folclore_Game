using UnityEngine;
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

    [Header("Movement Variables")] [SerializeField]
    private float maxSpeed;

    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float decelerationSpeed;
    private float speed;

    [Header("Jump Variables")] [SerializeField]
    private float jumpForce;

    [SerializeField] private float coyoteTime;
    private float coyoteTimeCounter;

    [SerializeField] private float jumpBufferTime;
    private float jumpBufferCounter;

    #endregion


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

    void OnMove(InputAction.CallbackContext value)
    {
        moveDirection = value.ReadValue<Vector2>();
    }

    void OnMoveExit(InputAction.CallbackContext value)
    {
        Decelerate();
        moveDirection = value.ReadValue<Vector2>();
    }

    void OnJump(InputAction.CallbackContext value)
    {
        isJumping = value.ReadValueAsButton();
        if (coyoteTimeCounter > 0f && isJumping)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce * 100));
        }
    }

    void OnJumpExit(InputAction.CallbackContext value)
    {
        isJumping = value.ReadValueAsButton();
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        coyoteTimeCounter = 0f;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        speed += Time.deltaTime * accelerationSpeed;
        transform.Translate(moveDirection * (speed * Time.deltaTime));

        if (speed >= maxSpeed)
        {
            speed = maxSpeed;
        }

        if (moveDirection.x == 0)
        {
            speed = 0;
        }
    }

    void Decelerate()
    {
        if (moveDirection.x > 0) rb.AddForce(new Vector2(decelerationSpeed * 10, rb.velocity.y));
        if (moveDirection.x < 0) rb.AddForce(new Vector2(decelerationSpeed * -10, rb.velocity.y));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            coyoteTimeCounter = coyoteTime;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            coyoteTimeCounter = coyoteTime;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }


    void SetInput()
    {
        controls = new Controls();

        controls.Player.Move.started += OnMove;
        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMoveExit;

        controls.Player.Jump.started += OnJump;
        controls.Player.Jump.performed += OnJump;
        controls.Player.Jump.canceled += OnJumpExit;
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