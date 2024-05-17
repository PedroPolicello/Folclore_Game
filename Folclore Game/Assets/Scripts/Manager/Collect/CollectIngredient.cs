using UnityEngine;

public class CollectIngredient : CollectableBase
{
    [SerializeField] private LayerMask ingredientType;

    protected override void Collect()
    {
        if (inRange && PlayerInputsControl.instance.GetIsPressed())
        {
            FirstQuestManager.Instance.AddIngredientCount();
            switch (ingredientType.ToString())
            {
                case "FrogLeg":
                    FirstQuestManager.Instance.frogLeg = true;
                    break;
                case "BatWing":
                    FirstQuestManager.Instance.batWing = true;
                    break;
                case "Feather":
                    FirstQuestManager.Instance.frogLeg = true;
                    break;
            }

            Destroy(gameObject);
        }
    }
}
