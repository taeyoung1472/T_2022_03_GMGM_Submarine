using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [Header("���� ������Ʈ")]
    [SerializeField] private Transform followObject;
    void Update()
    {
        transform.position = followObject.position;
    }
}
