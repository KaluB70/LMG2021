using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_CalUp : MonoBehaviour
{
    float addDamage = 100f;
    public static Pickup_CalUp ins;
    public static float duration = 10f;
    AudioSource sound;
    private void Start()
    {
        ins = this;
        sound = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" && !BuffsUIScript.ins.CheckActive())
        {
            FireUp();
            BuffsUIScript.ins.SetBuff("Higher Caliber", duration);
            sound.Play();
        }
    }

    private void FireUp()
    {
        print("Item picked up");

        WeaponScript.ins.HigherCaliber(addDamage, duration);

        Destroy(gameObject, 0.1f); //poistaa pickupin, pienella delaylla jotta esim. effektit varmasti toimii(?)
    }
}
