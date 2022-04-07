using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [Header("따라갈 오브젝트")]
    [SerializeField] private Transform followObject;
    void Update()
    {
        transform.position = followObject.position;
    }
}
