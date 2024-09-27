using TMPro;
using UnityEngine;

public class WarriorScript : MonoBehaviour
{
    public static WarriorScript instance;

    [SerializeField] private Sprite guerreiro2;
    [SerializeField] private string[] puzzleTexts;
    [SerializeField] private GameObject pressE;

    private GameObject textFeedback;
    private bool isNearWarrior;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        textFeedback = GameObject.FindGameObjectWithTag("textFeedback");
        textFeedback.GetComponent<TextMeshProUGUI>().text = "";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearWarrior = true;
            pressE.SetActive(true);
            textFeedback.GetComponent<TextMeshProUGUI>().text = puzzleTexts[0];
        }
        if (other.CompareTag("Player") && SecondQuestManager.instance.killAllEnemies)
        {
            isNearWarrior = true;
            pressE.SetActive(true);
            textFeedback.GetComponent<TextMeshProUGUI>().text = puzzleTexts[1];
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearWarrior = false;
            pressE.SetActive(false);
            textFeedback.GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void ChangeSprite()
    {
        spriteRenderer.sprite = guerreiro2;
    }

    public bool GetIsNearWarrior()
    {
        return isNearWarrior;
    }
}
