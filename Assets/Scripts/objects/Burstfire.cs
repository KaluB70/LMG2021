using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Burstfire : MonoBehaviour
{
    float AddFireR = 7;
    public static Burstfire ins;
    public static float duration = 10f;
    AudioSource sound;
    private void Start()
    {
        ins = this;
        sound = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && !BuffsUIScript.ins.CheckActive())
        {
            FireUp();
            BuffsUIScript.ins.SetBuff("Rapid Fire", duration);
            sound.Play();
        }
    }

    private void FireUp()
    {
        print("Item picked up");

        WeaponScript.ins.UpdateF(AddFireR, duration);

        Destroy(gameObject, 0.1f); //poistaa pickupin, pienella delaylla jotta esim. effektit varmasti toimii(?)
    }
}
