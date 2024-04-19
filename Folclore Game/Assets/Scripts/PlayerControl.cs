using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance;
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
    private bool isDashing;

    #endregion

    #region SerializedField Variables

    [Header("Movement Variables")]
    private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float accelerationSpeed;

    [Header("Jump Variables")]
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask groundLayer;

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
    }

    private void Start()
    {
        SetInput();
        GetComponents();
    }

    void OnMove(InputAction.CallbackContext value)
    {
        moveDirection = value.ReadValue<Vector2>();
        isMoving = true;
    }

    void OnMoveExit(InputAction.CallbackContext value)
    {
        moveDirection = value.ReadValue<Vector2>();
        isMoving = false;
    }

    void OnJump(InputAction.CallbackContext value)
    {
        isJumping = value.ReadValueAsButton();
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void OnJumpExit(InputAction.CallbackContext value)
    {
        isJumping = value.ReadValueAsButton();
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (isMoving)
        {
            speed += accelerationSpeed * Time.deltaTime;
        }
        else
        {
            speed = 0;
        }

        transform.Translate(moveDirection * (Mathf.Clamp(speed, 0, maxSpeed) * Time.deltaTime));
    }

    private bool IsGrounded()
    {
        bool isGrounded = Physics2D.OverlapBox(groundCheckPos.position, new Vector2(1.5f, 0.8f), 0, groundLayer);
        return isGrounded;
    }

    private void OnDrawGizmos()
    {
        if (IsGrounded())
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawWireCube(groundCheckPos.position, new Vector3(1.5f, 0.8f));
    }

    void GetComponents()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        controls.Player.Collect.started += Collectable.Instance.OnCollect;
        controls.Player.Collect.canceled += Collectable.Instance.OnCollect;
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

        controls.Player.Jump.started -= OnJump;
        controls.Player.Jump.performed -= OnJump;
        controls.Player.Jump.canceled -= OnJumpExit;

        controls.Player.Collect.started -= Collectable.Instance.OnCollect;
        controls.Player.Collect.canceled -= Collectable.Instance.OnCollect;

        controls.Disable();
    }
}