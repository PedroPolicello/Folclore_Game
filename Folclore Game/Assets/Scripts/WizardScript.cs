using UnityEngine;

public class WizardScript : MonoBehaviour
{
    public static WizardScript instance;
    
    private bool isNearWizard;

    private void Awake()
    {
        instance = this;
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

    public bool GetIsNearWizard()
    {
        return isNearWizard;
    }
}
