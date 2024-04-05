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

    [Header("Movement Variables")]
    [SerializeField] private float maxSpeed;

    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float decelerationSpeed;
    private float speed;

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
            speed += Time.deltaTime * accelerationSpeed;
        }
        else
        {
            speed = 0;
            // switch (moveDirection.x)
            // {
            //     case  > 0:
            //         speed -= Time.deltaTime * decelerationSpeed;
            //         rb.velocity = new Vector2(1, 0 * (speed * Time.deltaTime));
            //         break;
            //     
            //     case < 0:
            //         speed -= Time.deltaTime * decelerationSpeed;
            //         rb.velocity = new Vector2(-1, 0 * (speed * Time.deltaTime));
            //         break;
            //     
            //     default:
            //         speed = 0;
            //         break;
            // }
        }

        if (speed >= maxSpeed)
        {
            speed = maxSpeed;
        }
        else if (speed <= 0)
        {
            speed = 0;
        }

        transform.Translate(moveDirection * (speed * Time.deltaTime));
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
}