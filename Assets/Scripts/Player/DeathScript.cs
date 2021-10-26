using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeathScript : MonoBehaviour
{
    [SerializeField]

    private float hitpoints = 100f;
    public float damageMultiplier = 1f;


    public float Hitpoints
    {
        get { return hitpoints;  }
        set { float newValue = value;
            if (newValue < hitpoints)
            {
                float change = newValue - hitpoints;
                newValue = hitpoints + change * damageMultiplier;
            }
            hitpoints = Mathf.Clamp(newValue, 0f, 100f);
            PlayerStats.Hitpoints = Mathf.Clamp(newValue, 0f, 100f);
            if (hitpoints == 0f)
            {
                Die();
            }
        }
        
    }

    protected abstract void Die();
}
