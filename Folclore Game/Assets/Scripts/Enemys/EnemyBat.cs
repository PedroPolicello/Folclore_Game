using UnityEngine;
using UnityEngine.Serialization;

public class EnemyBat : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int maxLife;
    [SerializeField] private float fireRate;
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
        AttackPlayer();
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

    void AttackPlayer()
    {
        if (transform.position.x > target.transform.position.x - .5 && transform.position.x < target.transform.position.x + .5 && Time.time > nextFire)
        {
            //transform.position = Vector2.MoveTowards(transform.position, target.transform.position - new Vector3(0, -1, 0), speed * Time.deltaTime); AJUTES!!!!!!
            nextFire = Time.time + fireRate;
        }
        else
        {
            transform.position = new Vector2(transform.position.x, maxHeight);
        }
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
            transform.position = new Vector2(transform.position.x, maxHeight);
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