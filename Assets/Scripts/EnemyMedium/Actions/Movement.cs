using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private NavMeshAgent m_navMeshAgent;
    private Vector3 m_startPos;
    // private bool m_headingTowardStartPos = false;

    private Animator m_anim;
    // [SerializeField] private float m_wanderRange = 15.0f;
    [SerializeField] private float aggroRange = 10.0f;
    private Vector3 m_targetPos;

    private void Awake() 
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_startPos = transform.position;
        m_anim = GetComponentInChildren<Animator>();
    } 

    private void LateUpdate()
    {
        m_anim.SetFloat("Speed", m_navMeshAgent.desiredVelocity.magnitude);
    }
    // move to a position and stop at stopping distance
    public void MoveTo(Vector3 targetPosition) 
    {
      
        if (Vector3.Distance(transform.position, targetPosition) < GetStoppingDistance()) 
        {
            m_navMeshAgent.isStopped = true;
            return;
        }

        m_navMeshAgent.isStopped = false;
        m_navMeshAgent.SetDestination(targetPosition);
    }

    /* public void Wander()
    {
        if (m_navMeshAgent.isStopped)
        {
            return;
        }

        if (!m_headingTowardStartPos && Vector3.Distance(transform.position, m_startPos) > m_wanderRange)
        {
            // return towards startPos
            m_targetPos = m_startPos;
            m_headingTowardStartPos = true;
            StartCoroutine(Idle(Random.Range(1F, 4F)));
        }
        else if (Vector3.Distance(transform.position, m_targetPos) < GetStoppingDistance()) 
        {
            // wander about
            m_targetPos = GetRandomPosition();
            m_headingTowardStartPos = false;
            StartCoroutine(Idle(Random.Range(1F, 4F)));
        }

        MoveTo(m_targetPos);
    } */



   /*  private Vector3 GetRandomPosition()
    {
        Vector3 randomPos = UnityEngine.Random.insideUnitSphere * (m_wanderRange * 1.25f) + transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPos, out hit, (m_wanderRange * 1.25f), NavMesh.AllAreas);
        return hit.position;
    } */

    public void RunToPlayer(Vector3 playerPosition)
    {
     if (Vector3.Distance(transform.position, playerPosition) > aggroRange)
        {
            MoveTo(playerPosition);
        }
    }

    public float GetStoppingDistance() => m_navMeshAgent.stoppingDistance;

    private IEnumerator Idle(float idleTime)
    {
        m_navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(idleTime);
        m_navMeshAgent.isStopped = false;
    }
}
