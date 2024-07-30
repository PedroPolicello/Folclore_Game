using UnityEngine;

public class CollectIngredient : CollectableBase
{
    public Ingredient ingredientType;

    protected override void Collect()
    {
        if (inRange && PlayerInputsControl.instance.GetIsPressed())
        {
            CollectionManager.Instance.AddIngredientCount();
            Destroy(gameObject);
            // switch (ingredientType)
            // {
            //     case Ingredient.FrogLeg:
            //         FirstQuestManager.Instance.frogLeg = true;
            //         break;
            //     case Ingredient.BatWing:
            //         FirstQuestManager.Instance.batWing = true;
            //         break;
            //     case Ingredient.Feather:
            //         FirstQuestManager.Instance.frogLeg = true;
            //         break;
            // }
        }
    }
}

public enum Ingredient
{
    FrogLeg,
    BatWing,
    Feather
}
