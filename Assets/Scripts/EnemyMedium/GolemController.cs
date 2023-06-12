using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemController : MonoBehaviour
{
    public float aggroRange;
    public int damage = 10;
    private Animator anim;
    private GameObject player;
    private NavMeshAgent agent;
    private EnemyHealth status;
    private bool isAttacking = false;
    public GameObject[] waypoints;
    
    int waypointIndex;
    Vector3 target;
    // public AudioClip[] attackSounds;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        status = GetComponent<EnemyHealth>();

        // UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {
        
            if (player && !status.IsEnemyDead() && !player.GetComponent<PlayerHealth>().IsDead())
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
            Patroling();
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
            if(Vector3.Distance(transform.position, player.transform.position) < agent.stoppingDistance) player.GetComponent<PlayerHealth>().TakeDamage(damage);
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

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].GetComponent<Transform>().position;
        agent.SetDestination(target);
    }


    void IterateWaypointIndex()
    {
        waypointIndex++;
        if(waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

    void Patroling ()
    {
        if(Vector3.Distance(transform.position, target) <= agent.stoppingDistance)
        {
            IterateWaypointIndex(); 
            Debug.Log("Waypoint reached");
        }
        UpdateDestination();
    }

    public void SetWaypointGroup(string group)
    {
        waypoints = GameObject.FindGameObjectsWithTag(group);
        Debug.Log($"{waypoints[0].transform.localPosition}");
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }
}
