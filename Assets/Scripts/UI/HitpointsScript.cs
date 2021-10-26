using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitpointsScript : MonoBehaviour
{
    public static HitpointsScript ins;
    public Image img;

    void Start()
    {
        ins = this;
    }
    public void UpdateHP(float hp)
    {
        img.fillAmount = PlayerStats.Hitpoints / 100f;
    }
}
