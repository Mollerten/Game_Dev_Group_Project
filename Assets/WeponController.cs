using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponController : MonoBehaviour
{
    public GameObject Sword;
    private bool canAttack = true;
    public float AttackCooldown = 1.0f;
    public bool isAttacking = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if(canAttack)
            {
                SwordAttack();
            }
        }
    }


    public void SwordAttack()
    {
        isAttacking = true;
        canAttack = false;
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
    }


    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        canAttack = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }
}
