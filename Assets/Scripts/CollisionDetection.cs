using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeponController wc;

    public float minDamage = 5f;
    public float maxDamage = 10f;
    public float range = 0.7f;


    

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Enemy" && wc.isAttacking)
        {
            Debug.Log(other.name);
            other.GetComponent<Animator>().SetTrigger("Hit");
            doAttack(other);
        }
    }
   
    private void doAttack(Collider enemy)
    {
                int damage = Mathf.RoundToInt(Random.Range(minDamage, maxDamage));
                enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
    }
   
}
