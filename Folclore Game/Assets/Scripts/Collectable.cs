using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class Collectable : MonoBehaviour
{
    public static Collectable Instance;

    [SerializeField] private GameObject pressE;
    private bool inRange;
    private bool isPressed;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Collect();
    }

    public void OnCollect(InputAction.CallbackContext value)
    {
        isPressed = value.ReadValueAsButton();
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

    void Collect()
    {
        if (inRange && isPressed)
        {
            FirstQuestManager.Instance.AddIngredientCount();
            Destroy(gameObject);
        }
    }
}
