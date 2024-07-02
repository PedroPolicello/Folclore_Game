using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorsManager : MonoBehaviour
{
    private GameObject textFeedback;
    
    //[SerializeField] private GameObject Player;
    [SerializeField] private string text;

    private bool inRange = false;

    private void Awake()
    {
        textFeedback = GameObject.FindGameObjectWithTag("textFeedback");
        textFeedback.GetComponent<TextMeshProUGUI>().text = "";
    }

    private void Update()
    {
        SceneController();
    }

    void SceneController()
    {
        if (inRange && CompareTag("esgoto") && PlayerInputsControl.instance.GetIsPressed() && FirstQuestManager.Instance.finishPuzzle1)
        {
            SceneManager.LoadScene("CucaEsgoto");
        }
        if (inRange && CompareTag("alquimista") && PlayerInputsControl.instance.GetIsPressed())
        {
            SceneManager.LoadScene("CucaCasaAlquimista");
        }
        if (inRange && CompareTag("caminho") && PlayerInputsControl.instance.GetIsPressed())
        {
            SceneManager.LoadScene("CucaLevel");
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
