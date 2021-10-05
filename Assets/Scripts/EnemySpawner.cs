using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner spawner;
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    float lastSpawnElapsed;
    public float timeBetweenSpawns = 2F;
    public int maxEnemies = 10;
    private int enemyAmount;

    private void Start()
    {
        spawner = this;
        enemyAmount = maxEnemies;
    }
    // Update is called once per frame
    void Update()
    {

        if (Time.time - lastSpawnElapsed > timeBetweenSpawns && enemyAmount >= 0 )
        {
            InstantiateEnemy();
            enemyAmount--;
        }
        else if (Time.time -lastSpawnElapsed > timeBetweenSpawns && enemyAmount < 0)
        {
            Debug.Log($"Pelaaja läpäisi kentän tappamalla {maxEnemies} vihollista!");
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
