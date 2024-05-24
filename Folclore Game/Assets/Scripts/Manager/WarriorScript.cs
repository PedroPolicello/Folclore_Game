using TMPro;
using UnityEngine;

public class WarriorScript : MonoBehaviour
{
    public static WarriorScript instance;

    [SerializeField] private Sprite guerreiro2;
    [SerializeField] private string[] puzzleTexts;
    private GameObject textFeedback;

    private bool isNearWarrior;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        textFeedback = GameObject.FindGameObjectWithTag("textFeedback");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearWarrior = true;
            textFeedback.GetComponent<TextMeshProUGUI>().text = puzzleTexts[0];
            textFeedback.SetActive(true);
        }
        //if (other.CompareTag("Player") && SecondQuestManager.instance.killCount)
        //{
        //    isNearWarrior = true;
        //    textFeedback.GetComponent<TextMeshProUGUI>().text = puzzleTexts[0];
        //    textFeedback.SetActive(true);
        //}
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearWarrior = false;
            textFeedback.SetActive(false);
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
