using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyDragon : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int maxLife;

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