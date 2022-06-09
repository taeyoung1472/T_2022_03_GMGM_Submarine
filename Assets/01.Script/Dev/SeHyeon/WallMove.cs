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
   
     void Update()
    {
        rb.velocity = new Vector3(-4, 0, 0);
    }
}
