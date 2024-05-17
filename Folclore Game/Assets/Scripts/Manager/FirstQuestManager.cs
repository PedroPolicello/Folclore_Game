using System.Collections;
using TMPro;
using UnityEngine;

public class FirstQuestManager : MonoBehaviour
{
    public static FirstQuestManager Instance;

    private int ingredientCount;
    private bool hasAllIngredients;

    #region GameObjects
    [SerializeField] private GameObject ingredient1;
    [SerializeField] private GameObject ingredient2;
    [SerializeField] private GameObject ingredient3;

    [HideInInspector] public bool frogLeg;
    [HideInInspector] public bool feather;
    [HideInInspector] public bool batWing;

    [SerializeField] private GameObject potion;
    [SerializeField] private GameObject card;
    #endregion

    #region Texts
    [SerializeField] private GameObject textBox;
    
    [TextArea(3,10)] [SerializeField] private string[] texts;
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        DeliverQuest();
        HasAllIngredients();
        SetupQuest();
    }
    void SetupQuest()
    {
        if (WizardScript.instance.GetIsNearWizard() && PlayerInputsControl.instance.GetIsPressed() && !HasAllIngredients())
        {
            StartCoroutine(Dialogue());
            ingredient1.SetActive(true);
            ingredient2.SetActive(true);
            ingredient3.SetActive(true);
        }
    }
    void DeliverQuest()
    {
        if (HasAllIngredients() && WizardScript.instance.GetIsNearWizard() && PlayerInputsControl.instance.GetIsPressed())
        {
            WizardScript.instance.ChangeSprite();
            card.SetActive(true);
            potion.SetActive(true);
            print("Procure a segunda carta nos esgostos!");
        }
    }
    bool HasAllIngredients()
    {
        hasAllIngredients = ingredientCount >= 3;
        return hasAllIngredients;
    }
    public void AddIngredientCount()
    {
        ingredientCount++;
    }
    IEnumerator Dialogue()
    {
        textBox.gameObject.SetActive(true);
        textBox.GetComponentInChildren<TextMeshProUGUI>().text = texts[0];
        yield return new WaitForSeconds(5f);
        textBox.gameObject.SetActive(false);
    }
}