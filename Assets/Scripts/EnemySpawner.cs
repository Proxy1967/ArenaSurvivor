using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyChase enemyPrefab;
    [SerializeField] Rigidbody2D player;
    [SerializeField] float spawnInterval = 2f;
    [SerializeField] float wavePause = 3f;
    [SerializeField] float winPause = 1f;
    // half the screen's diagonal is about 10.2, so 11 is always off-screen
    [SerializeField] float spawnRadius = 11f;
    [SerializeField] int waves = 5;
    [SerializeField] int enemiesPerWave = 3;

    private int aliveEnemies;
    private Health enemyHealth;
    public event Action<int, int> OnWaveStarted;
    public event Action OnAllWavesCleared;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for(int i = 0; i < waves; i++)
        {
            OnWaveStarted?.Invoke(i + 1, waves);

            // spawn this wave's enemies, one every spawnInterval
            for(int j = 0; j < enemiesPerWave * (i+1); j++)
            {
                EnemyChase newEnemy = Instantiate(enemyPrefab, UnityEngine.Random.insideUnitCircle.normalized * spawnRadius, Quaternion.identity);
                newEnemy.SetTarget(player);
                aliveEnemies++;
                enemyHealth = newEnemy.GetComponent<Health>();
                enemyHealth.OnDied += HandleEnemyDied;
                yield return new WaitForSeconds(spawnInterval);
            }

            // wait until every enemy is dead
            yield return new WaitUntil(() => aliveEnemies == 0);
            
            // log "Wave X cleared", pause a few seconds if there are more waves
            Debug.Log("Wave " + (i+1) + " cleared");
            if (i+1 < waves)
            {
                yield return new WaitForSeconds(wavePause);            
            }
        }

        // win condition met - all waves cleared
        yield return new WaitForSeconds(winPause);  
        OnAllWavesCleared?.Invoke();
        Debug.Log("YOU WIN !");
    }

    void HandleEnemyDied()
    {
        aliveEnemies--;
    }
}
