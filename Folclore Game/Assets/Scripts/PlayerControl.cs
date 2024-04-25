using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance;
    private Controls controls;

    #region PlayerComponents

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    #endregion
    #region SerializedField Variables

    [Header("Movement Variables")] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float accelerationSpeed;

    [Header("Jump Variables")] [SerializeField]
    private float jumpForce;

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
        GetComponents();
    }
    void Update()
    {
        Move();
        FlipX();
    }
    void Move()
    {
        if (PlayerInputsControl.instance.GetIsMoving())
        {
            speed += accelerationSpeed * Time.deltaTime;
        }
        else
        {
            speed = 0;
        }

        transform.Translate(PlayerInputsControl.instance.GetMoveDirection() * (Mathf.Clamp(speed, 0, maxSpeed) * Time.deltaTime));
    }
    void FlipX()
    {
        if (PlayerInputsControl.instance.GetMoveDirection().x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (PlayerInputsControl.instance.GetMoveDirection().x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
    public bool IsGrounded()
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
    public Rigidbody2D GetRb()
    {
        return rb;
    }
    public float GetJumpForce()
    {
        return jumpForce;
    }
}