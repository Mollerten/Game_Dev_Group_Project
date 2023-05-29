using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FrostboltDmg : MonoBehaviour
{
    public float minDamage = 6f;
    public float maxDamage = 12f;
    public float lifeTime =  3f;
    public float slowTime = 2f;
    private GameObject player;
    public int level = 1;
    
    
    

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, lifeTime);
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damageScaling());
            for (int i = 0; i < slowScale(); i++)
            {
                other.gameObject.GetComponent<NavMeshAgent>().speed = 0.5f;
    
                if (i == slowScale() - 1)
                {
                    other.gameObject.GetComponent<NavMeshAgent>().speed = 2f;
                }
            }
            
            other.gameObject.GetComponent<NavMeshAgent>().speed = 2f;
            
            Destroy(gameObject);
        }
    }

    

    public int damageScaling()
    {
        int damage = 10;
        level = player.GetComponent<PlayerUpgrades>().frostboltLevel;
    
        if (level >= 0)
        {
         damage = Mathf.RoundToInt(Random.Range(minDamage, maxDamage) + ((level*0.7f)*3));
        }
        // Debug.Log("Spell damage: " + damage);
        return damage;
    }

    public float slowScale()
    {
        
        level = player.GetComponent<PlayerUpgrades>().frostboltLevel;
    
        if (level >= 0)
        {
            slowTime = 2 + (level*1.5f);
        }
        return slowTime;
    }
}
