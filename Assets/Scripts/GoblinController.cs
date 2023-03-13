using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinController : MonoBehaviour
{
    public float aggroRange;
    private Animator anim;
    private GameObject player;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
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

            SetAnimationParameters();
        }
    }

    private void SetAnimationParameters()
    {
        anim.SetFloat("Speed", agent.desiredVelocity.magnitude);
    }
}
