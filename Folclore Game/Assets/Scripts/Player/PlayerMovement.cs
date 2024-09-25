using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    private Controls controls;

    #region PlayerComponents

    private Animator animator;
    private Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    #endregion
    #region SerializedField Variables

    [Header("Movement Variables")] 
    private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float accelerationSpeed;
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool canJump = true;
    [HideInInspector] public bool canFlip = true;

    [Header("Jump Variables")] 
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask groundLayer;

    #endregion

    void Awake()
    {
        Instance = this;
        GetComponents();
    }
    void Update()
    {
        Move();
        FlipX();
        SetAnimations();
    }
    void Move()
    {
        if (PlayerInputsControl.Instance.GetIsMoving() && canMove)
        {
            speed += accelerationSpeed * Time.deltaTime;
        }
        else
        {
            speed = 0;
        }

        transform.Translate(PlayerInputsControl.Instance.GetMoveDirection() * (Mathf.Clamp(speed, 0, maxSpeed) * Time.deltaTime));
    }
    void FlipX()
    {
        if (PlayerInputsControl.Instance.GetMoveDirection().x > 0 && canFlip)
        {
            spriteRenderer.flipX = false;
        }
        else if (PlayerInputsControl.Instance.GetMoveDirection().x < 0 && canFlip)
        {
            spriteRenderer.flipX = true;
        }
    }
    public void JumpStart()
    {
        if (IsGrounded() && canJump)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = SoundManager.Instance.sFXVolume.value/10;
            audioSource.PlayOneShot(SoundManager.Instance.jump);
            Destroy(audioSource);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("isJumping", true);
        }
    }
    public void JumpCanceled()
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
    }
    public bool IsGrounded()
    {
        bool isGrounded = Physics2D.OverlapBox(groundCheckPos.position, new Vector2(1.5f, 0.8f), 0, groundLayer);
        return isGrounded;
    }

    private void SetAnimations()
    {
        if (speed != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        
        if (rb.velocity.y < 0f)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }
        else if (rb.velocity.y == 0)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
    }

    public void SetPlayerStatic(bool isStatic)
    {
        if (isStatic)
        {
            canMove = false;
            canFlip = false;
            canJump = false;
        }
        else
        {
            canMove = true;
            canFlip = true;
            canJump = true;
        }
    }
    
    void GetComponents()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}