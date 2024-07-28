using UnityEngine;

public class CollectIngredient : CollectableBase
{
    public Ingredient ingredientType;

    protected override void Collect()
    {
        if (inRange && PlayerInputsControl.instance.GetIsPressed())
        {
            switch (ingredientType)
            {
                case Ingredient.FrogLeg:
                    FirstQuestManager.Instance.frogLeg = true;
                    break;
                case Ingredient.BatWing:
                    FirstQuestManager.Instance.batWing = true;
                    break;
                case Ingredient.Feather:
                    FirstQuestManager.Instance.frogLeg = true;
                    break;
            }

            FirstQuestManager.Instance.AddIngredientCount();
            Destroy(gameObject);
        }
    }
}

public enum Ingredient
{
    FrogLeg,
    BatWing,
    Feather
}
