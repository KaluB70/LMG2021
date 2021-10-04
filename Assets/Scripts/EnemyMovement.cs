using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;     //Vihollinen
    Transform player;       //Pelaaja
    Vector3 destination;    //Kohde
    PlayerDeath playerDeath;
    public float attackDamage = 15f;
    public float attackSpeed = 2f;
    float lastAttack;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        destination = agent.destination;
        playerDeath = player.GetComponent<PlayerDeath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastAttack >= attackSpeed)
        {
            if (agent.hasPath && agent.remainingDistance <= agent.stoppingDistance)
            {
                playerDeath.Hitpoints -= attackDamage;
                lastAttack = Time.time;
            }
        }
        if (Vector3.Distance(player.position, destination) > 1.0f)
        {
            destination = player.position;
            agent.destination = destination;

        }
    }
}
