using TMPro;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    public static CollectableBase Instance;

    private GameObject textFeedback;
    [SerializeField] private string text;
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
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = SoundManager.Instance.sFXVolume.value/10;
            audioSource.PlayOneShot(SoundManager.Instance.collect);
            Destroy(audioSource);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textFeedback.GetComponent<TextMeshProUGUI>().text = text;
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textFeedback.GetComponent<TextMeshProUGUI>().text = "";
            inRange = false;
        }
    }
}