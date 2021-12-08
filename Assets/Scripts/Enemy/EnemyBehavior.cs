using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public bool demonBat;
    public bool slime;
    public bool snake;
    public bool golem;
    public bool boss;
    NavMeshAgent agent;     //Vihollinen
    Transform player;       //Pelaaja
    public Vector3 destination;    //Kohde
    public float attackDamage = 15f;
    public float attackSpeed = 2f;
    float lastAttack;
    Animator anim;
    public AudioClip hit;
    AudioSource soundSrc;
    EnemyDeath ed;
    public bool active;
    public Transform restZone;
    int difficulty = 1;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        destination = agent.destination;
        ed = GetComponent<EnemyDeath>();
        soundSrc = GetComponent<AudioSource>();
        soundSrc.Play();
        active = true;
        difficulty = PlayerPrefs.GetInt("Difficulty");
        if (difficulty == 0)
        {
            attackDamage *= 0.7f;
        }
        if (difficulty == 2)
        {
            attackDamage *= 1.3f;
        }
    }

    private void LateUpdate()
    {
        float speed = this.agent.velocity.magnitude;
        anim.SetFloat("WalkSpeed", speed);
    }

    void Update()
    {
        if (Time.time - lastAttack >= attackSpeed && active)
        {
            if (agent.hasPath && agent.remainingDistance <= agent.stoppingDistance && agent.remainingDistance > 0.1f)
            {
                if (!boss)
                {
                    anim.SetBool("Move", false);
                }
                PlayerDeath.ins.Hitpoints -= attackDamage;
                HitpointsScript.ins.UpdateHP(PlayerDeath.ins.Hitpoints);
                lastAttack = Time.time;
                anim.SetTrigger("Attack");
                soundSrc.PlayOneShot(hit);
                //Debug.Log(this.name + "damaged you from " + agent.remainingDistance + " away");
                //Debug.Log("This agent has path to player: " + agent.hasPath);
                if (slime && !PlayerMovement.slowActive)
                {
                    GameObject.Find("player").GetComponent<PlayerMovement>().StartCoroutine("TempSlow");
                    
                }
                if (snake)
                {
                    ed.Hitpoints += 10;
                }
            }
        }
        if (Vector3.Distance(player.position, destination) > 2.0f && active)
        {
            destination = player.position;
            agent.destination = destination;
            if (!boss)
            {
                anim.SetBool("Move", true);
            }
            
        }
        else if ((Vector3.Distance(player.position, destination) > 2.0f && !active))
        {
            destination = restZone.position;
            agent.destination = destination;
            if (!boss)
            {
                anim.SetBool("Move", true);
            }
        }
    }

}
