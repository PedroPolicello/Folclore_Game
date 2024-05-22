using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] private float lifeSpawn;
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    void Awake()
    {
        Destroy(gameObject, lifeSpawn);
        rb = GetComponent<Rigidbody2D>();
        AddForce();
    }

    void AddForce()
    {
        if (PlayerMovement.Instance.spriteRenderer.flipX == false)
        {
            rb.AddForce(Vector2.right * speed * 100);
            transform.eulerAngles = Vector3.forward * 90;
        }
        else
        {
            rb.AddForce(Vector2.left * speed * 100);
            transform.eulerAngles = Vector3.forward * -90;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}