using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalTorpedo : MonoBehaviour
{
    Transform target;
    Action boomCallback;
    private void Start()
    {
        Invoke("Boom", 10f);
    }
    private void Update()
    {
        Controll();
    }
    public void OnCollisionEnter(Collision collision)
    {
        Boom();
    }
    private void Controll()
    {
        Quaternion rot = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 10);
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * 10);
    }
    private void Boom()
    {
        boomCallback?.Invoke();
        Destroy(gameObject);
    }
    public void Set(Transform _target)
    {
        target = _target;
    }
}
