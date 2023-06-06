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
    private EnemyHealth status;
    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        status = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {
            if (player && !status.IsEnemyDead())
            {
                FollowPlayer();

                StartCoroutine(AttackPlayer());

                SetAnimationParameters();
            }
            else
            {
                agent.isStopped = true;
            }
        }
    }

    private void FollowPlayer()
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

    IEnumerator AttackPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < agent.stoppingDistance && !isAttacking)
        {
            isAttacking = true;
            agent.isStopped = true;
            // This is where you would activate an attack animation
            anim.SetTrigger("Attack");
            agent.enabled = false;
            yield return new WaitForSeconds(0.8f);
            StartCoroutine(EnableNavMeshAgent());
            StartCoroutine(ResetAttackBool());
            if(Vector3.Distance(transform.position, player.transform.position) < agent.stoppingDistance) player.GetComponent<PlayerHealth>().TakeDamage(5);
            //Debug.Log("Attacking");

        }

    }

    private void SetAnimationParameters()
    {
        anim.SetFloat("Speed", agent.desiredVelocity.magnitude);
    }

    IEnumerator EnableNavMeshAgent()
    {
        yield return new WaitForSeconds(1.0f);
        agent.enabled = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(2f);
        isAttacking = false;
    }

    public void slowEnemy(float slowTime)
    {
        StartCoroutine(slowReset(slowTime));
    }

    IEnumerator slowReset(float slowTime)
    {
        float originalSpeed = agent.speed;
        agent.speed = 1.25f;
        yield return new WaitForSeconds(slowTime);
        agent.speed = originalSpeed;
    }
    

}
