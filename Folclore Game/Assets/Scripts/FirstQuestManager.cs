using UnityEngine;

public class FirstQuestManager : MonoBehaviour
{
    public static FirstQuestManager Instance;

    private int ingredientCount;
    private bool hasAllIngredients;
    private bool isNearWizard;

    private void Awake()
    {
        Instance = this;
    }

    void DeliverQuest()
    {
        if (HasAllIngredients() && isNearWizard)
        {
            //Instancia Carta Nº1
        }
    }

    bool HasAllIngredients()
    {
        hasAllIngredients = ingredientCount >= 3;
        return hasAllIngredients;
    }

    public void AddIngredientCount()
    {
        ingredientCount ++;
    }
}
