using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDmg : MonoBehaviour
{
    public float minDamage = 20f;
    public float maxDamage = 25f;
    public float lifeTime =  3f;
    

    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            float damage = Random.Range(minDamage, maxDamage);
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage((int)damage);
            Destroy(gameObject);
        }
    }
}
