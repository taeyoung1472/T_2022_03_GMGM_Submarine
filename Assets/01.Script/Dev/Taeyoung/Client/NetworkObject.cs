using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class NetworkObject : MonoBehaviour
{
    [SerializeField] private UnityEvent SpawnEvent;
    public void PlaySpawnEvent()
    {
        SpawnEvent?.Invoke();
    }
    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }
}
