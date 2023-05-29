using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponController : MonoBehaviour
{
    public InputHandler _input;
    // sword
    public GameObject Sword;
    public float swordAttackCooldown = 1.0f;
    public bool swordCanAttack = true;
    // axe
    public GameObject Axe;
    public float axeAttackCooldown = 2.0f;
    public bool isAttacking = false;
    public bool axeCanAttack = true;
    public GameObject player;
    



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        yield return new WaitForSeconds(CooldownScalingSword());
        swordCanAttack = true;
        
    }

    IEnumerator ResetAxeAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(CooldownScalingAxe());
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

    public float CooldownScalingSword()
    {
        int swordlevel = player.GetComponent<PlayerUpgrades>().swordlevel;
        
        if (swordlevel < 5)
        {
            swordAttackCooldown = 1 - (swordlevel * 0.1f);
        }
        if (swordlevel >= 5)
        {
            swordAttackCooldown = 0.50f;
        }
        // Debug.Log("Sword cooldown: " + swordAttackCooldown);
        return swordAttackCooldown;
    }

    public float CooldownScalingAxe()
    {
        int axelevel = player.GetComponent<PlayerUpgrades>().axeLevel;
        
        if (axelevel < 5)
        {
            axeAttackCooldown = 2 - (axelevel * 0.2f);
        }
        if (axelevel >= 5)
        {
            axeAttackCooldown = 1.0f;
        }
        //  Debug.Log("Axe cooldown: " + axeAttackCooldown);
        return axeAttackCooldown;
    }

}
