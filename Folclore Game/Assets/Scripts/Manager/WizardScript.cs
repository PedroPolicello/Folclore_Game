using UnityEngine;

public class WizardScript : MonoBehaviour
{
    public static WizardScript instance;

    [SerializeField] private Sprite alquimista2;
    private bool isNearWizard;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearWizard = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearWizard = false;
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
