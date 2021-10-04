using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : DeathScript
{
    ParticleSystem deathEffect;

    void Start()
    {
        deathEffect = GetComponentInChildren<ParticleSystem>();
    }
    protected override void Die()
    {
        deathEffect.transform.SetParent(null);
        deathEffect.Play();
        Destroy(gameObject);
    }

}
