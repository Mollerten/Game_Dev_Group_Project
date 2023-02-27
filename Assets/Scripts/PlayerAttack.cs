using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public float minDamage = 5f;
    public float maxDamage = 10f;
    public float range = 0.7f;
    //public float critMult = 1.5f;
    //public float critChance = 10f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse pressed");
            HitCheck();
        }
    }

    void HitCheck()
    {
        Ray rayFrom = new(transform.position, transform.forward);
        if (Physics.Raycast(rayFrom, out RaycastHit hit, range))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                float damage = Random.Range(minDamage, maxDamage);
                Debug.Log("HIT: " + damage);
                hit.collider.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }
    }
}
