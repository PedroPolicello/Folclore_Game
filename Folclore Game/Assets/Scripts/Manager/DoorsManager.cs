using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorsManager : MonoBehaviour
{
    [SerializeField] private GameObject textFeedback;
    [SerializeField] private string text;

    private bool inRange = false;

    private void Update()
    {
        if (inRange && PlayerInputsControl.instance.GetIsPressed() && FirstQuestManager.Instance.finishPuzzle1)
        {
            SceneManager.LoadScene("CucaEsgoto");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && FirstQuestManager.Instance.finishPuzzle1)
        {
            textFeedback.GetComponent<TextMeshProUGUI>().text = text;
            textFeedback.SetActive(true);
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && FirstQuestManager.Instance.finishPuzzle1)
        {
            textFeedback.SetActive(false);
            inRange = false;
        }
    }
}
