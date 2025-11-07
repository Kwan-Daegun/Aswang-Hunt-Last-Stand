using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public Transform leftSpawn;         
    public Transform rightSpawn;        
    public GameObject[] wave1Enemies;
    public GameObject[] wave2Enemies;
    public GameObject[] wave3Enemies;

    [Header("Wave Settings")]
    public float timeBetweenWaves = 5f;
    public int enemiesPerWave = 5;
    public float enemySpawnInterval = 3f;

    private int currentWave = 0;
    private bool spawning = false;

    public GameManager gameManager;
    void Start()
    {
        StartNextWave();
    }

    void Update()
    {

        if (!spawning && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            if (currentWave >= 2 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                gameManager.nextWave();
                //nextWaveGO.SetActive(true);
            }
            else if(currentWave == 1 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                //Display the continue wave or buy items canvas
                //
                Invoke(nameof(StartNextWave), timeBetweenWaves);
                spawning = true;
            }
        }
        /*if (!spawning && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            //Display the continue wave or buy items canvas
            //
            Invoke(nameof(StartNextWave), timeBetweenWaves);
            spawning = true;

        }*/
    }

    public void OnClickNextWave()
    {
        Invoke(nameof(StartNextWave), timeBetweenWaves);
        spawning = true;
    }

    public void StartNextWave()
    {
        currentWave++;
        spawning = false;

        Debug.Log("Wave " + currentWave);

        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {

        if (currentWave >= 2)
        {
            enemiesPerWave += 2;
        }
        GameObject[] enemyPool = GetEnemyPool(currentWave);
        int bossCount = 0;
        for (int i = 0; i < enemiesPerWave + (currentWave - 1) * 2; i++)
        {
            yield return new WaitForSeconds(enemySpawnInterval);
            GameObject enemyPrefab = enemyPool[0];
            Transform spawnPoint = Random.value < 0.5f ? leftSpawn : rightSpawn;
            enemyPrefab = enemyPool[Random.Range(0, enemyPool.Length)];
            if (enemyPrefab.name == "Tikbalang" && bossCount < (currentWave - 1))
            {
                bossCount++;
            }
            else
            {
                enemyPrefab = enemyPool[0];
            }
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
                enemyAI.SetTarget(Vector2.zero);

            enemyPrefab = enemyPool[0];
        }
    }

    GameObject[] GetEnemyPool(int wave)
    {
        if (wave == 1) return wave1Enemies;
        if (wave == 2) return wave2Enemies;
        if (wave == 3) return wave3Enemies;
        return wave3Enemies;
    }
}
