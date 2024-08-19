using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    
    [SerializeField] private Transform spawnPos;
    [SerializeField] private GameObject dragon;
    [SerializeField] private GameObject bat;
    [SerializeField] private GameObject[] dragons;
    [SerializeField] private GameObject[] bats;

    private Collider2D collider;

    private void Awake()
    {
        Instance = this;
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        bats = GameObject.FindGameObjectsWithTag("bat");
        dragons = GameObject.FindGameObjectsWithTag("dragon");
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

    public void KillAllEnemies()
    {
        foreach (var enemy in bats)
        {
            Destroy(enemy);
        }
        foreach (var enemy in dragons)
        {
            Destroy(enemy);
        }
    }
}
