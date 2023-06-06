using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunToPlayer : Leaf<Context>
{
    private Movement runToPlayer;

    public override Result Run(Context context) 
    {
        runToPlayer = context.transform.GetComponent<Movement>();

        if (context == null)
        {
            Debug.LogError("Context is null");
            return Result.FAILURE;
        }
        if (context.navMeshAgent == null) 
        {
            Debug.LogError("No NavMeshAgent set on Context");
            return Result.FAILURE;
        }

        if (context.Target == null)
        {
            return Result.FAILURE;
        }

        context.navMeshAgent.SetDestination(context.Target.position);
        if ((context.transform.position - context.Target.position).magnitude 
        > context.navMeshAgent.stoppingDistance)
        {
            return Result.RUNNING;
        }
    
    

        return Result.SUCCESS;
    }
}
