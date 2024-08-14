using UnityEngine;

public class ActiveIngredients : MonoBehaviour
{
    public GameObject ingredient;

    private void Awake()
    {
        ingredient = GameObject.Find("Ingredient");
        ingredient.SetActive(false);
    }

    void Update()
    {
        if (FirstQuestManager.Instance.activeIngredient)
        {
            ingredient.SetActive(true);
        }
    }
}