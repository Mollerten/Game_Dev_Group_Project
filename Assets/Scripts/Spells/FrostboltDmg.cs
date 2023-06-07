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
            DoFrostboltAttack(other);
       
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
            slowTime = 2 + (level*0.2f);
        }
        return slowTime;
    }

    
    
    public void DoFrostboltAttack(Collider enemy)
    {
        Debug.Log("enemy: " + enemy.gameObject.name);
        enemy.gameObject.GetComponent<EnemyHealth>().TakeDamage(damageScaling());
        if (enemy.gameObject.GetComponent<GoblinController>() != null) enemy.gameObject.GetComponent<GoblinController>().slowEnemy(slowScale());
        if (enemy.gameObject.GetComponent<GolemController>() != null) enemy.gameObject.GetComponent<GolemController>().slowEnemy(slowScale());

    }


    
}
