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
            doAttack();
        }
    }
   
    private void doAttack()
    {
        Ray rayFrom = new(transform.position, transform.forward);
        if (Physics.Raycast(rayFrom, out RaycastHit hit, range, 1 << 3))
        {
            if ((bool)!hit.collider.GetComponent<EnemyHealth>()?.IsEnemyDead())
            {
                int damage = Mathf.RoundToInt(Random.Range(minDamage, maxDamage));
                hit.collider.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }
    } 
   
}
