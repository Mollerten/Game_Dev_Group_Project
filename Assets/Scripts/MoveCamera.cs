using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

<<<<<<<< HEAD:Assets/Scripts/MoveCamera.cs
    public Transform cameraPostion;

    // Start is called before the first frame update
    void Start()
    {
========
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
>>>>>>>> main:Assets/Scripts/FloatyMcSinky.cs
        
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<<< HEAD:Assets/Scripts/MoveCamera.cs
        transform.position = cameraPostion.position;
========
        if(Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector3.up * 100);
        }
        
>>>>>>>> main:Assets/Scripts/FloatyMcSinky.cs
    }
}
