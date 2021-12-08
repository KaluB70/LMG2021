using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPickup : MonoBehaviour
{
    public float hp = 100f; //Hp maara
    AudioSource sound;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Hpup(collider);
            sound.Play();
        }
    }

    void Hpup(Collider player)
    {
        print("Item picked up");

        PlayerDeath.ins.AddHp(hp);
        HitpointsScript.ins.UpdateHP(PlayerDeath.ins.Hitpoints);

        Destroy(gameObject, 0.1f); //poistaa pickupin, pienella delaylla jotta esim. effektit varmasti toimii(?)
    }
}
