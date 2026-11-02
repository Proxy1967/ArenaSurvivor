using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyChase enemyPrefab;
    [SerializeField] Rigidbody2D player;
    [SerializeField] float spawnInterval = 2f;
    // half the sceen's diagonal is about 10.2, so 11 is always off-screen
    [SerializeField] int spawnRadius = 11;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while(true)
        {
            EnemyChase newEnemy = Instantiate(enemyPrefab, Random.insideUnitCircle.normalized * spawnRadius, Quaternion.identity);
            newEnemy.SetTarget(player);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
