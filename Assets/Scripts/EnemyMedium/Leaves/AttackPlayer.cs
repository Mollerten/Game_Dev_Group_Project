using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : Leaf<Context>
{
    private Attacking attacking;


    public override Result Run(Context context)
    {
        if (attacking == null)
        attacking = context.transform.GetComponent<Attacking>();

        if(attacking == null)
            return Result.FAILURE;
       if(attacking.CanAttack() == false)
           return Result.RUNNING;

        return Result.SUCCESS;
    }
}
