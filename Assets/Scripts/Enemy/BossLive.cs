using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLive : MonoBehaviour
{
    public GameObject boss;
    public AudioClip bossMusic; 
    public GameObject door;
    public AudioSource music;
    public GameObject bossHealthbar;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            door.SetActive(true) ;
            music.Stop();
            StartBattle();
            Destroy(this);
        }
    }

    void StartBattle()
    {
        bossHealthbar.SetActive(true);
        print("started boss battle");
        music.GetComponent<MusicScript>().enabled = false;
        music.clip = bossMusic;
        music.Play();
        boss.SetActive(true); //aktivoi bossi
    }
}
