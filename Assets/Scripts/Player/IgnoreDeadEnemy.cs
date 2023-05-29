using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreDeadEnemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("DeadEnemy"))
        {
            Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
}
