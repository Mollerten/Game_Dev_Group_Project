using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Enemy01 : MonoBehaviour
{
    private Context context;
    
    Node<Context> root;


    List<Node<Context>> layer1 = new List<Node<Context>>();

    Node<Context> moveTo = new RunToPlayer();
    Node<Context> attackTarget = new AttackPlayer();
    Node<Context> findTargetInRange = new LocatePlayer();
    Node<Context> patol = new Patrol();




    void Awake()
    {
    // layer 1
        layer1.Add(findTargetInRange);
        layer1.Add(moveTo);
        layer1.Add(attackTarget);

    // root
        root = new Sequence<Context>(layer1);

        context = GetComponent<Context>();
        
        StartCoroutine(Loop());
    }

    private IEnumerator Loop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            root.Run(context);
        }
    }
}
