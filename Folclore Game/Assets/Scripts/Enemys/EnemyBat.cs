using UnityEngine;
using UnityEngine.Serialization;

public class EnemyBat : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int maxLife;
    [SerializeField] private float fireRate;
    [FormerlySerializedAs("poopPrefab")] [SerializeField] private GameObject batAttackPrefab;
    [SerializeField] private float maxHeight;
    private float nextFire;

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
         if (transform.position.y <= maxHeight)
         {
             transform.position = new Vector2(transform.position.x, maxHeight);
         }

        // if (transform.position.x >= target.transform.position.x - 1 &&
        //     transform.position.x <= target.transform.position.x + 1)
        // {
        //     Attack();
        // }
    }

    void Attack()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            transform.position = new Vector2(transform.position.x, -0.5f);
            //Instantiate(batAttackPrefab, transform.position, transform.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spell"))
        {
            currentLife -= 1;
            CheckLife();
            print(currentLife);
        }
    }

    void CheckLife()
    {
        if (currentLife <= 0)
        {
            Destroy(gameObject);
        }
    }
}