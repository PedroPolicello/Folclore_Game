using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class FirstQuestManager : MonoBehaviour
{
    public static FirstQuestManager Instance;

    private GameObject player;
    private int ingredientCount;
    [HideInInspector] public bool hasAllIngredients;
    [HideInInspector] public bool finishPuzzle1 = false;
    [HideInInspector] public bool activeIngredient = false;

    [Header("---- Scene Variables ----")]
    [SerializeField] private GameObject spawnPointSeita;
    [SerializeField] private GameObject spawnPointAlquimista;
    [SerializeField] private float timeToFade;
    [SerializeField] private CanvasGroup fade;
    public bool goToWizard;
    private bool hasTalked;
    
    [Header("---- GameObjects Variables ----")]
    [SerializeField] private GameObject ingredient1;
    [SerializeField] private GameObject ingredient2;
    [SerializeField] private GameObject ingredient3;
    [HideInInspector] public bool frogLeg;
    [HideInInspector] public bool feather;
    [HideInInspector] public bool batWing;
    [SerializeField] private GameObject potion;
    private GameObject card;
    
    [Header("---- Dialogue Variables ----")]
    [SerializeField] private float duration;
    [TextArea(3, 10)][SerializeField] private string[] texts;
    private GameObject textBox;
    
    private void Awake()
    {
        Instance = this;
        SetReferences();
    }
    void SetReferences()
    {
        card = GameObject.FindGameObjectWithTag("card1");
        card.SetActive(false);
        textBox = GameObject.FindGameObjectWithTag("backgroundDialogue");
        textBox.GetComponent<Image>().enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
        fade = GameObject.FindGameObjectWithTag("fade").GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        DeliverQuest();
        SetupQuest();
    }
    void SetupQuest()
    {
        if (WizardScript.instance.GetIsNearWizard() && PlayerInputsControl.Instance.GetIsPressed() && !hasAllIngredients && !finishPuzzle1)
        {
            StartCoroutine(Dialogue());
            ingredient1.SetActive(true);
            ingredient2.SetActive(true);
            ingredient3.SetActive(true);
        }
    }
    void DeliverQuest()
    {
        if (HasAllIngredients() && WizardScript.instance.GetIsNearWizard() && PlayerInputsControl.Instance.GetIsPressed() && !finishPuzzle1 && !hasTalked)
        {
            StartCoroutine(Dialogue2());
            WizardScript.instance.ChangeSprite();
            card.SetActive(true);
            potion.SetActive(true);
            MainQuestManager.Instance.finishPuzzle1 = true;
            hasTalked = true;
        }
    }

    bool HasAllIngredients()
    {
        hasAllIngredients = ingredientCount >= 3;
        if(hasAllIngredients) goToWizard = true;
        return hasAllIngredients;
    }

    public void AddIngredientCount()
    {
        ingredientCount++;
        if (HasAllIngredients() && goToWizard) StartCoroutine(SendBackToWizard());
    }

    void ResetTextBox()
    {
        textBox.GetComponentInChildren<TextMeshProUGUI>().text = "";
        textBox.GetComponent<Image>().enabled = false;
    }

    IEnumerator Dialogue()
    {
        PlayerAttack.instance.SetCanAttack(false);
        PlayerMovement.Instance.SetPlayerStatic(true);
        textBox.GetComponent<Image>().enabled = true;
        textBox.GetComponentInChildren<TextMeshProUGUI>().text = texts[0];
        yield return new WaitForSeconds(duration);
        ResetTextBox();
        StartCoroutine(SendToSeita());
    }
    IEnumerator Dialogue2()
    {
        PlayerAttack.instance.SetCanAttack(false);
        PlayerMovement.Instance.SetPlayerStatic(true);
        textBox.GetComponent<Image>().enabled = true;
        textBox.GetComponentInChildren<TextMeshProUGUI>().text = texts[1];
        yield return new WaitForSeconds(duration);
        PlayerMovement.Instance.SetPlayerStatic(false);
        PlayerAttack.instance.SetCanAttack(true);
        ResetTextBox();
    }
    IEnumerator SendToSeita()
    {
        fade.DOFade(1, timeToFade);
        yield return new WaitForSeconds(timeToFade + .5f);
        player.transform.position = spawnPointSeita.transform.position;
        yield return new WaitForSeconds(timeToFade + .5f);
        fade.DOFade(0, timeToFade);
        PlayerMovement.Instance.SetPlayerStatic(false);
        yield return new WaitForSeconds(5f);
        PlayerAttack.instance.SetCanAttack(true);
    }
    IEnumerator SendBackToWizard()
    {
        MainQuestManager.Instance.ActiveText(true, "Puzzle 01 Finalizado!");
        goToWizard = false;
        yield return new WaitForSeconds(1f);
        PlayerMovement.Instance.SetPlayerStatic(true);
        fade.DOFade(1, timeToFade);
        yield return new WaitForSeconds(timeToFade + .5f);
        MainQuestManager.Instance.ActiveText(false, "");
        player.transform.position = spawnPointAlquimista.transform.position;
        yield return new WaitForSeconds(timeToFade + .5f);
        fade.DOFade(0, timeToFade);
        PlayerMovement.Instance.SetPlayerStatic(false);
    }
}
