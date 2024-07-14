using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private int maxHealth;
    public int currentHealth;
    
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        instance = this;
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // public void CallWinScreen()
    // {
    //     if (SecondQuestManager.instance.finishPuzzle2)
    //     {
    //         StartCoroutine(WinScreen());
    //     }
    // }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(Damage());
        CheckHealth();
    }

    void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Damage()
    {
        spriteRenderer.color = Color.clear;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.clear;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.clear;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
    
    IEnumerator Death()
    {
        PlayerMovement.Instance.SetPlayerStatic(true);
        animator.SetTrigger("isDead");
        yield return new WaitForSeconds(2.2f);
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
        PlayerMovement.Instance.SetPlayerStatic(false);
    }
    
    IEnumerator WinScreen()
    {
        yield return new WaitForSeconds(6);
        winScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
