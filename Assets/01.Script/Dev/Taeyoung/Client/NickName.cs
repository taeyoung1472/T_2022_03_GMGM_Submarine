using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NickName : MonoBehaviour
{
    private Transform lookObject;
    public void Set(Transform target)
    {
        lookObject = target;
    }
    void Update()
    {
        try
        {
            transform.LookAt(lookObject.position);
        }
        catch(Exception ex)
        {
            Debug.LogError(ex);
        }
    }
}
