using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : DeathScript
{
    ParticleSystem deathEffect;
    public int killAmount;

    void Start()
    {
        deathEffect = GetComponentInChildren<ParticleSystem>();
    }
    protected override void Die()
    {
        KillCounterScript.instance.AddKills(killAmount);
        deathEffect.transform.SetParent(null);
        deathEffect.Play();
        Destroy(gameObject);
    }

}
