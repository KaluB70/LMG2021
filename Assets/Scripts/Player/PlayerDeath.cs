using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : DeathScript
{
    void Update()
    {
        Hitpoints += Time.deltaTime;
        PlayerStats.Hitpoints += Time.deltaTime;
        HitpointsScript.ins.UpdateHP(PlayerStats.Hitpoints);
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
        Time.timeScale = 0f;
        print("Pelaaja hävisi!");
    }
}
