using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponController : MonoBehaviour
{
    public InputHandler _input;
    // sword
    public GameObject Sword;
    public float swordAttackCooldown = 1.0f;
    // axe
    public GameObject Axe;
    public float axeAttackCooldown = 2.0f;
    public bool isAttacking = false;

    private bool swordCanAttack = true;
    private bool axeCanAttack = true;
    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_input.Fire)
        {
            if(swordCanAttack)
            {
                SwordAttack();
            }
        }
        if(_input.RightFire)
        {
            if(axeCanAttack)
            {
                AxeAttack();
            }
        }
    }

    

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(swordAttackCooldown);
        swordCanAttack = true;
        
    }

    IEnumerator ResetAxeAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(axeAttackCooldown);
        axeCanAttack = true;
        
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }

    public void SwordAttack()
    {
        isAttacking = true;
        swordCanAttack = false;
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
    }

    public void AxeAttack()
    {
        isAttacking = true;
        axeCanAttack = false;
        Animator anim = Axe.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAxeAttackCooldown());
    }


}
