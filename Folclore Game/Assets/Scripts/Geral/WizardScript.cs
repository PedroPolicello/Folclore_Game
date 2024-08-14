using TMPro;
using UnityEngine;

public class WizardScript : MonoBehaviour
{
    public static WizardScript instance;

    [SerializeField] private Sprite alquimista2;
    [SerializeField] private string[] puzzleTexts;

    private GameObject textFeedback;
    private bool isNearWizard;
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
        if (other.CompareTag("Player") && !FirstQuestManager.Instance.finishPuzzle1 && !FirstQuestManager.Instance.hasAllIngredients)
        {
            isNearWizard = true;
            textFeedback.GetComponent<TextMeshProUGUI>().text = puzzleTexts[0];
        }
        if (other.CompareTag("Player") && FirstQuestManager.Instance.hasAllIngredients && !FirstQuestManager.Instance.finishPuzzle1)
        {
            isNearWizard = true;
            textFeedback.GetComponent<TextMeshProUGUI>().text = puzzleTexts[1];
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearWizard = false;
            textFeedback.GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void ChangeSprite()
    {
        spriteRenderer.sprite = alquimista2;
    }

    public bool GetIsNearWizard()
    {
        return isNearWizard;
    }
}