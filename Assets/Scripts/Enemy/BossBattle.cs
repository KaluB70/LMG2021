using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossBattle : MonoBehaviour
{
    float hp, maxHp;
    public Transform[] spawnPointsP1;
    public Transform[] spawnPointsP2;
    public Transform[] spawnPointsP3;
    public GameObject[] enemies;
    public Transform restZone;
    int enemyPhase = 0;
    EnemyBehavior eb;
    int killsBefore;
    EnemyDeath ed;
    public GameObject bossHealthBar;
    public GameObject boss;
    int enemiesToKill;
    public GameObject victoryCollider;
    float damageMultiplierTemp;
    public static BossBattle ins;

    void Start()
    {
        hp = GetComponent<EnemyDeath>().Hitpoints;
        maxHp = GetComponent<EnemyDeath>().maxHealth;
        eb = GetComponent<EnemyBehavior>();
        ed = GetComponent<EnemyDeath>();
        ed.damageMultiplier = 0.01f;
        ins = this;
    }

    void Update()
    {

        hp = GetComponent<EnemyDeath>().Hitpoints;
        if (Time.timeScale == 0)
        {
            bossHealthBar.SetActive(false);
        }
        else
        {
            bossHealthBar.SetActive(true);
        }
        if (hp / maxHp <= 0.75f && enemyPhase == 0)
        {
            Debug.Log("Phase1");
            enemiesToKill = HandleBossPhase1();
        }
        if (hp / maxHp <= 0.5f && enemyPhase == 1)
        {
            Debug.Log("Phase2");
            enemiesToKill = HandleBossPhase2();
        }
        if (hp/maxHp <= 0.25f && enemyPhase == 2)
        {
            Debug.Log("Phase3");
            enemiesToKill = HandleBossPhase3();
        }
        if (killsBefore + enemiesToKill == PlayerStats.Kills)
        {
            ResetBoss();
        }
        
    }
    private void OnDestroy()
    {
        bossHealthBar.SetActive(false);
        victoryCollider.SetActive(true);
        Debug.Log("Player won!");
    }
    int HandleBossPhase1()
    {
        damageMultiplierTemp = ed.damageMultiplier;
        ed.damageMultiplier = 0;
        killsBefore = PlayerStats.Kills;
        eb.active = false;
        eb.destination = restZone.position;
        enemyPhase = 1;
        for (int i = 0; i < 4; i++)
        {
            Instantiate(enemies[i], spawnPointsP1[i].position, spawnPointsP1[i].rotation);
        }
        return 4;
    }
    int HandleBossPhase2()
    {
        damageMultiplierTemp = ed.damageMultiplier;
        ed.damageMultiplier = 0;
        killsBefore = PlayerStats.Kills;
        eb.active = false;
        eb.destination = restZone.position;
        enemyPhase = 2;
        for (int i = 0; i < 4; i++)
        {
            Instantiate(enemies[i], spawnPointsP1[i].position, spawnPointsP1[i].rotation);
            Instantiate(enemies[i], spawnPointsP2[i].position, spawnPointsP2[i].rotation);
        }
        return 8;
    }
    int HandleBossPhase3()
    {
        damageMultiplierTemp = ed.damageMultiplier;
        ed.damageMultiplier = 0;
        killsBefore = PlayerStats.Kills;
        eb.active = false;
        eb.destination = restZone.position;
        enemyPhase = 3;
        
        for (int i = 0; i < 4; i++)
        {
            Instantiate(enemies[i], spawnPointsP1[i].position, spawnPointsP1[i].rotation);
            Instantiate(enemies[i], spawnPointsP2[i].position, spawnPointsP2[i].rotation);
            Instantiate(enemies[i], spawnPointsP3[i].position, spawnPointsP3[i].rotation);
        }
        return 12;
    }
    void ResetBoss()
    {
        eb.active = true;
        ed.damageMultiplier = 0.01f;
    }
}
