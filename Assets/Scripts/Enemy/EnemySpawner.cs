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
    private int enemyAmount;
    public bool setActive = false;
    int killsBefore;
    public Collider doorToOpenCollider;
    public ParticleSystem ps;

    private void Start()
    {
        enemyAmount = maxEnemies;
    }
    // Update is called once per frame
    void Update()
    {
        int kills = PlayerStats.Kills;

        if (Time.time - lastSpawnElapsed > timeBetweenSpawns && enemyAmount > 0 && setActive)
        {
            InstantiateEnemy();
            enemyAmount--;
        }
        if (kills == maxEnemies+killsBefore && setActive)
        {
            Debug.Log($"All {maxEnemies} enemies killed!");
            setActive = false;
            if (doorToOpenCollider != null)
            {
                doorToOpenCollider.isTrigger = true;
            }
            if (ps != null)
            {
                ps.Play();
            }
        }
        
    }
    void InstantiateEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        lastSpawnElapsed = Time.time;
    }
    public void SetActive(int kills)
    {
        setActive = true;
        killsBefore = kills;
        enemyAmount = maxEnemies;
    }
    public void SetInactive()
    {
        setActive = false;
    }
}