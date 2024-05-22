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
        if (inRange && PlayerInputsControl.instance.GetIsPressed())
        {
            SceneManager.LoadScene("CucaEsgoto");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            textFeedback.GetComponent<TextMeshProUGUI>().text = text;
            textFeedback.SetActive(true);
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            textFeedback.SetActive(false);
            inRange = false;
        }
    }
}
