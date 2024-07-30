using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    public static CollectionManager Instance;

    public int ingredientCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        print(ingredientCount);
        if (ingredientCount >= 3)
        {
            FirstQuestManager.Instance.hasAllIngredients = true;
        }
    }

    public void AddIngredientCount()
    {
        ingredientCount++;
    }
}