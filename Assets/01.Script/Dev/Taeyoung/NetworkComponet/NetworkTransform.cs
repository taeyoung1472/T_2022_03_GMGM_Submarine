using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkTransform : MonoBehaviour
{
    public void SetPosition(Vector3 pos)
    {
        transform.localPosition = pos;
    }
    public void SetRotation(Quaternion rot)
    {
        transform.localRotation = rot;
    }
}
