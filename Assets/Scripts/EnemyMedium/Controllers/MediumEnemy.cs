using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemy : MonoBehaviour
{
    private Context context;
    
    Node<Context> root;


    List<Node<Context>> layer1 = new List<Node<Context>>();
    List<Node<Context>> layer2 = new List<Node<Context>>();

    Node<Context> moveTo = new RunToPlayer();
    Node<Context> attackTarget = new AttackPlayer();
    Node<Context> patrolArea = new Patrol();
    Node<Context> patol = new Patrol();




    void Awake()
    {
    // layer 1
        layer1.Add(moveTo);
        layer1.Add(patrolArea);
        

    // layer 2 

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
