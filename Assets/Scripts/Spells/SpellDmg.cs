using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDmg : MonoBehaviour
{
    public float minDamage = 10f;
    public float maxDamage = 17f;
    public float lifeTime =  3f;
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
            Destroy(gameObject);
        }
    }

    public int damageScaling()
    {
        int damage = 10;
        level = player.GetComponent<PlayerUpgrades>().fireballLevel;
    
        if (level >= 0)
        {
         damage = Mathf.RoundToInt(Random.Range(minDamage, maxDamage) + ((level*0.8f)*5));
        }

        Debug.Log("Spell damage: " + damage);
        return damage;
    }
}
