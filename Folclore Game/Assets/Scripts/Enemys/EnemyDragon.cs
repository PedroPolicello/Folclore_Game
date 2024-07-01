using UnityEngine;

public class EnemyDragon : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int maxLife;

    private int currentLife;
    private Transform transform;
    private GameObject target;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    void Awake()
    {
        transform = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
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
        transform.position = new Vector2(transform.position.x, -1);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spell"))
        {
            currentLife -= 1;
            CheckLife();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth.instance.TakeDamage(1);
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
