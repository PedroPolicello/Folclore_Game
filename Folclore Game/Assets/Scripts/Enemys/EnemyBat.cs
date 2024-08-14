using System.Collections;
using UnityEngine;
using DentedPixel;

public class EnemyBat : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int maxLife;
    [SerializeField] private float fireRate;
    [SerializeField] private float maxHeight;
    private float nextFire;
    private bool inPos = false;

    [SerializeField] private int currentLife;
    private Transform transform;
    private GameObject target;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        transform = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player");
        currentLife = maxLife;
    }

    void Update()
    {
        FollowPlayer();
        FlipX();
        InPosition();
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
    }

    void InPosition()
    {
        if (transform.position.x > target.transform.position.x - 1 && transform.position.x < target.transform.position.x + 1 && Time.time > nextFire)
        {
            StartCoroutine(Attack());
            nextFire = Time.time + fireRate;
        }
        else
        {
            transform.position = new Vector2(transform.position.x, maxHeight);
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.1f);
        LeanTween.moveLocalY(gameObject, transform.position.y - 2, 1.5f).setEasePunch();
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
            PlayerHealth.Instance.TakeDamage(1);
        }
    }

    void CheckLife()
    {
        if (currentLife <= 0)
        {
            SecondQuestManager.instance.AddKillCount();
            Destroy(gameObject);
        }
    }
}