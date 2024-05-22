using TMPro;
using UnityEngine;

public class WizardScript : MonoBehaviour
{
    public static WizardScript instance;

    [SerializeField] private Sprite alquimista2;
    [SerializeField] private GameObject textFeedback;
    [SerializeField] private string[] puzzleTexts;

    private bool isNearWizard;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !FirstQuestManager.Instance.finishPuzzle1 && !FirstQuestManager.Instance.hasAllIngredients)
        {
            isNearWizard = true;
            textFeedback.GetComponent<TextMeshProUGUI>().text = puzzleTexts[0];
            textFeedback.SetActive(true);
        }
        if (other.CompareTag("Player") && FirstQuestManager.Instance.hasAllIngredients && !FirstQuestManager.Instance.finishPuzzle1)
        {
            isNearWizard = true;
            textFeedback.GetComponent<TextMeshProUGUI>().text = puzzleTexts[1];
            textFeedback.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearWizard = false;
            textFeedback.SetActive(false);
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