using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;
    [SerializeField] private GameObject dragon;
    [SerializeField] private GameObject bat;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SpawnEnemies());
            
        }
    }

    IEnumerator SpawnEnemies()
    {
        Vector3 position = spawnPos.position;
        Quaternion rotation = spawnPos.rotation;
        
        Instantiate(bat, position, rotation);
        Instantiate(dragon, position, rotation);
        yield return new WaitForSeconds(2f);
        Instantiate(bat, position, rotation);
        Instantiate(dragon, position, rotation);
        yield return new WaitForSeconds(2f);
        Instantiate(bat, position, rotation);
        Instantiate(dragon, position, rotation);
        Destroy(gameObject);
    }
}
