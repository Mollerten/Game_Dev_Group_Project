using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeponController wc;

    public float minDamageSword = 5f;
    public float maxDamageSword = 10f;
    public float range = 2.7f;
    public GameObject HitParticle;
    private GameObject player;
   
    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Enemy") && wc.isAttacking)
        {
            //Debug.Log(other.name);
            other.GetComponent<Animator>().SetTrigger("Hit");
            if(!other.GetComponent<EnemyHealth>().IsEnemyDead())
            {
                Destroy(Instantiate(HitParticle, new Vector3(other.transform.position.x, this.transform.position.y, other.transform.position.z), other.transform.rotation), 1f);
            }
            // Debug.Log("Sword: " + swordDamageScaling());
            DoSwordAttack(other);
        }
    }


    private void DoSwordAttack(Collider enemy)
    {
            enemy.GetComponent<EnemyHealth>().TakeDamage(swordDamageScaling());
    }

    int swordDamageScaling()
    {
        int damage = Mathf.RoundToInt(Random.Range(minDamageSword, maxDamageSword) + player.GetComponent<PlayerUpgrades>().swordlevel * 3);
        return damage;
    }

    
   
}
