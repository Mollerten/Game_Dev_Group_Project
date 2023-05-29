using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectionAxe : MonoBehaviour
{
    public WeponController wc;

    public float minDamageAxe = 8f;
    public float maxDamageAxe = 15f;
    public float range = 1.6f;
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
                Destroy(Instantiate(HitParticle, new Vector3(other.transform.position.x, other.transform.position.y + 0.75f, other.transform.position.z), other.transform.rotation), 1f);
            }
            // Debug.Log("Axe: " + axeDamageScaling());
            DoAttack(other);
        }
    }


    private void DoAttack(Collider enemy)
    {
            enemy.GetComponent<EnemyHealth>().TakeDamage(axeDamageScaling());
    }

    int axeDamageScaling()
    {
        int damage = Mathf.RoundToInt(Random.Range(minDamageAxe, maxDamageAxe) + (player.GetComponent<PlayerUpgrades>().axeLevel) * 2.5f);
        return damage;
    }

    
   
}
