using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputsControl : MonoBehaviour
{
    public static PlayerInputsControl instance;
    private Controls controls;
    
    private Vector2 moveDirection;
    private bool isMoving;
    private bool isJumping;
    private bool isPressed;
    
    private void Awake()
    {
        instance = this;
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
        if (PlayerControl.Instance.IsGrounded())
        {
            PlayerControl.Instance.GetRb().velocity = new Vector2(PlayerControl.Instance.GetRb().velocity.x, PlayerControl.Instance.GetJumpForce());
        }
    }
    void OnJumpExit(InputAction.CallbackContext value)
    {
        isJumping = value.ReadValueAsButton();
        PlayerControl.Instance.GetRb().velocity = new Vector2(PlayerControl.Instance.GetRb().velocity.x, PlayerControl.Instance.GetRb().velocity.y * 0.5f);
    }
    public void OnCollect(InputAction.CallbackContext value)
    {
        isPressed = value.ReadValueAsButton();
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
        
        controls.Disable();
    }
}
