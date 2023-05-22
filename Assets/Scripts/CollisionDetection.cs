using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeponController wc;

    public float minDamage = 5f;
    public float maxDamage = 10f;
    public float range = 0.7f;
    public float attackCooldown = 1f;
    public GameObject HitParticle;
   
    

   

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Enemy") && wc.isAttacking)
        {
            //Debug.Log(other.name);
            other.GetComponent<Animator>().SetTrigger("Hit");
            if(!other.GetComponent<EnemyHealth>().IsEnemyDead())
            {
                Destroy(Instantiate(HitParticle, new Vector3(other.transform.position.x, other.transform.position.y + 0.75f, other.transform.position.z), other.transform.rotation), 1f);
            }
            DoAttack(other);
        }
    }
   
    private void DoAttack(Collider enemy)
    {
                int damage = Mathf.RoundToInt(Random.Range(minDamage, maxDamage));
                enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
    }
   
}
