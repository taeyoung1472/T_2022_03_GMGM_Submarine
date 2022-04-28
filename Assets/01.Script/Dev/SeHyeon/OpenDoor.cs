using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    
    GameObject[] door ;
    BoxCollider boxCollider;
    void Start()
    {
       
        door = GameObject.FindGameObjectsWithTag("Door");
        foreach(GameObject dor in door)
        {
            dor.AddComponent<OpenDoor>();   
        }
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseEnter()
    {
        if(Input.GetKeyDown(KeyCode.E))
            {
            boxCollider.isTrigger = false;
        }
    }
}
