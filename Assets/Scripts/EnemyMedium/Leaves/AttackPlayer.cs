using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : Leaf<Context>
{
    private GoblinController goblinController;
   
  

    /* public override Result Run(Context context)
    {
        if (goblinController == null)
            goblinController = context.transform.GetComponent<GoblinController>();
        
        if (goblinController == null)

        goblinController();

        return Result.SUCCESS;

    } */
}
