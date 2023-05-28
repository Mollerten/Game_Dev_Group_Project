using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{

    public GameObject spellPrefab;
    public float spellSpeed = 50.0f;
    public Transform spellSpawnPoint;
    public InputHandler _input;
    private GameObject player;
    public float spellCooldown = 2.0f;
    private bool spellEcanAttack = true;
    




    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        

        var spell = Instantiate(spellPrefab, spellSpawnPoint.position, spellSpawnPoint.rotation);
        spell.GetComponent<Rigidbody>().velocity = spell.transform.forward * spellSpeedScaling();
        StartCoroutine(ResetAttackCooldown());
    }



    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(spellCoolDownScaling());
        spellEcanAttack = true;
        
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(0.1f);
        spellEcanAttack = false;
        
    }

    float spellCoolDownScaling()
    {
        int spellLevel = player.GetComponent<PlayerUpgrades>().fireballLevel;
        
        if (spellLevel < 7)
        {
            spellCooldown = 2.0f - (spellLevel * 0.25f);
        }
        if (spellLevel >= 7)
        {
            spellCooldown = 0.50f;
        }
        Debug.Log("Spell cooldown: " + spellCooldown);
        return spellCooldown;
    }

    float spellSpeedScaling()
    {
        int spellLevel = player.GetComponent<PlayerUpgrades>().fireballLevel;
        if(spellLevel > 1)
        {
            spellSpeed = 50.0f + (spellLevel * 2.5f);
        }
        return spellSpeed;
    }
}
