using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    public static CollectableBase Instance;
    private Controls controls;
    
    [SerializeField] private GameObject pressE;
    [HideInInspector] public bool inRange;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        Collect();
    }
    
    protected virtual void Collect()
    {
        if (inRange && PlayerInputsControl.instance.GetIsPressed())
        {
            Destroy(gameObject);
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