using System.Collections;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossDialog : MonoBehaviour
{
    public static BossDialog Instance;

    [SerializeField] private GameObject bossFightManager;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Camera camera;

    [Header("---- Dialogue Variables ----")] 
    [SerializeField] [TextArea(3,10)] private string text;
    [SerializeField] private float duration;
    private GameObject textBox;
    
    private void Awake()
    {
        Instance = this;
        SetReferences();
    }
    
    void SetReferences()
    {
        textBox = GameObject.FindGameObjectWithTag("backgroundDialogue");
        textBox.GetComponent<Image>().enabled = false;
    }

    public void StartBossDialog()
    {
        virtualCamera.Follow = null;
        virtualCamera.transform.position = new Vector3(-90, -90.3f, -10);
        StartCoroutine(CucaDialog());
    }

    IEnumerator CucaDialog()
    {
        yield return new WaitForSeconds(1f);
        SetDialogue(true);
        yield return new WaitForSeconds(duration);
        SetDialogue(false);
        
        UIManager.Instance.bossLife.SetActive(true);
        yield return new WaitForSeconds(5f);
        PlayerMovement.Instance.SetPlayerStatic(false);
        PlayerAttack.instance.SetCanAttack(true);
        PlayerHealth.Instance.currentHealth = PlayerHealth.Instance.maxHealth;
        UIManager.Instance.UpdateUI();
        yield return new WaitForSeconds(0.5f);
        bossFightManager.SetActive(true);
    }

    void SetDialogue(bool active)
    {
        if (active)
        {
            textBox.GetComponent<Image>().enabled = true;
            textBox.GetComponentInChildren<TextMeshProUGUI>().text = text;
        }
        else
        {
            textBox.GetComponentInChildren<TextMeshProUGUI>().text = "";
            textBox.GetComponent<Image>().enabled = false;
        }
    }
}
