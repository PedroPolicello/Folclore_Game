using UnityEngine;
using UnityEngine.InputSystem;

public class Collectable : MonoBehaviour
{
    public static Collectable Instance;
    private Controls controls;
    
    [SerializeField] private GameObject pressE;
    private bool inRange;
    private bool pressed;

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
    }

    private void Update()
    {
        SetInput();
    }
    
    private void OnCollect(InputAction.CallbackContext value)
    {
        pressed = value.ReadValueAsButton();
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pressE.SetActive(true);
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pressE.SetActive(false);
            inRange = false;
        }
    }

    public bool IsPressed()
    {
        return pressed;
    }

    public bool InRange()
    {
        return inRange;
    }

    public string ObjTag()
    {
        return gameObject.tag;
    }
    
    
    private void SetInput()
    {
        controls = new Controls();

        controls.Player.Collect.started += OnCollect;
        controls.Player.Collect.canceled += OnCollect;
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Collect.started -= OnCollect;
        controls.Player.Collect.canceled -= OnCollect;
        
        controls.Disable();
    }
}
