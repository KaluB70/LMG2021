using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public float damage = 50f;
    public float range = 100f;
    public float fireRate = 2;
    float fireRateTemp;
    public float impactForce = 30f;
    float damageTemp;

    public static WeaponScript ins;
    Timer CoolDown = new Timer();

    public Camera mainCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    AudioSource gunShot;
    
    private float nextTimeToFire = 0f;
    Animator anim;
    //float pitchTemp;

    void Start()
    {
        gunShot = GetComponentInChildren<AudioSource>();
        anim = GetComponent<Animator>();
        ins = this;
    }

    void Update()
    {
        if (Time.timeScale == 0)
            return;
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate; //esim fireRate 4 meinaa 4 kertaa sekunnissa
            Shoot();
            gunShot.Play();
            if (fireRate > 5)
            {
                anim.speed = 3f;
            }
            else if (fireRate < 1.5)
            {
                anim.speed = 0.5f;
            }
            else
            {
                anim.speed = 1f;
            }
            anim.SetTrigger("Shoot");
            
        }
    }
    public void HigherCaliber(float dmg, float duration)
    {
        fireRateTemp = fireRate;
        fireRate *= 0.3f;
        damageTemp = damage;
        damage += dmg;
        //pitchTemp = gunShot.pitch;
        //gunShot.pitch = 0.6f;

        CoolDown.Interval = duration * 1000;
        CoolDown.Enabled = true;

        CoolDown.Elapsed += new ElapsedEventHandler(StopHighCal);
    }

    private void StopHighCal(object sender, ElapsedEventArgs e)
    {
        damage = damageTemp;
        fireRate = fireRateTemp;
        //GameObject.Find("MuzzleFlash (1)").GetComponent<AudioSource>().pitch = pitchTemp;
    }

    public void UpdateF(float value, float duration)
    {
        fireRateTemp = fireRate;
        fireRate += value;
 
        CoolDown.Interval = duration * 1000; // = kuika kauvan eventin laukaisuun
        CoolDown.Enabled = true; // aktivoidaan eventhandler

        CoolDown.Elapsed += new ElapsedEventHandler(StopBrust); //kutsutaan eventhadler metodia kun aika kulunut
    }

    private void StopBrust(object sender, ElapsedEventArgs e)
    {
        fireRate = fireRateTemp;
    }

    void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range)) //tarkistaa osutaanko
        {

            TargetScript target = hit.transform.GetComponent<TargetScript>();
            if (target != null)
            {
                target.TakeDamage(damage); //tehdaan vahinkoa osuttuun kohteeseen
            }

            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<EnemyDeath>().Hitpoints -= damage; //Sama kayttaen DeathScript ja EnemyDeath luokkia (ilman targetScriptia)
                hit.transform.GetComponent<EnemyHitpointsScript>().UpdateHP(damage);
            }
            if (hit.transform.CompareTag("Boss"))
            {
                hit.transform.GetComponent<EnemyDeath>().Hitpoints -= damage; 
                hit.transform.GetComponent<BossHitpoints>().UpdateHP(damage);
            }

            if (hit.rigidbody != null/* && !hit.rigidbody.isKinematic*/)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce); //jos kohteella on rigidbody, lisataan osumaan myos voimaa
            }
            //else if (hit.rigidbody != null && hit.rigidbody.isKinematic)
            //{
            //    hit.rigidbody.transform.position += Vector3.back -hit.normal * impactForce/50; //Lisää kinemaattisiin objecteihin "forcen"
            //}

            GameObject ImpactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)); //impact effekti osuttuun kohtaan 
            Destroy(ImpactGO, 1f); //poistetaan impact effekti jottei peli tayty niilla
        }
    }
}
