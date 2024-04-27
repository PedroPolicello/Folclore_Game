using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class Collectable : MonoBehaviour
{
    public static Collectable Instance;
    private Controls controls;
    
    [SerializeField] private GameObject pressE;
    private bool inRange;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        Collect();
    }
    
    void Collect()
    {
        if (inRange && PlayerInputsControl.instance.GetIsPressed())
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
}