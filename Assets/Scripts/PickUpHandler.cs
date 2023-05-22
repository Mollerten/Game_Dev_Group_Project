using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpHandler : MonoBehaviour
{

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = player.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag) // using tags to differentiate
        {
            case "HealthPickupSmall":
                player.GetComponent<PlayerHealth>().TakeDamage(-10);
                Destroy(other.gameObject);
                break;

            case "HealthPickupMedium":
                player.GetComponent<PlayerHealth>().TakeDamage(-25);
                Destroy(other.gameObject);
                break;

            default:
                break;
        }
    }
}
