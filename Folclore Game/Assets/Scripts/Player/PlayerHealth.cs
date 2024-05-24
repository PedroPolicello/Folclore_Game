using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private int maxHealth;
    private int currentHealth;

    void Awake()
    {
        instance = this;
        currentHealth = maxHealth;
    }

    public void CallWinScreen()
    {
        if (SecondQuestManager.instance.finishPuzzle2)
        {
            StartCoroutine(WinScreen());
        }
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
    
    IEnumerator WinScreen()
    {
        yield return new WaitForSeconds(6);
        winScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
