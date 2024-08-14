public class CollectCard : CollectableBase
{
    protected override void Collect()
    {
        if (inRange && PlayerInputsControl.instance.GetIsPressed())
        {
            MainQuestManager.Instance.AddCardToCount();
            Destroy(gameObject);
        }
    }
}
