public class CollectIngredient : CollectableBase
{
    protected override void Collect()
    {
        if (inRange && PlayerInputsControl.instance.GetIsPressed())
        {
            FirstQuestManager.Instance.AddIngredientCount();
            Destroy(gameObject);
        }
    }
}
