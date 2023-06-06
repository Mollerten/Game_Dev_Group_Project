using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : Leaf<Context>
{
    private Patroling patrol;
    private Movement runToPlayer;

    public override Result Run(Context context) 
    {
        patrol = context.transform.GetComponent<Patroling>();
        runToPlayer = context.transform.GetComponent<Movement>();

        if(patrol == null)
            return Result.FAILURE;

        

        return Result.SUCCESS;
    } 
}
