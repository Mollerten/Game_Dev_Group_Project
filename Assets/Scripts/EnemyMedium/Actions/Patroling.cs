using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patroling : MonoBehaviour
{
    NavMeshAgent agent; 
    Animator animator;
    private EnemyHealth status;
    public Transform[] waypoints;
    int waypointIndex;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        status = GetComponent<EnemyHealth>();
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimationParameters(); 

        if(Vector3.Distance(transform.position, target) <= agent.stoppingDistance)
        {
            IterateWaypointIndex();
            UpdateDestination(); 
            Debug.Log("Waypoint reached");
        }
    }

    void UpdateDestination()
        {
            target = waypoints[waypointIndex].position;
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

    

    private void SetAnimationParameters()
    {
        animator.SetFloat("Speed", agent.desiredVelocity.magnitude);
    }
}
