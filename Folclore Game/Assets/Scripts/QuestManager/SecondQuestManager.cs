using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SecondQuestManager : MonoBehaviour
{
    public static SecondQuestManager instance;

    private GameObject Player;
    private int killCount;
    [HideInInspector] public bool killAllEnemies;
    [HideInInspector] public bool finishPuzzle2 = false;
    private bool hasTalked;
    private bool hasAppeard = true;
    
    private GameObject enemySpawner;
    private GameObject card;

    [Header("---- Dialogue Variables ----")]
    [SerializeField] private float duration;
    [TextArea(3, 10)][SerializeField] private string[] texts;
    private GameObject textBox;
    private AudioSource audioSource;
    private bool hasInteracted;

    private void Awake()
    {
        instance = this;
        SetReferences();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void SetReferences()
    {
        textBox = GameObject.FindGameObjectWithTag("backgroundDialogue");
        textBox.GetComponent<Image>().enabled = false;
        enemySpawner = GameObject.FindGameObjectWithTag("enemySpawner");
        enemySpawner.SetActive(false);
        card = GameObject.FindGameObjectWithTag("card2");
        card.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        DeliverQuest();
        KillAllEnemies();
        SetupQuest();

        if (killCount >= 6 && hasAppeard) StartCoroutine(PuzzleFeedback());
    }

    void SetupQuest()
    {
        if (WarriorScript.instance.GetIsNearWarrior() && PlayerInputsControl.Instance.GetIsPressed() && !killAllEnemies && !finishPuzzle2 && !hasInteracted)
        {
            StartCoroutine(Dialogue());
            hasInteracted = true;
        }
    }

    void DeliverQuest()
    {
        if (killAllEnemies && WarriorScript.instance.GetIsNearWarrior() && PlayerInputsControl.Instance.GetIsPressed() && !finishPuzzle2 && !hasTalked && !hasInteracted)
        {
            StartCoroutine(Dialogue2());
            WarriorScript.instance.ChangeSprite();
            card.SetActive(true);
            MainQuestManager.Instance.finishPuzzle2 = true;
            hasTalked = true;
            hasInteracted = true;
        }
    }
    bool KillAllEnemies()
    {
        killAllEnemies = killCount >= 6;
        hasInteracted = false;
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
        audioSource.volume = SoundManager.Instance.sFXVolume.value/50;
        audioSource.PlayOneShot(SoundManager.Instance.nPCInteract);
        
        PlayerAttack.instance.SetCanAttack(false);
        PlayerMovement.Instance.SetPlayerStatic(true);
        textBox.GetComponent<Image>().enabled = true;
        textBox.GetComponentInChildren<TextMeshProUGUI>().text = texts[0];
        yield return new WaitForSeconds(duration);
        PlayerMovement.Instance.SetPlayerStatic(false);
        PlayerAttack.instance.SetCanAttack(true);
        ResetTextBox();
        enemySpawner.SetActive(true);
    }
    IEnumerator Dialogue2()
    {
        audioSource.volume = SoundManager.Instance.sFXVolume.value/50;
        audioSource.PlayOneShot(SoundManager.Instance.nPCInteract);
        
        PlayerMovement.Instance.SetPlayerStatic(true);
        textBox.GetComponent<Image>().enabled = true;
        textBox.GetComponentInChildren<TextMeshProUGUI>().text = texts[1];
        yield return new WaitForSeconds(duration);
        PlayerMovement.Instance.SetPlayerStatic(false);
        ResetTextBox();
    }

    IEnumerator PuzzleFeedback()
    {
        hasAppeard = false;
        MainQuestManager.Instance.ActiveText(true, "Puzzle 02 Finalizado!");
        yield return new WaitForSeconds(2);
        MainQuestManager.Instance.ActiveText(false, "");
    }
}