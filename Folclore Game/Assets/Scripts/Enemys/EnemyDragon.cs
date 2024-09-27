using System;
using UnityEngine;

public class EnemyDragon : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int maxLife;
    [SerializeField] private bool isBoss;
    
    private int currentLife;
    private Transform transform;
    private GameObject target;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private AudioSource audioSource;

    void Awake()
    {
        transform = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Target");
        currentLife = maxLife;
    }

    void Update()
    {
        FollowPlayer();
        FlipX();
    }

    void FlipX()
    {
        if (target.transform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        rb.AddForce(Vector2.down*2);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spell"))
        {
            currentLife -= 1;
            CheckLife();
        }

        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.volume = SoundManager.Instance.sFXVolume.value/10;
            audioSource.PlayOneShot(SoundManager.Instance.dragonAttack);
            
            PlayerHealth.Instance.TakeDamage(1);
        }
    }

    void CheckLife()
    {
        if (currentLife <= 0 && isBoss)
        {
            audioSource.volume = SoundManager.Instance.sFXVolume.value/10;
            audioSource.PlayOneShot(SoundManager.Instance.bossHit);
            
            BossFightScript.Instance.TakeDamage(1);
            Destroy(gameObject);
        }
        else if (currentLife <= 0)
        {
            SecondQuestManager.instance.AddKillCount();
            Destroy(gameObject);
        }
    }
}