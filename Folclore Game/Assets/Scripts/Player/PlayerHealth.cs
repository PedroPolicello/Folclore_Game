using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;
    
    [SerializeField] private GameObject gameOverScreen;
    public int maxHealth;
    public int currentHealth;
    
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    private float timeToFade = 2f;
    private CanvasGroup fade;
    [SerializeField] private AudioSource audioSource;


    void Awake()
    {
        Instance = this;
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        fade = GameObject.FindGameObjectWithTag("fade").GetComponent<CanvasGroup>();
    }

    public void TakeDamage(int damage)
    {
        audioSource.volume = SoundManager.Instance.sFXVolume.value/10;
        audioSource.PlayOneShot(SoundManager.Instance.damage);

        currentHealth -= damage;
        StartCoroutine(Damage());
        CheckHealth();
        UIManager.Instance.UpdateUI();
    }

    void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
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
        audioSource.volume = SoundManager.Instance.sFXVolume.value/10;
        audioSource.PlayOneShot(SoundManager.Instance.death);

        SetColliderRB2D(false, 0);
        BossFightScript.Instance.StopAllCoroutines();
        PlayerAttack.instance.SetCanAttack(false);
        PlayerMovement.Instance.SetPlayerStatic(true);
        BossFightScript.Instance.KillAllDragons();
        animator.SetTrigger("isDead");
        yield return new WaitForSeconds(2f);
        fade.DOFade(1, timeToFade);
        yield return new WaitForSeconds(timeToFade + .5f);
        gameOverScreen.SetActive(true);
        fade.DOFade(0, timeToFade);
        yield return new WaitForSeconds(timeToFade + .5f);
        Time.timeScale = 0;
    }

    public void SetColliderRB2D(bool collider, int gravityScale)
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = collider;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
    }
}
