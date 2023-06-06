using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private Attacking attacking;
    private Movement locating;
    private Patroling patroling;



   private void Awake() 
   {
           attacking = GetComponent<Attacking>();
           locating = GetComponent<Movement>();
           patroling = GetComponent<Patroling>();
   }
     
   
}
