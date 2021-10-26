using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : DeathScript
{
    ParticleSystem deathEffect;
    public int ScoreYield = 10;

    void Start()
    {
        deathEffect = GetComponentInChildren<ParticleSystem>();
    }
    protected override void Die()
    {
        PlayerStats.Kills++;
        PlayerStats.Score += ScoreYield;
        deathEffect.transform.SetParent(null);
        deathEffect.Play();
        Destroy(gameObject);
    }

}
