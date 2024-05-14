using UnityEngine;

public class EnemyBat : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int maxLife;
    [SerializeField] private float fireRate;
    [SerializeField] private GameObject poopPrefab;
    private float nextFire = 0.0F;

    private int currentLife;
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
        if (transform.position.y <= 1)
        {
            transform.position = new Vector2(transform.position.x, 1);
        }

        if (transform.position.x >= target.transform.position.x - 1 &&
            transform.position.x <= target.transform.position.x + 1)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(poopPrefab, transform.position, transform.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spell"))
        {
            TakeDamame(1);
        }
    }

    void TakeDamame(int dmg)
    {
        currentLife -= dmg;
        CheckLife();
        print(currentLife);
    }

    void CheckLife()
    {
        if (currentLife <= 0)
        {
            Destroy(gameObject);
        }
    }
}