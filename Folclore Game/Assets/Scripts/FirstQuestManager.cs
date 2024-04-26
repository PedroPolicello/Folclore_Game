using System;
using UnityEngine;

public class FirstQuestManager : MonoBehaviour
{
    public static FirstQuestManager Instance;

    private int ingredientCount;
    private bool hasAllIngredients;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        HasAllIngredients();
        DeliverQuest();
    }

    void DeliverQuest()
    {
        if (HasAllIngredients() && WizardScript.instance.GetIsNearWizard() && PlayerInputsControl.instance.GetIsPressed())
        {
            print("Primeira carta recebida!");
            //Instancia Carta Nº1
        }
    }

    bool HasAllIngredients()
    {
        hasAllIngredients = ingredientCount == 4;
        return hasAllIngredients;
    }

    public void AddIngredientCount()
    {
        ingredientCount ++;
    }


}
