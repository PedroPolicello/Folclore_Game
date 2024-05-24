using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;
    [SerializeField] private GameObject dragon;
    [SerializeField] private GameObject bat;

    private Collider2D collider;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SpawnEnemies());
            Destroy(this.collider);
        }
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 position = spawnPos.position;
            Quaternion rotation = spawnPos.rotation;

            Instantiate(bat, position, rotation);
            Instantiate(dragon, position, rotation);
            yield return new WaitForSeconds(2f);
        }
    }
}
