using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    float lastSpawnElapsed;
    public float timeBetweenSpawns = 2F;
    public int maxEnemies = 10;
    public static int enemyAmount;

    private void Start()
    {
        enemyAmount = maxEnemies;
    }
    // Update is called once per frame
    void Update()
    {

        if (Time.time - lastSpawnElapsed > timeBetweenSpawns && enemyAmount > 0 )
        {
            InstantiateEnemy();
            enemyAmount--;
        }
    }
    void InstantiateEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        lastSpawnElapsed = Time.time;
    }
}
