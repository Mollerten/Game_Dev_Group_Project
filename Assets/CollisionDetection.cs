using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeponController wc;
    

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Enemy" && wc.isAttacking)
        {
            Debug.Log(other.name);
            other.GetComponent<Animator>().SetTrigger("Hit");
        }
    }
   
}
