using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{

    [SerializeField] private float baseDmg = 10f;
    private bool canAttack = true;
    [SerializeField] private float attackDelay = 0.8f;
    
    private Animator animator;


    public bool CanAttack() => canAttack;
    internal void Attack(GameObject target)
    {
        canAttack = false;
        
        if (animator) animator.SetTrigger("Attack");

        transform.LookAt(target.transform.position);
        
        float dmg = CalculateDamage();
        target.GetComponent<PlayerHealth>().TakeDamage((int)dmg);

        StartCoroutine(AttackDelay(attackDelay));
    }

    private float CalculateDamage()
    {
        return baseDmg; // TODO: weaponDamage, Crit chance, etc
    }

    private IEnumerator AttackDelay(float sec)
    {
        yield return new WaitForSeconds(sec);
        canAttack = true;
    }
}
