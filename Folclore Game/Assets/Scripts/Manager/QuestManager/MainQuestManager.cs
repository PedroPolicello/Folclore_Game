using UnityEngine;

public class MainQuestManager : MonoBehaviour
{
    public static MainQuestManager Instance;

    [SerializeField] private int cardsCollect;
    [SerializeField] private bool unlockBoss;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if(cardsCollect>=2) unlockBoss = true;
    }

    public void AddCardToCount()
    {
        cardsCollect++;
    }



}
