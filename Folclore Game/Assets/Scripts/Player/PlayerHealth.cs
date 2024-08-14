using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;
    
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject gameOverScreen;
    public int maxHealth;
    public int currentHealth;
    
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    private float timeToFade = 2f;
    private CanvasGroup fade;

    void Awake()
    {
        Instance = this;
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        fade = GameObject.FindGameObjectWithTag("fade").GetComponent<CanvasGroup>();
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
        UIManager.Instance.UpdateUI();
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
        PlayerAttack.instance.SetCanAttack(false);
        PlayerMovement.Instance.SetPlayerStatic(true);
        animator.SetTrigger("isDead");
        yield return new WaitForSeconds(2f);
        fade.DOFade(1, timeToFade);
        yield return new WaitForSeconds(timeToFade + .5f);
        fade.DOFade(0, timeToFade);
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        PlayerMovement.Instance.SetPlayerStatic(false);
        PlayerAttack.instance.SetCanAttack(true);
    }
    
    IEnumerator WinScreen()
    {
        yield return new WaitForSeconds(6);
        winScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
