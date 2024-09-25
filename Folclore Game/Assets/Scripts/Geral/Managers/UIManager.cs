using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject pauseMenu;
    public GameObject audioSettings;
    
    [Header("---- Player Life Variables ----")] 
    [SerializeField] private Slider healthBar;
    [SerializeField] private Image fillColor;

    [Header("---- Boss Life Variables ----")] [SerializeField]
    public GameObject bossLife;
    [SerializeField] private Slider bossHealthBar;
    [SerializeField] private Image bossFillColor;

    private void Awake()
    {
        Instance = this;
        bossLife.SetActive(false);
    }

    private void Start()
    {
        healthBar.maxValue = PlayerHealth.Instance.maxHealth;
        healthBar.value = healthBar.maxValue;
    }

    public void ExitPause()
    {
        audioSettings.SetActive(false);
        pauseMenu.SetActive(false);
        PlayerInputsControl.Instance.inPause = false;
        Time.timeScale = 1;
    }

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        healthBar.value = PlayerHealth.Instance.currentHealth;

        if (healthBar.value <= healthBar.maxValue / 2)
        {
            fillColor.color = Color.red;
        }
        else if (healthBar.value >= healthBar.maxValue / 2)
        {
            fillColor.color = Color.green;

        }
    }

    public void SetBossHealthBar()
    {
        bossHealthBar.maxValue = BossFightScript.Instance.maxLife;
        bossHealthBar.value = bossHealthBar.maxValue;
    }

    public void UpdateBossUI()
    {
        bossHealthBar.value = BossFightScript.Instance.currentLife;

        if (bossHealthBar.value <= bossHealthBar.maxValue / 2)
        {
            bossFillColor.color = Color.red;
        }
        else if (bossHealthBar.value >= bossHealthBar.maxValue / 2)
        {
            bossFillColor.color = Color.green;

        }
    }
}