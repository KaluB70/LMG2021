using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerDungeon : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    float lastSpawnElapsed;
    public float timeBetweenSpawns = 5F;
    void Update()
    {
        if (Time.time - lastSpawnElapsed > timeBetweenSpawns)
        {
            InstantiateEnemy();
        }
        switch (PlayerStats.Kills)
        {
            case 10:
                timeBetweenSpawns = 4f;
                break;
            case 25:
                timeBetweenSpawns = 3f;
                break;
            case 50:
                timeBetweenSpawns = 2f;
                break;
            case 75:
                timeBetweenSpawns = 1f;
                break;
            case 100:
                timeBetweenSpawns = 0.5f;
                break;
            default:
                break;
        }
        if (PlayerStats.Kills > 100)
        {
            timeBetweenSpawns = 0.3f;
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