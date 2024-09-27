using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputsControl : MonoBehaviour
{
    public static PlayerInputsControl Instance;
    private Controls controls;
    
    private Vector2 moveDirection;
    private bool isMoving;
    private bool isAttacking;
    private bool isPressed;
    private bool isPausePressed;
    [HideInInspector] public bool inPause;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        PlayerMovement.Instance.JumpStart();
    }
    void OnJumpExit(InputAction.CallbackContext value)
    {
        PlayerMovement.Instance.JumpCanceled();
    }
    public void OnCollect(InputAction.CallbackContext value)
    {
        isPressed = value.ReadValueAsButton();
    }
    public void OnAttack(InputAction.CallbackContext value)
    {
        isAttacking = value.ReadValueAsButton();
        PlayerAttack.instance.Attack();
    }
    public void OnPause(InputAction.CallbackContext value)
    {
        isPausePressed = value.ReadValueAsButton();
        
        if (!inPause && isPausePressed)
        {
            PlayerAttack.instance.SetCanAttack(false);
            UIManager.Instance.pauseMenu.SetActive(true);
            inPause = true;
            Time.timeScale = 0;
        }
        else if(inPause && isPausePressed)
        {
            UIManager.Instance.audioSettings.SetActive(false);
            UIManager.Instance.pauseMenu.SetActive(false);
            PlayerAttack.instance.SetCanAttack(true);
            inPause = false;
            Time.timeScale = 1;
        }
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

        controls.Player.Pause.started += OnPause;
        controls.Player.Pause.canceled += OnPause;
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Move.started -= OnMove;
        controls.Player.Move.performed -= OnMove;
        controls.Player.Move.canceled -= OnMoveExit;

        controls.Player.Jump.started -= OnJump;
        controls.Player.Jump.performed -= OnJump;
        controls.Player.Jump.canceled -= OnJumpExit;

        controls.Player.Collect.started -= OnCollect;
        controls.Player.Collect.canceled -= OnCollect;
        
        controls.Player.Attack.started -= OnAttack;
        controls.Player.Attack.canceled -= OnAttack;

        controls.Player.Pause.started -= OnPause;
        controls.Player.Pause.canceled -= OnPause;
        
        controls.Disable();
    }
}
