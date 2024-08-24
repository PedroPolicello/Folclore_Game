using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private Slider healthBar;
    [SerializeField] private Image fillColor;
    public GameObject pauseMenu;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        healthBar.maxValue = PlayerHealth.Instance.maxHealth;
        healthBar.value = healthBar.maxValue;
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
}