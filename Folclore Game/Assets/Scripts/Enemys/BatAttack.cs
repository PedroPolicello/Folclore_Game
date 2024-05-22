
using UnityEngine;

public class BatAttack : MonoBehaviour
{
    [SerializeField] private float speed;

    private GameObject target;
    private Transform transform;
    private Rigidbody2D rb;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");

    }

     private void Update()
     {
         transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
     }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth.instance.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}