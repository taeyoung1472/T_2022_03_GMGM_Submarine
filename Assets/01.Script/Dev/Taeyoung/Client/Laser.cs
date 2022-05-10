using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    LineRenderer lineRenderer;
    private void Reset()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
    }
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        RaycastHit hit;
        lineRenderer.SetPosition(0, transform.position);
        if(Physics.Raycast(transform.position, transform.forward, out hit, 100f, layerMask))
        {
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, transform.forward * 100 + transform.position);
        }
    }
}
