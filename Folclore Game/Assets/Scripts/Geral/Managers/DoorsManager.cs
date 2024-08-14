using TMPro;
using UnityEngine;

public class DoorsManager : MonoBehaviour
{
    private bool inRange;
    private GameObject textFeedback;
    [SerializeField] private string text;
    
    [SerializeField] private float timeToFade = 2;
    private CanvasGroup fade;

    [SerializeField] private bool isEsgoto;

    private void Awake()
    {
        textFeedback = GameObject.FindGameObjectWithTag("textFeedback");
        textFeedback.GetComponent<TextMeshProUGUI>().text = "";
        fade = GameObject.FindGameObjectWithTag("fade").GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        SceneController();
    }

    void SceneController()
    {
        if(inRange && CompareTag("esgoto") && PlayerInputsControl.instance.GetIsPressed() && !MainQuestManager.Instance.finishPuzzle1)
        {
            textFeedback.GetComponent<TextMeshProUGUI>().text = "Bloqueado";
        }
        else if (inRange && CompareTag("esgoto") && PlayerInputsControl.instance.GetIsPressed() && MainQuestManager.Instance.finishPuzzle1)
        {
            global::SceneController.Instance.CucaEsgoto();
        }
        if (inRange && CompareTag("alquimista") && PlayerInputsControl.instance.GetIsPressed())
        {
            global::SceneController.Instance.CucaCasaAlquimista();
        }
        if (inRange && CompareTag("caminho") && PlayerInputsControl.instance.GetIsPressed())
        {
            if (isEsgoto) global::SceneController.Instance.CucaCaminho(true);
            else global::SceneController.Instance.CucaCaminho(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            textFeedback.GetComponent<TextMeshProUGUI>().text = text;
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            textFeedback.GetComponent<TextMeshProUGUI>().text = "";
            inRange = false;
        }
    }
}
