using TMPro;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    public static CollectableBase Instance;
    
    [SerializeField] private GameObject textFeedback;
    [SerializeField] private string text;
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
            textFeedback.GetComponent<TextMeshProUGUI>().text = text;
            textFeedback.SetActive(true);
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textFeedback.SetActive(false);
            inRange = false;
        }
    }
}