using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FirstQuestManager : MonoBehaviour
{
    public static FirstQuestManager Instance;

    private int ingredientCount;
    [HideInInspector] public bool hasAllIngredients;
    [HideInInspector] public bool finishPuzzle1 = false;
    [HideInInspector] public bool activeIngredient = false;

    #region GameObjects
    [SerializeField] private GameObject ingredient1;
    [SerializeField] private GameObject ingredient2;
    [SerializeField] private GameObject ingredient3;
    public bool frogLeg;
    public bool feather;
    public bool batWing;

    [SerializeField] private GameObject potion;
    private GameObject card;
    #endregion

    #region Texts
    private GameObject textBox;
    
    [TextArea(3,10)] [SerializeField] private string[] texts;
    #endregion

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
    }

    private void Update()
    {
        DeliverQuest();
        SetupQuest();
    }
    void SetupQuest()
    {
        if (WizardScript.instance.GetIsNearWizard() && PlayerInputsControl.instance.GetIsPressed() && !hasAllIngredients && !finishPuzzle1)
        {
            StartCoroutine(Dialogue());
            activeIngredient = true;
        }
    }
    void DeliverQuest()
    {
        if (hasAllIngredients && WizardScript.instance.GetIsNearWizard() && PlayerInputsControl.instance.GetIsPressed() && !finishPuzzle1)
        {
            StartCoroutine(Dialogue2());
            WizardScript.instance.ChangeSprite();
            card.SetActive(true);
            potion.SetActive(true);
            finishPuzzle1 = true;
        }
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
