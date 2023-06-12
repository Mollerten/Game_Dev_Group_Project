using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onHitDeath : MonoBehaviour
{
    public GameObject player;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerHealth>().TakeDamage(player.GetComponent<PlayerHealth>().maxHealth);
        }
    }
}
