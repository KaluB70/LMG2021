using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitpointsScript : MonoBehaviour
{
    public static HitpointsScript ins;
    public Image img;
    public Text hpAmount;

    void Start()
    {
        ins = this;
        hpAmount.text = Math.Floor(PlayerDeath.ins.Hitpoints).ToString();
    }
    public void UpdateHP(float hp)
    {
        img.fillAmount = PlayerDeath.ins.Hitpoints / PlayerDeath.ins.maxHealth;
        hpAmount.text = Math.Floor(PlayerDeath.ins.Hitpoints).ToString();
    }
}
