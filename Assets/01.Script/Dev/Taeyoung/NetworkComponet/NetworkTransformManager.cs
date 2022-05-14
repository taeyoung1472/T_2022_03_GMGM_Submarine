using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkTransformManager : MonoBehaviour
{
    [SerializeField] private Dictionary<int, NetworkTransform> netTransforms = new Dictionary<int, NetworkTransform>();
    public Dictionary<int, NetworkTransform> NetTransforms { get { return netTransforms; } }
    public static NetworkTransformManager instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
