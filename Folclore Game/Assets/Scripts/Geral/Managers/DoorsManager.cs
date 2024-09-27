using TMPro;
using UnityEngine;

public class DoorsManager : MonoBehaviour
{
    private bool inRange;
    private GameObject textFeedback;
    [SerializeField] private string text;
    [SerializeField] private GameObject pressE;
    
    [SerializeField] private float timeToFade = 2;
    //private CanvasGroup fade;

    [SerializeField] private bool isEsgoto;

    private void Awake()
    {
        textFeedback = GameObject.FindGameObjectWithTag("textFeedback");
        textFeedback.GetComponent<TextMeshProUGUI>().text = "";
        //fade = GameObject.FindGameObjectWithTag("fade").GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        SceneController();
    }

    void SceneController()
    {
        //Vai para o esgoto
        if(inRange && CompareTag("esgoto") && PlayerInputsControl.Instance.GetIsPressed() && !MainQuestManager.Instance.finishPuzzle1)
        {
            textFeedback.GetComponent<TextMeshProUGUI>().text = "Bloqueado";
        }
        else if (inRange && CompareTag("esgoto") && PlayerInputsControl.Instance.GetIsPressed() && MainQuestManager.Instance.finishPuzzle1)
        {
            global::SceneController.Instance.CucaEsgoto();
        }
        //Vai para o Alquimista
        if (inRange && CompareTag("alquimista") && PlayerInputsControl.Instance.GetIsPressed())
        {
            global::SceneController.Instance.CucaCasaAlquimista();
        }
        //Volta para o caminho
        if (inRange && CompareTag("caminho") && PlayerInputsControl.Instance.GetIsPressed())
        {
            if (isEsgoto) global::SceneController.Instance.CucaCaminho(true);
            else global::SceneController.Instance.CucaCaminho(false);
        }
        //Vai para o Boss
        if(inRange && CompareTag("boss") && PlayerInputsControl.Instance.GetIsPressed() && !MainQuestManager.Instance.finishPuzzle1)
        {
            textFeedback.GetComponent<TextMeshProUGUI>().text = "Bloqueado";
        }
        else if (inRange && CompareTag("boss") && PlayerInputsControl.Instance.GetIsPressed() && MainQuestManager.Instance.unlockBoss)
        {
            global::SceneController.Instance.CucaBoss();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            textFeedback.GetComponent<TextMeshProUGUI>().text = text;
            pressE.SetActive(true);
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Player")) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            textFeedback.GetComponent<TextMeshProUGUI>().text = "";
            pressE.SetActive(false);
            inRange = false;
        }
    }
}
