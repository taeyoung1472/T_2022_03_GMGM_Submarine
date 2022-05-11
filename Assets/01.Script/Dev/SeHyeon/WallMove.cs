using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMove : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb= GetComponent<Rigidbody>();  
    }
   
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(1, 0, 0);
    }
}
