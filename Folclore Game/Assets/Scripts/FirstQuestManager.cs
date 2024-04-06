using UnityEngine;

public class FirstQuestManager : MonoBehaviour
{
    private int ingredientCount;
    private bool hasAllIngredients;
    private bool isNearWizard;

    void Update()
    {
        Collect();
    }

    void DeliverQuest()
    {
        if (HasAllIngredients() && isNearWizard)
        {
            //Instancia Carta Nº1
        }
    }

    void Collect()
    {
        if (Collectable.Instance.InRange() && 
            Collectable.Instance.ObjTag() == "Ingredient" &&
            Collectable.Instance.IsPressed())
        {
            ingredientCount++;
            Destroy(Collectable.Instance.gameObject);
        }
    }

    bool HasAllIngredients()
    {
        hasAllIngredients = ingredientCount >= 3;
        return hasAllIngredients;
    }
}
