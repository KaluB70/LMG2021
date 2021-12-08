using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHookshot : MonoBehaviour
{
    public float multiplier = 1.4f; //kerroin
    public int points = 100; //pisteiden maara
    public AudioSource pickupSound;   //talla voisi lisata effektin, mutta ensimmaisen persoonan pelissa ehka aanieffekti on tarkeampi kun visuaalinen

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Powerup(collider);
            pickupSound.Play();
        }
    }

    void Powerup(Collider player)
    {
        //pickupSound.Play();
        print("Item picked up");
        //Instantiate(pickupEffect, transform.position, transform.rotation);    //tekee pickupeffektin jos sellaista haluaa kayttaa

        //talla esimerkikka voisi statsista ottaa vaikka elamapisteet tai rahat 
        PlayerStats.Score += (int)Mathf.Floor(points * multiplier);   //kerrointa voi vaikka powerupilla lisata (esim health pickupit antaa 2x mita yleensa)
                                                //stats.characterHealth


        //talla hetkella pickup antaa hookshotin
        PlayerMovement hookshot = player.GetComponent<PlayerMovement>();
        hookshot.canUseHookshot = true;


        Destroy(gameObject, 0.1f); //poistaa pickupin, pienella delaylla jotta esim. effektit varmasti toimii(?)
    }
}
