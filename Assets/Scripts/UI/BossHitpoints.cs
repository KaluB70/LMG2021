using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHitpoints : MonoBehaviour
{
    public static BossHitpoints ins;
    public Image img;
    public EnemyDeath ed;

    void Start()
    {
        ins = this;
    }
    public void UpdateHP(float hp)
    {
        img.fillAmount = ed.Hitpoints / ed.maxHealth;
    }
}
