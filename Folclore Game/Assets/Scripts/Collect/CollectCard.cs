public class CollectCard : CollectableBase
{
    protected override void Collect()
    {
        if (inRange && PlayerInputsControl.Instance.GetIsPressed())
        {
            MainQuestManager.Instance.AddCardToCount();
            Destroy(gameObject);
        }
    }
}
