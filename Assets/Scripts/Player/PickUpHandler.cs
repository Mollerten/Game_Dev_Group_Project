using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUpHandler : MonoBehaviour
{
    [SerializeField] InputHandler _input;
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

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("bossDoor"))
        {
            if (_input.Interact && SceneManager.GetActiveScene().name == "Level1Scene")
            {
                SceneManager.LoadScene("Level2Scene", LoadSceneMode.Single);
            }
        }
    }
}
