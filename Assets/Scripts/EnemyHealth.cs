using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth = 20f;

    private bool isEnemyDead = false;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 && !isEnemyDead)
        {
            Debug.Log("DEAD: " + currentHealth);
            // Death animation goes here
            isEnemyDead = true;
            Destroy(gameObject);
        }
    }

    public bool IsEnemyDead()
    {
        return isEnemyDead;
    }
}
