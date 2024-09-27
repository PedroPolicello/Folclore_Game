using TMPro;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    public static CollectableBase Instance;

    private GameObject textFeedback;
    [SerializeField] private string text;
    [SerializeField] private GameObject pressE;
    [HideInInspector] public bool inRange;

    private void Awake()
    {
        Instance = this;
        textFeedback = GameObject.FindGameObjectWithTag("textFeedback");
        textFeedback.GetComponent<TextMeshProUGUI>().text = "";
    }

    private void Update()
    {
        Collect();
    }

    protected virtual void Collect()
    {
        if (inRange && PlayerInputsControl.Instance.GetIsPressed())
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textFeedback.GetComponent<TextMeshProUGUI>().text = text;
            pressE.SetActive(true);
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textFeedback.GetComponent<TextMeshProUGUI>().text = "";
            pressE.SetActive(false);
            inRange = false;
        }
    }
}