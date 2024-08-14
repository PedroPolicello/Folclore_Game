using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SecondQuestManager : MonoBehaviour
{
    public static SecondQuestManager instance;

    private int killCount;
    [HideInInspector] public bool killAllEnemies;
    [HideInInspector] public bool finishPuzzle2 = false;

    [SerializeField] private GameObject Player;

    #region GameObjects

    private GameObject enemySpawner;
    private GameObject card;

    #endregion
    #region Texts

    private GameObject textBox;

    [TextArea(3, 10)] [SerializeField] private string[] texts;

    #endregion

    private void Awake()
    {
        instance = this;
        SetReferences();
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.transform.position = new Vector3(-75, 0, 0);
    }

    void SetReferences()
    {
        textBox = GameObject.FindGameObjectWithTag("backgroundDialogue");
        textBox.GetComponent<Image>().enabled = false;
        enemySpawner = GameObject.FindGameObjectWithTag("enemySpawner");
        enemySpawner.SetActive(false);
        card = GameObject.FindGameObjectWithTag("card2");
        card.SetActive(false);
    }
    private void Update()
    {
        DeliverQuest();
        KillAllEnemies();
        SetupQuest();
        
    }

    void SetupQuest()
    {
        if (WarriorScript.instance.GetIsNearWarrior() && PlayerInputsControl.instance.GetIsPressed() &&
            !killAllEnemies && !finishPuzzle2)
        {
            StartCoroutine(Dialogue());
        }
    }

    void DeliverQuest()
    {
        if (killAllEnemies && WarriorScript.instance.GetIsNearWarrior() && PlayerInputsControl.instance.GetIsPressed() && !finishPuzzle2)
        {
            StartCoroutine(Dialogue2());
            WarriorScript.instance.ChangeSprite();
            card.SetActive(true);
            MainQuestManager.Instance.finishPuzzle2 = true;
            MainQuestManager.Instance.AddCardToCount();
        }
    }

    bool KillAllEnemies()
    {
        killAllEnemies = killCount >= 6;
        return killAllEnemies;
    }
    public void AddKillCount()
    {
        killCount++;
    }
    void ResetTextBox()
    {
        textBox.GetComponentInChildren<TextMeshProUGUI>().text = "";
        textBox.GetComponent<Image>().enabled = false;
    }

    IEnumerator Dialogue()
    {
        PlayerMovement.Instance.SetPlayerStatic(true);
        textBox.GetComponent<Image>().enabled = true;
        textBox.GetComponentInChildren<TextMeshProUGUI>().text = texts[0];
        yield return new WaitForSeconds(5f);
        PlayerMovement.Instance.SetPlayerStatic(false);
        ResetTextBox();
        enemySpawner.SetActive(true);
    }
    IEnumerator Dialogue2()
    {
        PlayerMovement.Instance.SetPlayerStatic(true);
        textBox.GetComponent<Image>().enabled = true;
        textBox.GetComponentInChildren<TextMeshProUGUI>().text = texts[1];
        yield return new WaitForSeconds(5f);
        PlayerMovement.Instance.SetPlayerStatic(false);
        ResetTextBox();
    }
}