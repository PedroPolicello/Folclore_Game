using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputsControl : MonoBehaviour
{
    public static PlayerInputsControl instance;
    private Controls controls;
    
    private Vector2 moveDirection;
    private bool isMoving;
    private bool isJumping;
    private bool isAttacking;
    private bool isPressed;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        SetInput();
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
        PlayerMovement.Instance.JumpStart();
    }
    void OnJumpExit(InputAction.CallbackContext value)
    {
        isJumping = value.ReadValueAsButton();
        PlayerMovement.Instance.JumpCanceled();
    }
    public void OnCollect(InputAction.CallbackContext value)
    {
        isPressed = value.ReadValueAsButton();
        print(isPressed);
    }
    public void OnAttack(InputAction.CallbackContext value)
    {
        isAttacking = value.ReadValueAsButton();
        PlayerAttack.instance.Attack();
    }
    public bool GetIsMoving()
    {
        return isMoving;
    }
    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }
    public bool GetIsPressed()
    {
        return isPressed;
    }
    public bool GetIsAttacking()
    {
        return isAttacking;
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

        controls.Player.Collect.started += OnCollect;
        controls.Player.Collect.canceled += OnCollect;
        
        controls.Player.Attack.started += OnAttack;
        controls.Player.Attack.canceled += OnAttack;
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

        controls.Player.Collect.started -= OnCollect;
        controls.Player.Collect.canceled -= OnCollect;
        
        controls.Player.Attack.started -= OnAttack;
        controls.Player.Attack.canceled -= OnAttack;
        
        controls.Disable();
    }
}
