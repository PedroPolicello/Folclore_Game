using TMPro;
using UnityEngine;

public class MainQuestManager : MonoBehaviour
{
    public static MainQuestManager Instance;

    [SerializeField] private GameObject morvain;
    [SerializeField] private int cardsCollect;
    [SerializeField] private TextMeshProUGUI missionCompleteText;
    public bool unlockBoss;
    public bool finishPuzzle1;
    public bool finishPuzzle2;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (cardsCollect >= 2) unlockBoss = true;
        if (finishPuzzle2) morvain.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void AddCardToCount()
    {
        cardsCollect++;
    }

    public void ActiveText(bool active, string text)
    {
        if (active)
        {
            print(text);
            missionCompleteText.gameObject.SetActive(true);
            missionCompleteText.text = text;
        }
        else
        {
            missionCompleteText.text = text;
            missionCompleteText.gameObject.SetActive(false);
        }
    }
}
