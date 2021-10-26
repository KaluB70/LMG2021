using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    //anna tama skirpti vihollisille ja tuhottaville esineille
    public float hitpoints = 50f;

    public void TakeDamage (float amount)
    {
        hitpoints -= amount;
        if (hitpoints <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
