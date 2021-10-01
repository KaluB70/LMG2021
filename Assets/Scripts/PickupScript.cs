using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public float multiplier = 1.4f; //kerroin
    public float points = 100f; //pisteiden m‰‰r‰
    //public GameObject pickupEffect;   //t‰ll‰ voisi lis‰t‰ effektin, mutta ensimm‰isen persoonan peliss‰ ehk‰ ‰‰nieffekti on t‰rke‰mpi kun visuaalinen

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Powerup(collider);
        }
    }

    void Powerup(Collider player)
    {
        print("Item picked up");
        //Instantiate(pickupEffect, transform.position, transform.rotation);    //tekee pickupeffektin jos sellaista haluaa k‰ytt‰‰

        //t‰ll‰ esimerkikk‰ voisi statsista ottaa vaikka el‰m‰pisteet tai rahat
        PlayerStats stats = player.GetComponent<PlayerStats>();  
        stats.Score += (points * multiplier);   //kerrointa voi vaikka powerupilla lis‰t‰ (esim health pickupit antaa 2x mit‰ yleens‰)
                                                //stats.characterHealth


        //t‰ll‰ hetkell‰ pickup antaa hookshotin
        PlayerMovement hookshot = player.GetComponent<PlayerMovement>();
        hookshot.canUse = true;


        Destroy(gameObject, 0.1f); //poistaa pickupin, pienell‰ delaylla jotta esim. effektit varmasti toimii(?)
    }
}
