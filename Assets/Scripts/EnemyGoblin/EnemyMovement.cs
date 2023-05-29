using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float aggroRange;
    private GameObject player;
    private NavMeshAgent agent;
    private EnemyHealth status;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        status = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player && !status.IsEnemyDead())
        {
            if (Vector3.Distance(transform.position, player.transform.position) < aggroRange)
            {
                agent.isStopped = false;
                // This is where you would activate a walk/run animation
                agent.SetDestination(player.transform.position);

            }
            else
            {
                // This is where you would activate an idle animation
                // OR make the enemy go back to a "home" position
                // defined earlier
                agent.isStopped = true;
            }
        }
        else
        {
            agent.isStopped = true;
        }
        Debug.Log("Enemy movespeed: " + agent.speed);
    }




}
