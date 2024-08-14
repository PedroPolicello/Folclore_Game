using UnityEngine;

public class MainQuestManager : MonoBehaviour
{
    public static MainQuestManager Instance;

    [SerializeField] private int cardsCollect;
    [SerializeField] private bool unlockBoss;
    public bool finishPuzzle1;
    public bool finishPuzzle2;

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
