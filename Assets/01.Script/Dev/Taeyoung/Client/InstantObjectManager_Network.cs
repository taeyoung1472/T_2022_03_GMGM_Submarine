using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantObjectManager_Network : MonoBehaviour
{
    public static InstantObjectManager_Network Instance;
    [SerializeField] private GameObject[] objects;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void InstantObject(int id, int index, Vector3 pos, Quaternion rot)
    {
        GameObject obj = Instantiate(objects[index], pos, rot);
        NetworkObject netObj = obj.GetComponent<NetworkObject>();
        netObj.PlaySpawnEvent();
        //GameManager_Network.netObjects.Add(id, netObj);
    }
    public void SendedObject()
    {

    }
}
public enum ObjectEnum
{
    Test,
}