using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class Collectable : MonoBehaviour
{
    public static Collectable Instance;
    private Controls controls;
    
    [SerializeField] private GameObject pressE;
    private bool inRange;
    private bool isPressed;

    private void Awake()
    {
        Instance = this;
        
        // controls = new Controls();
        // controls.Player.Collect.started += OnCollect;
        // controls.Player.Collect.canceled += OnCollect;
    }

    private void Update()
    {
        Collect();
    }

    public void OnCollect(InputAction.CallbackContext value)
    {
        isPressed = value.ReadValueAsButton();
    }

    void Collect()
    {
        if (inRange && isPressed)
        {
            FirstQuestManager.Instance.AddIngredientCount();
            gameObject.SetActive(false);
        }
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


    // private void OnEnable()
    // {
    //     controls.Enable();
    // }
    //
    // private void OnDisable()
    // {
    //     controls.Player.Collect.started -= OnCollect;
    //     controls.Player.Collect.canceled -= OnCollect;
    //
    //     controls.Disable();
    // }
}