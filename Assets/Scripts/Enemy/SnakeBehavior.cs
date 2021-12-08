using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnakeBehavior : MonoBehaviour
{
    NavMeshAgent agent;     //Vihollinen
    Transform player;       //Pelaaja
    Vector3 destination;    //Kohde
    public float attackDamage = 15f;
    public float attackSpeed = 2f;
    float lastAttack;
    Animator anim;
    AudioClip hit;
    AudioClip death;
    private static int ANIMATOR_PARAM_WALK_SPEED =
        Animator.StringToHash("WalkSpeed");

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("player").transform;
        destination = agent.destination;
    }

    private void LateUpdate()
    {
        float speed = this.agent.velocity.magnitude;
        this.anim.SetFloat(ANIMATOR_PARAM_WALK_SPEED, speed);
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastAttack >= attackSpeed)
        {
            if (agent.hasPath && agent.remainingDistance <= agent.stoppingDistance && agent.remainingDistance > 0.1f)
            {
                PlayerDeath.ins.Hitpoints -= attackDamage;
                HitpointsScript.ins.UpdateHP(PlayerDeath.ins.Hitpoints);
                lastAttack = Time.time;
                GameObject.Find("player").GetComponent<PlayerMovement>().StartCoroutine("TempSlow");
                anim.SetTrigger("Attack");
            }
        }
        if (Vector3.Distance(player.position, destination) > 1.0f)
        {
            destination = player.position;
            agent.destination = destination;
            anim.SetBool("Move", true);
        }
    }
}
