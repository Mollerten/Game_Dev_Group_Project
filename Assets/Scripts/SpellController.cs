using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{

    public GameObject spellPrefab;
    public float spellSpeed = 10.0f;
    public Transform spellSpawnPoint;
    public InputHandler _input;
    public float spellCooldown = 1.0f;
    private bool spellEcanAttack = true;

    private bool isAttacking = false;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_input.EAction)
        {
            if(spellEcanAttack)
            {
                SpellAttack();
            }
        }
    }



    void SpellAttack()
    {
        spellEcanAttack = false;
        isAttacking = true;

        var spell = Instantiate(spellPrefab, spellSpawnPoint.position, spellSpawnPoint.rotation);
        spell.GetComponent<Rigidbody>().velocity = spell.transform.forward * spellSpeed;
        StartCoroutine(ResetAttackCooldown());
    }



    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(spellCooldown);
        spellEcanAttack = true;
        
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }
}
