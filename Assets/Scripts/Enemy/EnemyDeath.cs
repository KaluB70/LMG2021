using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : DeathScript
{
    ParticleSystem deathEffect;
    public int ScoreYield = 10;
    static bool lifeStealEnabled = false;
    static float lifeStealAmount;
    public AudioClip deathSound;
    public GameObject[] pickupBuffs;
    AudioSource soundSrc;
    Transform deathPos;
    System.Random rng;
    public static float dropRate = 0.05f;
    int difficulty = 1;

    void Start()
    {
        deathEffect = GetComponentInChildren<ParticleSystem>();
        soundSrc = GetComponent<AudioSource>();
        rng = new System.Random();
        difficulty = PlayerPrefs.GetInt("Difficulty");
        if (difficulty == 0)
        {
            damageMultiplier += 0.4f;
        }
        if (difficulty == 2 && this.tag != "Boss")
        {
            damageMultiplier -= 0.3f;
            if (damageMultiplier <= 0)
            {
                damageMultiplier = 0.01f;
            }
        }
    }
    protected override void Die()
    {
        deathPos = GetComponent<Transform>();
        deathPos.position.Set(deathPos.position.x, deathPos.position.y + 2, deathPos.position.z);
        PlayerStats.Kills++;
        PlayerStats.Score += ScoreYield;
        deathEffect.transform.SetParent(null);
        deathEffect.Play();
        soundSrc.PlayOneShot(deathSound);
        if (Random.Range(0f,1f) <= dropRate)
        {
            Instantiate(pickupBuffs[Random.Range(0, pickupBuffs.Length)], deathPos.position, pickupBuffs[0].transform.rotation);
        }
        Destroy(gameObject);
        if (lifeStealEnabled)
        {
            PlayerDeath.ins.AddHp(lifeStealAmount);
            HitpointsScript.ins.UpdateHP(PlayerDeath.ins.Hitpoints);
        }
        
    }
    public static void EnableLifeSteal(float amount)
    {
        lifeStealEnabled = true;
        lifeStealAmount += amount;
    }

}
