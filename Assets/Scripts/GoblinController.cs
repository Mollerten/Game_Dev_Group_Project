using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : MonoBehaviour
{
    public float speed;

    [SerializeField]
    private GameObject player;
    private Transform playerTransform;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < 40f)
        {
            transform.LookAt(new Vector3(playerTransform.position.x, 0, playerTransform.position.z));
            rb.position = Vector3.MoveTowards(transform.position, new Vector3(playerTransform.position.x, 0, playerTransform.position.z), speed* Time.deltaTime);
        }
    }
}
