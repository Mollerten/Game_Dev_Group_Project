using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private float attackCooldown = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       attackCooldown -= Time.deltaTime;
    
    }

     private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player") && attackCooldown <= 0)
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(10);
            attackCooldown = 2.0f;
        }
    }
}
