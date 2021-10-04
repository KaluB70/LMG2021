using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public float damage = 40f;
    public float range = 100f;
    public float fireRate = 2;
    public float impactForce = 30f;

    public Camera mainCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    AudioSource gunShot;

    private float nextTimeToFire = 0f;

    void Start()
    {
        gunShot = GetComponentInChildren<AudioSource>();
    }
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate; //esim fireRate 4 meinaa 4 kertaa sekunnissa
            Shoot();
            gunShot.Play();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range)) //tarkistaa osutaanko
        {
            Debug.Log(hit.transform.name);



            TargetScript target = hit.transform.GetComponent<TargetScript>();
            if (target != null)
            {
                target.TakeDamage(damage); //tehdaan vahinkoa osuttuun kohteeseen
            }

            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<EnemyDeath>().Hitpoints -= damage; //Sama kayttaen DeathScript ja EnemyDeath luokkia (ilman targetScriptia)
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
