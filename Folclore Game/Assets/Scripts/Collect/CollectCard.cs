using UnityEngine;

public class CollectCard : CollectableBase
{
    protected override void Collect()
    {
        if (inRange && PlayerInputsControl.Instance.GetIsPressed())
        {
            MainQuestManager.Instance.AddCardToCount();
            SoundManager.Instance.PlaySFX(SoundManager.Instance.collect);
            Destroy(gameObject);
        }
    }
}
