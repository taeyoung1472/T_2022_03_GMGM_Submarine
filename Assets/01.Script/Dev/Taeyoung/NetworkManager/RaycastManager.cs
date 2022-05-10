using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    public static RaycastManager instance;
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public bool ShootRaycast(Vector3 startPos, Vector3 dir, LayerMask layerMask, out RaycastHit hit, float range = 10000)
    {
        if(Physics.Raycast(startPos, dir, out hit, range, layerMask))
        {
            return true;
        }
        return false;
    }
}
