using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private int maxHealth;
    private int currentHealth;

    void Awake()
    {
        instance = this;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        CheckHealth();
    }

    void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
